using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class ColorsCalculator
{
    public int[] FindColors(double[] fftMag, int lowColorGain, int midColorGain, int highColorGain)
    {
        int firstIndex = 0;
        double firstThird = 0;
        for (int i = firstIndex; i < 4; i++) // 0-184Hz 4
        {
            firstThird += fftMag[i];
            if (fftMag[i] > fftMag[firstIndex])
                firstIndex = i;
        }
        firstThird = (firstThird / 4) * lowColorGain; //4.25
        if (firstThird < 0) { firstThird = 0; }
        if (firstThird > 255) { firstThird = 255; }

        int secondIndex = 4;
        double secondThird = 0;
        for (int i = secondIndex; i < 80; i++) // 185Hz-3kHz 80
        {
            secondThird += fftMag[i];
            if (fftMag[i] > fftMag[secondIndex])
                secondIndex = i;
        }
        secondThird = (secondThird / 76) * midColorGain; //6.375
        if (secondThird < 0) { secondThird = 0; }
        if (secondThird > 255) { secondThird = 255; }

        int thirdIndex = 80;
        double thirdThird = 0;
        for (int i = thirdIndex; i < 173; i++) // 3-8kHz 173
        {
            thirdThird += fftMag[i];
            if (fftMag[i] > fftMag[thirdIndex])
                thirdIndex = i;
        }
        thirdThird = (thirdThird / 93) * highColorGain; //7.25
        if (thirdThird < 0) { thirdThird = 0; }
        if (thirdThird > 255) { thirdThird = 255; }

        return new int[] { (int)firstThird, (int)secondThird, (int)thirdThird };
    }
}
