using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


//ColorsCalculator calculate the color based on the strength of the signal in the given interval
public class ColorsCalculator
{
    public int[] FindColors(double[] fftMag, double fftPeriod, int lowColorGain, int midColorGain, int highColorGain)
    {
        int lowColorMaxPeriod = Convert.ToInt32(fftMag.Length * (300 / (fftPeriod * fftMag.Length))); //300Hz
        int midColorMaxPeriod = Convert.ToInt32(fftMag.Length * (1500 / (fftPeriod * fftMag.Length))); //1.5kHz

        double lowColorCounter = 100;
        double midColorCounter = 100;
        double highColorCounter = 100;


        for (int i = 0; i < lowColorMaxPeriod; i++)
        {
            lowColorCounter += fftMag[i];
        }
        if (lowColorCounter < 0) { lowColorCounter = 0; }
        int lowColorMax = Convert.ToInt32(lowColorCounter / lowColorMaxPeriod * lowColorGain);
        if (lowColorMax > 250) {
            lowColorMax = 250;
        }

        for (int i = lowColorMaxPeriod; i < midColorMaxPeriod; i++)
        {
            midColorCounter += fftMag[i];
        }
        if (midColorCounter < 0) { midColorCounter = 0; }
        int midColorMax = Convert.ToInt32(midColorCounter / (midColorMaxPeriod - lowColorMaxPeriod) * midColorGain);
        if (midColorMax > 250)
        {
            midColorMax = 250;
        }

        for (int i = midColorMaxPeriod; i < fftMag.Length; i++)
        {
            highColorCounter += fftMag[i];
        }
        if (highColorCounter < 0) { highColorCounter = 0; }
        int highColorMax = Convert.ToInt32(highColorCounter / (fftMag.Length - midColorMaxPeriod) * highColorGain);
        if (highColorMax > 250)
        {
            highColorMax = 250;
        }

        return new int[] { lowColorMax, midColorMax, highColorMax };

        /*int lowColorPower = (firstThird / periodDelimeter * 8) * lowColorGain;
        if (firstThird < 0) { firstThird = 0; }
        if (firstThird > 255) { firstThird = 255; }

        int secondIndex = periodDelimeter * 8;
        double secondThird = 0;
        //You can change interval(i) to expand or reduce the signal calculation area
        for (int i = secondIndex; i < periodDelimeter * 160; i++)
        {
            secondThird += fftMag[i];
            if (fftMag[i] > fftMag[secondIndex])
                secondIndex = i;
        }
        secondThird = (secondThird / periodDelimeter * 160) * midColorGain;
        if (secondThird < 0) { secondThird = 0; }
        if (secondThird > 255) { secondThird = 255; }

        int thirdIndex = periodDelimeter * 160;
        double thirdThird = 0;
        //You can change interval(i) to expand or reduce the signal calculation area
        for (int i = thirdIndex; i < periodDelimeter * 346; i++)
        {
            thirdThird += fftMag[i];
            if (fftMag[i] > fftMag[thirdIndex])
                thirdIndex = i;
        }
        thirdThird = (thirdThird / periodDelimeter * 346) * highColorGain;
        if (thirdThird < 0) { thirdThird = 0; }
        if (thirdThird > 255) { thirdThird = 255; }

        return new int[] { lowColorPower, (int)secondThird, (int)thirdThird };*/

    }
}
