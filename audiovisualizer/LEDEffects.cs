using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LEDVisualizer
{
    internal class LEDEffects
    {
        SerialWriter serialWriter = new SerialWriter();

        Stopwatch overColorWatch = new Stopwatch();
        Stopwatch writerWatch = new Stopwatch();

        public void Overflow(SerialPort[] _ports)
        {
            Color color1 = Color.FromArgb(50, 250, 250);
            Color color2 = Color.FromArgb(250, 50, 250);
            Color color3 = Color.FromArgb(250, 50, 50);
            Color color4 = Color.FromArgb(250, 250, 50);
            Color color5 = Color.FromArgb(50, 250, 50);
            int delay = 50;
            OverColor(color1, color2, delay, _ports);
            OverColor(color2, color3, delay, _ports);
            OverColor(color3, color4, delay, _ports);
            OverColor(color4, color5, delay, _ports);
            OverColor(color5, color1, delay, _ports);
        }

        private void OverColor(Color startColor, Color finishColor, int smooth, SerialPort[] _ports)
        {
            float tempRed = startColor.R;
            float tempGreen = startColor.G;
            float tempBlue = startColor.B;
            float stepRed = (finishColor.R - startColor.R) / smooth;
            float stepGreen = (finishColor.G - startColor.G) / smooth;
            float stepBlue = (finishColor.B - startColor.B) / smooth;
            for (int i = 0; i < smooth; i++){
                tempRed += stepRed;
                tempGreen += stepGreen;
                tempBlue += stepBlue;
                foreach (SerialPort _port in _ports)
                {
                    serialWriter.Send((int)tempRed, (int)tempGreen, (int)tempBlue, _port);
                }
                writerWatch.Start();
                while (writerWatch.Elapsed.Milliseconds < smooth*2) { }
                writerWatch.Reset();
            }
        }
    }
}
