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
        int periodDelimeter = fftMag.Length / 83;
        int firstIndex = 0;
        double firstThird = 0;
        //You can change interval(i) to expand or reduce the signal calculation area
        for (int i = firstIndex; i < periodDelimeter * 8; i++)
        {
            firstThird += fftMag[i];
            if (fftMag[i] > fftMag[firstIndex])
                firstIndex = i;
        }
        firstThird = (firstThird / periodDelimeter * 8) * lowColorGain;
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

        return new int[] { (int)firstThird, (int)secondThird, (int)thirdThird };
    }
}
