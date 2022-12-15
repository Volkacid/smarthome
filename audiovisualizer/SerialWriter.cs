using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class SerialWriter
{
    public void WithScheme(string scheme, int lowColor, int midColor, int highColor, SerialPort _stripPort)
    {
        switch (scheme)
        {
            case "RGB":
                Send(lowColor, midColor, highColor, _stripPort);
                break;
            case "RBG":
                Send(lowColor, highColor, midColor, _stripPort);
                break;
            case "GRB":
                Send(midColor, lowColor, highColor, _stripPort);
                break;
            case "GBR":
                Send(midColor, highColor, lowColor, _stripPort);
                break;
            case "BRG":
                Send(highColor, lowColor, midColor, _stripPort);
                break;
            case "BGR":
                Send(highColor, midColor, lowColor, _stripPort);
                break;
        }
    }

    public void Send(int lowColor, int midColor, int highColor, SerialPort _stripPort)
    {
        byte[] buf = new byte[5];
        buf[0] = (byte)251;
        buf[2] = (byte)lowColor;
        buf[3] = (byte)midColor;
        buf[4] = (byte)highColor;
        if (_stripPort.IsOpen == true)
        {
            _stripPort.Write(buf, 0, 5);
        }
    }

    public Color[] WithAmbilight(Color[] currentColors, Color[] previousColors, SerialPort _stripPort)
    {
        byte[] buf = new byte[13];
        int[] shortBuf = new int[3];
        int counter = 0;
        bool someChanged = false;
        for (int i = 0; i < currentColors.Length; i++)
        {
            if (currentColors[i] != previousColors[i])
            {
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
                    _stripPort.Write(buf, 0, 13);
                    counter = 0;
                    someChanged = true;
                }
            }
            if (someChanged)
            {
                buf[0] = (byte)255;
                _stripPort.Write(buf, 0, 13);
                someChanged = false;
            }
        }
        return previousColors;
    }
}
