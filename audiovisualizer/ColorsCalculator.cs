using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


//ColorsCalculator calculate the color based on the strength of the signal in the given interval
public class ColorsCalculator
{
    public int[] FindColors(double[] fftMag, int lowColorGain, int midColorGain, int highColorGain)
    {
        int firstIndex = 0;
        double firstThird = 0;
        //You can change interval(i) to expand or reduce the signal calculation area
        for (int i = firstIndex; i < 4; i++) // 0-184Hz for i = 0:4
        {
            firstThird += fftMag[i];
            if (fftMag[i] > fftMag[firstIndex])
                firstIndex = i;
        }
        firstThird = (firstThird / 4) * lowColorGain;
        if (firstThird < 0) { firstThird = 0; }
        if (firstThird > 255) { firstThird = 255; }

        int secondIndex = 4;
        double secondThird = 0;
        //You can change interval(i) to expand or reduce the signal calculation area
        for (int i = secondIndex; i < 80; i++) // 185Hz-3kHz for i = 5:80
        {
            secondThird += fftMag[i];
            if (fftMag[i] > fftMag[secondIndex])
                secondIndex = i;
        }
        secondThird = (secondThird / 76) * midColorGain;
        if (secondThird < 0) { secondThird = 0; }
        if (secondThird > 255) { secondThird = 255; }

        int thirdIndex = 80;
        double thirdThird = 0;
        //You can change interval(i) to expand or reduce the signal calculation area
        for (int i = thirdIndex; i < 173; i++) // 3-8kHz for i = 6:173
        {
            thirdThird += fftMag[i];
            if (fftMag[i] > fftMag[thirdIndex])
                thirdIndex = i;
        }
        thirdThird = (thirdThird / 93) * highColorGain;
        if (thirdThird < 0) { thirdThird = 0; }
        if (thirdThird > 255) { thirdThird = 255; }

        return new int[] { (int)firstThird, (int)secondThird, (int)thirdThird };
    }
}
