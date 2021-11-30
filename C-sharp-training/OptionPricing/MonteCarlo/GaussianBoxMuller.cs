using System;

namespace C_sharp_training.Common.OptionPricing.MonteCarlo
{
    class GaussianBoxMuller
    {
            public static double get()
            {
            //Random rand = new Random(); // need to reuse this
            // using the same Radom instance from several threads could crash it, it would return 0
            // multiple instance could be launched at same time => same pseudo random values
            double u1 = 1.0 - RandomProvider.getRandomValue();
            double u2 = 1.0 - RandomProvider.getRandomValue();
            return Math.Sqrt(-2.0 * Math.Log(u1)) * Math.Sin(2.0 * Math.PI * u2);
            }
        }
    }
