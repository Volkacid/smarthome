using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;

public class StripeWriter
{
    public void WithScheme(string scheme, int lowColor, int midColor, int highColor, int stripe, int effectType)
    {
        switch (scheme)
        {
            case "RGB":
                SendUDP(lowColor, midColor, highColor, stripe, effectType);
                break;
            case "RBG":
                SendUDP(lowColor, highColor, midColor, stripe, effectType);
                break;
            case "GRB":
                SendUDP(midColor, lowColor, highColor, stripe, effectType);
                break;
            case "GBR":
                SendUDP(midColor, highColor, lowColor, stripe, effectType);
                break;
            case "BRG":
                SendUDP(highColor, lowColor, midColor, stripe, effectType);
                break;
            case "BGR":
                SendUDP(highColor, midColor, lowColor, stripe, effectType);
                break;
        }
    }

    public void SendUDP(int lowColor, int midColor, int highColor, int stripe, int effectType)
    {
        //Arduino sketch accepts data in the following format: byte[controlByte; stripe number; R; G; B]
        //controlByte defines the effect:
        //251 for static(just changes color)
        //250 for overflow effect
        //249 for pulse effect
        byte[] buf = new byte[5];
        buf[0] = (byte)effectType;
        buf[1] = (byte)stripe;
        buf[2] = (byte)lowColor;
        buf[3] = (byte)midColor;
        buf[4] = (byte)highColor;

        UdpClient RGBSender = new UdpClient();

        //Change this to your UDP listener address
        IPAddress servAddr = new IPAddress(new byte[] { 192, 168, 100, 47 });
        //IPAddress servAddr = new IPAddress(new byte[] { 127, 0, 0, 1 });

        IPEndPoint endPoint = new IPEndPoint(servAddr, 5001);
        RGBSender.Send(buf, buf.Length, endPoint);
        RGBSender.Close();
    }
}
