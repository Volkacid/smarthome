using LEDVisualizer;
using System.Diagnostics;
using System.Windows.Media.Imaging;
using Windows.Media.Control;
using Windows.Storage.Streams;
using WindowsMediaController;
using System.IO;
using NAudio.Wave;
using NAudio.Wave.SampleProviders;

namespace AudioMonitor;

public partial class VisualizerForm : Form
{
    WasapiLoopbackCapture outCapture;

    readonly double[] AudioValues;
    readonly double[] FftValues;
    double fftPeriod;

    //readonly int SampleRate = 38400;
    readonly int SampleRate = 48000;
    int BitDepth = 16;
    readonly int ChannelCount = 2; //TODO: set automatically
    readonly int BufferMilliseconds = 15; // increase this to increase frequency resolution

    const int allStripes = 0;
    const int kitchenDownStripe = 1;
    const int kitchenUpStripe = 2;
    const int typeStatic = 255;
    const int typeOverflow = 250;
    const int typePulse = 249;

    CircularBuffer lowQueue;
    CircularBuffer midQueue;
    CircularBuffer highQueue;

    int lowColorGain;
    int midColorGain;
    int highColorGain;

    string scheme = "RGB";

    StripeWriter toStripe = new StripeWriter();
    ColorsCalculator calculator = new ColorsCalculator();

    Stopwatch programTime = new Stopwatch();

    bool isConnected = false;
    bool manualControl = false;
    bool isOtherSettingsActive = false;

    private static readonly MediaManager mediaManager = new MediaManager();

    public VisualizerForm()
    {
        InitializeComponent();

        AudioValues = new double[SampleRate * BufferMilliseconds / 1000];

        double[] paddedAudio = FftSharp.Pad.ZeroPad(AudioValues);
        double[] fftMag = FftSharp.Transform.FFTmagnitude(paddedAudio);
        FftValues = new double[fftMag.Length];

        fftPeriod = FftSharp.Transform.FFTfreqPeriod(SampleRate, fftMag.Length);

        signalPlot.Plot.AddSignal(FftValues, fftPeriod * 0.47); //TODO: this multiplier
        signalPlot.Plot.YLabel("Spectral Power");
        signalPlot.Plot.XLabel("Frequency (kHz)");
        signalPlot.Refresh();

        mediaManager.OnAnyMediaPropertyChanged += MediaManager_OnAnyMediaPropertyChanged;
        mediaManager.Start();
    }

    private void VisualizerForm_Load(object sender, EventArgs e)
    {
        for (int i = 0; i < NAudio.Wave.WaveOut.DeviceCount; i++)
        {
            var caps = NAudio.Wave.WaveOut.GetCapabilities(i);
            inputBox.Items.Add(caps.ProductName);
        }
    }

    private void inputBox_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (outCapture is not null)
        {
            outCapture.StopRecording();
            outCapture.Dispose();

            for (int i = 0; i < AudioValues.Length; i++)
                AudioValues[i] = 0;
            signalPlot.Plot.AxisAuto();
        }

        outCapture = new WasapiLoopbackCapture();
        outCapture.DataAvailable += (s, a) =>
        {
            ToPCM16(a.Buffer, a.BytesRecorded, outCapture.WaveFormat);
        };
        BitDepth = outCapture.WaveFormat.BitsPerSample;
        outCapture.StartRecording();

        Debug.WriteLine(outCapture.WaveFormat.SampleRate);
        Debug.WriteLine(outCapture.WaveFormat.BitsPerSample);
        Debug.WriteLine(outCapture.WaveFormat.Channels);
        Debug.WriteLine(outCapture.WaveFormat.AverageBytesPerSecond);

        signalPlot.Plot.Title(inputBox.SelectedItem.ToString());
    }

    void ToPCM16(byte[] inputBuffer, int length, WaveFormat format)
    {
        // Create a WaveStream from the input buffer.
        using var memStream = new MemoryStream(inputBuffer, 0, length);
        using var inputStream = new RawSourceWaveStream(memStream, format);

        // Convert the input stream to a WaveProvider in 16bit PCM format with sample rate of 48000 Hz.
        var convertedPCM = new SampleToWaveProvider16(
            new WdlResamplingSampleProvider(
                new WaveToSampleProvider(inputStream),
                48000)
            );

        byte[] convertedBuffer = new byte[length];

        using var stream = new MemoryStream();
        int read;

        // Read the converted WaveProvider into a buffer and turn it into a Stream.
        while ((read = convertedPCM.Read(convertedBuffer, 0, length)) > 0)
            stream.Write(convertedBuffer, 0, read);

        byte[] arr = stream.ToArray();
        for (int i = 0; i < arr.Length / 4; i++)
        {
            if (i >= AudioValues.Length)
            {
                break;
            }
            AudioValues[i] = BitConverter.ToInt16(arr, i * 4);
        }
    }

    private void timer1_Tick(object sender, EventArgs e)
    {
        double[] paddedAudio = FftSharp.Pad.ZeroPad(AudioValues);
        double[] fftMag = FftSharp.Transform.FFTpower(paddedAudio);
        Array.Copy(fftMag, FftValues, fftMag.Length);

        if (isConnected && !isOtherSettingsActive)
        {
            int[] colorsPower = new int[3];
            colorsPower = calculator.FindColors(fftMag, fftPeriod, lowColorGain, midColorGain, highColorGain);
            int lowPower = colorsPower[0];
            int midPower = colorsPower[1];
            int highPower = colorsPower[2];

            lowLabel.Text = "L:" + lowPower;
            midLabel.Text = "M:" + midPower;
            highLabel.Text = "H:" + highPower;

            lowQueue.Enqueue(lowPower);
            midQueue.Enqueue(midPower);
            highQueue.Enqueue(highPower);
            if (!manualControl)
            {
                toStripe.WithScheme(scheme, lowQueue.GetAverage(), midQueue.GetAverage(), highQueue.GetAverage(), allStripes, typeStatic);
            }
        }

        // find the frequency peak
        int peakIndex = 0;
        for (int i = 0; i < fftMag.Length; i++)
        {
            if (fftMag[i] > fftMag[peakIndex])
                peakIndex = i;
        }
        double peakFrequency = fftPeriod * peakIndex;
        peakLabel.Text = $"Peak Frequency: {peakFrequency:N0} Hz";

        // auto-scale the plot Y axis limits
        double fftPeakMag = fftMag.Max();
        double signalplotYMax = signalPlot.Plot.GetAxisLimits().YMax;
        signalPlot.Plot.SetAxisLimits(
            xMin: 0,
            xMax: 4,
            yMin: 0,
            yMax: Math.Max(fftPeakMag, signalplotYMax));

        signalPlot.RefreshRequest();
        programTime.Reset();
        programTime.Start();
    }

    private void schemeBox_SelectedIndexChanged(object sender, EventArgs e)
    {
        Color red = Color.FromArgb(255, 192, 192);
        Color green = Color.FromArgb(192, 255, 192);
        Color blue = Color.FromArgb(192, 255, 255);
        Color[] colorGainSet = new Color[3];
        scheme = schemeBox.Text.Remove(3);
        for (int i = 0; i < 3; i++)
        {
            switch (scheme[i])
            {
                case 'R':
                    colorGainSet[i] = red;
                    break;
                case 'G':
                    colorGainSet[i] = green;
                    break;
                case 'B':
                    colorGainSet[i] = blue;
                    break;
            }
        }
        lowGain.BackColor = colorGainSet[0];
        midGain.BackColor = colorGainSet[1];
        highGain.BackColor = colorGainSet[2];
    }

    private async void connectButton_Click(object sender, EventArgs e)
    {
        if (connectButton.Text == "Connect")
        {
            connectButton.Text = "Disconnect";
            isConnected = true;

            //Smoothing
            int bufferSize = Int32.Parse(smoothBox.Text);
            lowQueue = new CircularBuffer(bufferSize);
            midQueue = new CircularBuffer(bufferSize);
            highQueue = new CircularBuffer(bufferSize);

            //Gain
            lowColorGain = Int32.Parse(lowGain.Text);
            midColorGain = Int32.Parse(midGain.Text);
            highColorGain = Int32.Parse(highGain.Text);
        }
        else
        {
            toStripe.WithScheme(scheme, 0, 0, 0, allStripes, typeStatic);
            connectButton.Text = "Connect";
            isConnected = false;
            lowLabel.Text = "Low";
            midLabel.Text = "Mid";
            highLabel.Text = "High";
            Thread.Sleep(500);
            toStripe.WithScheme(scheme, 0, 0, 0, allStripes, typeStatic); //definitely turn it off
        }

    }

    //Effects for manual control
    bool isThumbnailCorrectionActive = false;

    private void MediaManager_OnAnyMediaPropertyChanged(MediaManager.MediaSession sender, GlobalSystemMediaTransportControlsSessionMediaProperties args)
    {
        Trace.WriteLine("Media property chaged");
        if (args.Thumbnail != null)
        {
            var thumb = GetThumbnail(args.Thumbnail);
            Bitmap image = BitmapImage2Bitmap(thumb);
            int matchPixelCount = 0;
            int matchRed = 0;
            int matchGreen = 0;
            int matchBlue = 0;
            for (int x = 0; x < image.Width; x++)
            {
                for (int y = 0; y < image.Height; y++)
                {
                    Color currentPixel = image.GetPixel(x, y);
                    if ((currentPixel.R + currentPixel.G + currentPixel.B) >= 100)
                    {
                        matchPixelCount++;
                        matchRed += currentPixel.R;
                        matchGreen += currentPixel.G;
                        matchBlue += currentPixel.B;
                    }
                }
            }
            int finalRed = matchRed / matchPixelCount;
            int finalGreen = matchGreen / matchPixelCount;
            int finalBlue = matchBlue / matchPixelCount;
            Color imageDominantColor = Color.FromArgb(finalRed, finalGreen, finalBlue);
            Trace.WriteLine("Dominant color:");
            Trace.WriteLine(imageDominantColor);
            if (isThumbnailCorrectionActive && isConnected && isOtherSettingsActive)
            {
                toStripe.SendUDP(imageDominantColor.R, imageDominantColor.G, imageDominantColor.B, allStripes, typeStatic);
            }
        }
    }

    internal static BitmapImage GetThumbnail(IRandomAccessStreamReference Thumbnail)
    {
        var imageStream = Thumbnail.OpenReadAsync().GetAwaiter().GetResult();
        byte[] fileBytes = new byte[imageStream.Size];
        using (DataReader reader = new DataReader(imageStream))
        {
            reader.LoadAsync((uint)imageStream.Size).GetAwaiter().GetResult();
            reader.ReadBytes(fileBytes);
        }

        var image = new BitmapImage();
        using (var ms = new System.IO.MemoryStream(fileBytes))
        {
            image.BeginInit();
            image.CacheOption = BitmapCacheOption.OnLoad;
            image.StreamSource = ms;
            image.EndInit();
        }
        return image;
    }

    internal static Bitmap BitmapImage2Bitmap(BitmapImage bitmapImage)
    {
        using (MemoryStream outStream = new MemoryStream())
        {
            BitmapEncoder enc = new BmpBitmapEncoder();
            enc.Frames.Add(BitmapFrame.Create(bitmapImage));
            enc.Save(outStream);
            System.Drawing.Bitmap bitmap = new System.Drawing.Bitmap(outStream);

            return new Bitmap(bitmap);
        }
    }

    private void settingsThumbnailBtn_CheckedChanged(object sender, EventArgs e)
    {
        if (!isConnected) { return; }

        isThumbnailCorrectionActive = true;
        isOtherSettingsActive = true;
        isDominantColorCorrectionActive = false;
    }

    bool isDominantColorCorrectionActive = false;

    private void settingsDominantBtn_CheckedChanged(object sender, EventArgs e)
    {
        if (!isConnected) { return; }

        isThumbnailCorrectionActive = false;
        isOtherSettingsActive = true;
        isDominantColorCorrectionActive = true;

        colorDialog1.FullOpen = true;
        if (colorDialog1.ShowDialog() == DialogResult.Cancel) {
            colorDialog1.Reset();
            return; 
        }
        Color dialogColor = colorDialog1.Color;
        toStripe.SendUDP(dialogColor.R, dialogColor.G, dialogColor.B, allStripes, typeStatic);
    }

    private void settingsNoneBtn_CheckedChanged(object sender, EventArgs e)
    {
        isThumbnailCorrectionActive = false;
        isOtherSettingsActive = false;
        isDominantColorCorrectionActive = false;
    }

    private void BitmapForm_Click(object sender, EventArgs e)
    {
        new Thread(() => new BitmapVisualizer().ShowDialog()).Start();
    }
}
