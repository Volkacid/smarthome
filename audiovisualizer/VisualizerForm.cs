using LEDVisualizer;
using ScottPlot;
using System.Diagnostics;
using System.Drawing.Imaging;
using System.IO.Ports;
using System.Reflection;

namespace AudioMonitor;

public partial class VisualizerForm : Form
{
    NAudio.Wave.WaveInEvent? Wave;

    readonly double[] AudioValues;
    readonly double[] FftValues;

    readonly int SampleRate = 44100;
    readonly int BitDepth = 16;
    readonly int ChannelCount = 1;
    readonly int BufferMilliseconds = 15; // increase this to increase frequency resolution

    //static String comBed = "COM4";
    //static String comTable = "COM5";
    //static SerialPort _bedPort = new SerialPort(comBed, 38400);
    //static SerialPort _tablePort = new SerialPort(comTable, 38400);

    const int bedStripe = 1;
    const int tableStripe = 2;
    const int bothStripes = 0;
    const int typeStatic = 251;
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
    LEDEffects effects = new LEDEffects();

    Stopwatch programTime = new Stopwatch();

    bool isConnected = false;
    bool manualControl = false;
    bool isSynchonizeActive = false;

    public VisualizerForm()
    {
        InitializeComponent();

        AudioValues = new double[SampleRate * BufferMilliseconds / 1000];
        double[] paddedAudio = FftSharp.Pad.ZeroPad(AudioValues);
        double[] fftMag = FftSharp.Transform.FFTmagnitude(paddedAudio);
        FftValues = new double[fftMag.Length];

        double fftPeriod = FftSharp.Transform.FFTfreqPeriod(SampleRate, fftMag.Length);

        signalPlot.Plot.AddSignal(FftValues, fftPeriod);
        signalPlot.Plot.YLabel("Spectral Power");
        signalPlot.Plot.XLabel("Frequency (kHz)");
        signalPlot.Refresh();
    }

    private void VisualizerForm_Load(object sender, EventArgs e)
    {
        for (int i = 0; i < NAudio.Wave.WaveIn.DeviceCount; i++)
        {
            var caps = NAudio.Wave.WaveIn.GetCapabilities(i);
            inputBox.Items.Add(caps.ProductName);
        }
    }

    private void inputBox_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Wave is not null)
        {
            Wave.StopRecording();
            Wave.Dispose();

            for (int i = 0; i < AudioValues.Length; i++)
                AudioValues[i] = 0;
            signalPlot.Plot.AxisAuto();
        }

        if (inputBox.SelectedIndex == -1)
            return;

        Wave = new NAudio.Wave.WaveInEvent()
        {
            DeviceNumber = inputBox.SelectedIndex,
            WaveFormat = new NAudio.Wave.WaveFormat(SampleRate, BitDepth, ChannelCount),
            BufferMilliseconds = BufferMilliseconds
        };

        Wave.DataAvailable += WaveIn_DataAvailable;
        Wave.StartRecording();

        signalPlot.Plot.Title(inputBox.SelectedItem.ToString());
    }

    void WaveIn_DataAvailable(object? sender, NAudio.Wave.WaveInEventArgs e)
    {
        for (int i = 0; i < e.Buffer.Length / 2; i++)
        {
            AudioValues[i] = BitConverter.ToInt16(e.Buffer, i * 2);
        }
    }

    private void timer1_Tick(object sender, EventArgs e)
    {
        double[] paddedAudio = FftSharp.Pad.ZeroPad(AudioValues);
        double[] fftMag = FftSharp.Transform.FFTpower(paddedAudio);
        Array.Copy(fftMag, FftValues, fftMag.Length);

        if (isConnected)
        {
            int[] colorsPower = new int[3];
            colorsPower = calculator.FindColors(fftMag, lowColorGain, midColorGain, highColorGain);
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
                toStripe.WithScheme(scheme, lowQueue.GetAverage(), midQueue.GetAverage(), highQueue.GetAverage(), bothStripes, typeStatic);
                //toStripe.WithScheme(scheme, lowQueue.GetAverage(), midQueue.GetAverage(), highQueue.GetAverage(), bedStripe);
            }
        }

        // find the frequency peak
        int peakIndex = 0;
        for (int i = 0; i < fftMag.Length; i++)
        {
            if (fftMag[i] > fftMag[peakIndex])
                peakIndex = i;
        }
        double fftPeriod = FftSharp.Transform.FFTfreqPeriod(SampleRate, fftMag.Length);
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
        timeLabel.Text = "" + programTime.Elapsed.Milliseconds;
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

    private void connectButton_Click(object sender, EventArgs e)
    {
        if (connectButton.Text == "Connect")
        {
            try
            {
                //_bedPort.Open();
                //_tablePort.Open();
                connectButton.Text = "Disconnect";
                isConnected = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            int bufferSize = Int32.Parse(smoothBox.Text);
            lowQueue = new CircularBuffer(bufferSize);
            midQueue = new CircularBuffer(bufferSize);
            highQueue = new CircularBuffer(bufferSize);
            lowColorGain = Int32.Parse(lowGain.Text);
            midColorGain = Int32.Parse(midGain.Text);
            highColorGain = Int32.Parse(highGain.Text);
        } else
        {
            //toSerial.WithScheme(scheme, 0, 0, 0, _tablePort);
            //toSerial.WithScheme(scheme, 0, 0, 0, _bedPort);
            toStripe.WithScheme(scheme, 0, 0, 0, bothStripes, typeStatic);
            //_bedPort.Close();
            //_tablePort.Close();
            connectButton.Text = "Connect";
            isConnected = false;
            lowLabel.Text = "Low";
            midLabel.Text = "Mid";
            highLabel.Text = "High";
        }
    }

    private void BitmapForm_Click(object sender, EventArgs e)
    {
        /*if (_tablePort.IsOpen)
        {
            _tablePort.Close();
        }*/
        new Thread(() => new BitmapVisualizer().ShowDialog()).Start();
    }

    private void manualControlBox_CheckedChanged(object sender, EventArgs e)
    {
        if (manualControlBox.Checked)
        {
            manualControl = true;
            bedBox.Enabled = true;
            if (!synchronizeCheck.Checked)
            {
                tableBox.Enabled = true;
            }
        } else
        {
            manualControl = false;
            bedBox.Enabled = false;
            tableBox.Enabled = false;
        }
    }

    private void synchronizeCheck_CheckedChanged(object sender, EventArgs e)
    {
        if (synchronizeCheck.Checked)
        {
            tableBox.Enabled = false;
            isSynchonizeActive = true;
        }
        else
        {
            isSynchonizeActive = false;
            if (manualControlBox.Checked)
            {
                tableBox.Enabled = true;
            }
        }
    }

    private void bedOverflowRadio_CheckedChanged(object sender, EventArgs e)
    {
        if (bedOverflowRadio.Checked && manualControl)
        {
            colorDialog1.FullOpen = true;
            if (colorDialog1.ShowDialog() == DialogResult.Cancel)
                return;
            Color dialogColor = colorDialog1.Color;
            if (synchronizeCheck.Checked)
            {
                toStripe.SendUDP(dialogColor.R, dialogColor.G, dialogColor.B, bothStripes, typeOverflow);
            } else
            {
                toStripe.SendUDP(dialogColor.R, dialogColor.G, dialogColor.B, bedStripe, typeOverflow);
            }
        }
    }

    private void bedPulseRadio_CheckedChanged(object sender, EventArgs e)
    {
        if (bedPulseRadio.Checked && manualControl)
        {
            colorDialog1.FullOpen = true;
            if (colorDialog1.ShowDialog() == DialogResult.Cancel)
                return;
            Color dialogColor = colorDialog1.Color;
            if (synchronizeCheck.Checked)
            {
                toStripe.SendUDP(dialogColor.R, dialogColor.G, dialogColor.B, bothStripes, typePulse);
            }
            else
            {
                toStripe.SendUDP(dialogColor.R, dialogColor.G, dialogColor.B, bedStripe, typeOverflow);
            }
        }
    }

    private void bedStaticRadio_CheckedChanged(object sender, EventArgs e)
    {
        if (bedStaticRadio.Checked && manualControl)
        {
            colorDialog1.FullOpen = true;
            if (colorDialog1.ShowDialog() == DialogResult.Cancel)
                return;
            Color dialogColor = colorDialog1.Color;
            if (isSynchonizeActive)
                toStripe.SendUDP(dialogColor.R, dialogColor.G, dialogColor.B, bothStripes, typeStatic);
            else
                toStripe.SendUDP(dialogColor.R, dialogColor.G, dialogColor.B, bedStripe, typeStatic);
        }
    }

    private void tableOverflowRadio_CheckedChanged(object sender, EventArgs e)
    {
        if (tableOverflowRadio.Checked && manualControl)
        {
            colorDialog1.FullOpen = true;
            if (colorDialog1.ShowDialog() == DialogResult.Cancel)
                return;
            Color dialogColor = colorDialog1.Color;
            toStripe.SendUDP(dialogColor.R, dialogColor.G, dialogColor.B, tableStripe, typeOverflow);
        }
    }

    private void tablePulseRadio_CheckedChanged(object sender, EventArgs e)
    {
        if (tablePulseRadio.Checked && manualControl)
        {
            colorDialog1.FullOpen = true;
            if (colorDialog1.ShowDialog() == DialogResult.Cancel)
                return;
            Color dialogColor = colorDialog1.Color;
            toStripe.SendUDP(dialogColor.R, dialogColor.G, dialogColor.B, tableStripe, typePulse);
        }
    }

    private void tableStaticRadio_CheckedChanged(object sender, EventArgs e)
    {
        if (tableStaticRadio.Checked && manualControl)
        {
            colorDialog1.FullOpen = true;
            if (colorDialog1.ShowDialog() == DialogResult.Cancel)
                return;
            Color dialogColor = colorDialog1.Color;
            toStripe.SendUDP(dialogColor.R, dialogColor.G, dialogColor.B, tableStripe, typeStatic);
        }
    }
}
