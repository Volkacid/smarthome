using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LEDVisualizer
{
    public partial class BitmapVisualizer : Form
    {
        BitmapMaker bitmapMaker = new BitmapMaker();

        //SerialWriter toSerial = new SerialWriter();

        Stopwatch programTime = new Stopwatch();

        Color[] currentColors = new Color[114];
        Color[] previousColors = new Color[114];
        int verticalOffset = 0;
        int horizontalOffset = 0;
        bool automaticVerticalOffset = false;
        bool automaticHorizontalOffset = false;

        SerialPort _tablePort = new SerialPort();
        String comTable = "COM3";

        public BitmapVisualizer()
        {
            InitializeComponent();
            _tablePort.PortName = comTable;
            _tablePort.BaudRate = 115200;
            _tablePort.DataBits = 8;
            _tablePort.Open();
        }

        private async void timer1_Tick(object sender, EventArgs e)
        {
            Bitmap captureBitmap = bitmapMaker.Make();
            if (automaticVerticalOffset)
            {
                verticalOffset = findVerticalOffset(captureBitmap);
                verticalOffsetCheck.Text = "Вертикально: " + verticalOffset;
            }
            if (automaticHorizontalOffset)
            {
                horizontalOffset = findHorizontalOffset(captureBitmap);
                horizontalOffsetCheck.Text = "Горизонтально: " + horizontalOffset;
            }

            for (int index = 1; index <= 37; index++)
            {
                //currentColors[index + 56] = captureBitmap.GetPixel(index * 103, 0 + verticalOffset);
                currentColors[index + 56] = CombineColors(captureBitmap.GetPixel((index * 103) - 1, 0 + verticalOffset), captureBitmap.GetPixel(index * 103, 0 + verticalOffset), captureBitmap.GetPixel((index * 103) + 1, 0 + verticalOffset));
                //currentColors[37 - index] = captureBitmap.GetPixel(index * 103, 2159 - verticalOffset);
                currentColors[37 - index] = CombineColors(captureBitmap.GetPixel((index * 103) - 1, 2159 - verticalOffset), captureBitmap.GetPixel(index * 103, 2159 - verticalOffset), captureBitmap.GetPixel((index * 103) + 1, 2159 - verticalOffset));
            }
            for (int index = 1; index <= 20; index++)
            {
                //currentColors[57 - index] = captureBitmap.GetPixel(0 + horizontalOffset, index * 107);
                currentColors[57 - index] = CombineColors(captureBitmap.GetPixel(0 + horizontalOffset, (index * 107) - 1), captureBitmap.GetPixel(0 + horizontalOffset, index * 107), captureBitmap.GetPixel(0 + horizontalOffset, (index * 107) + 1));
                //currentColors[93 + index] = captureBitmap.GetPixel(3839 - horizontalOffset, index * 107);
                currentColors[93 + index] = CombineColors(captureBitmap.GetPixel(3839 - horizontalOffset, (index * 107) - 1), captureBitmap.GetPixel(3839 - horizontalOffset, index * 107), captureBitmap.GetPixel(3839 - horizontalOffset, (index * 107) + 1));
            }

            bool someChanged = false;
            int counter = 0;
            int[] shortBuf = new int[3];
            byte[] buf = new byte[13];
            for (int i = 0; i < currentColors.Length; i++)
            {
                if (currentColors[i] != previousColors[i])
                {
                    someChanged = true;
                    previousColors[i] = currentColors[i];
                    shortBuf[counter] = i;
                    counter++;
                    if (counter >= 3)
                    {
                        buf[0] = (byte)254;
                        buf[1] = (byte)shortBuf[0];
                        buf[2] = (byte)currentColors[shortBuf[0]].R;
                        buf[3] = (byte)currentColors[shortBuf[0]].G;
                        buf[4] = (byte)currentColors[shortBuf[0]].B;
                        buf[5] = (byte)shortBuf[1];
                        buf[6] = (byte)currentColors[shortBuf[1]].R;
                        buf[7] = (byte)currentColors[shortBuf[1]].G;
                        buf[8] = (byte)currentColors[shortBuf[1]].B;
                        buf[9] = (byte)shortBuf[2];
                        buf[10] = (byte)currentColors[shortBuf[2]].R;
                        buf[11] = (byte)currentColors[shortBuf[2]].G;
                        buf[12] = (byte)currentColors[shortBuf[2]].B;
                        _tablePort.Write(buf, 0, 13);
                        counter = 0;
                    }

                    /*buf[1] = (byte)i;
                    buf[2] = (byte)currentColors[i].R;
                    buf[3] = (byte)currentColors[i].G;
                    buf[4] = (byte)currentColors[i].B;*/
                    //_serialPort.Write(buf, 0, 13);
                    //Console.WriteLine(String.Join(" ", buf));

                    //stopwatch.Start();
                    //while (stopwatch.Elapsed.Milliseconds < 6) { }
                    //stopwatch.Reset();
                }
            }
            if (someChanged)
            {
                //_serialPort.WriteLine("(X)");
                //Console.WriteLine("(X)");
                buf[0] = (byte)255;
                _tablePort.Write(buf, 0, 13);
                //Console.WriteLine(String.Join(" ", buf));
                //stopwatch.Start();
                //while (stopwatch.Elapsed.Milliseconds < 5) { }
                //stopwatch.Reset();
                someChanged = false;
            }
            timeLabel.Text = "" + programTime.ElapsedMilliseconds;
            programTime.Reset();
            programTime.Start();
        }
        
        private int findVerticalOffset(Bitmap image)
        {
            int verticalOffset = 0;
            for (int i = 0; i < image.Height / 2; i++)
            {
                Color pixelColor = image.GetPixel(image.Width / 2, i);
                if (pixelColor.R != 0 || pixelColor.G != 0 || pixelColor.B != 0)
                {
                    verticalOffset = i;
                    break;
                }
            }
            return verticalOffset + 5;
        }

        private int findHorizontalOffset(Bitmap image)
        {
            int horizontalOffset = 0;
            for (int i = 0; i < (image.Width / 2); i++)
            {
                Color pixelColor = image.GetPixel(i, image.Height / 2);
                if (pixelColor.R != 0 || pixelColor.G != 0 || pixelColor.B != 0)
                {
                    horizontalOffset = i;
                    break;
                }
            }
            return horizontalOffset + 5;
        }

        private void verticalOffsetCheck_CheckedChanged(object sender, EventArgs e)
        {
            if (verticalOffsetCheck.Checked)
            {
                automaticVerticalOffset = true;
            } else
            {
                automaticVerticalOffset = false;
                verticalOffset = 0;
                verticalOffsetCheck.Text = "Вертикально";
            }
        }

        private void horizontalOffsetCheck_CheckedChanged(object sender, EventArgs e)
        {
            if (horizontalOffsetCheck.Checked)
            {
                automaticHorizontalOffset = true;
            } else
            {
                automaticHorizontalOffset = false;
                horizontalOffset = 0;
                horizontalOffsetCheck.Text = "Горизонтально";
            }
        }

        private Color CombineColors(Color firstColor, Color secondColor, Color thirdColor)
        {
            int finalRed = (firstColor.R + secondColor.R + thirdColor.R) / 3;
            int finalGreen = (firstColor.G + secondColor.G + thirdColor.G) / 3;
            int finalBlue = (firstColor.B + secondColor.B + thirdColor.B) / 3;
            return Color.FromArgb(finalRed, finalGreen, finalBlue);
        }
    }
}
