using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace C_sharp_training.Common.OptionPricing.MonteCarlo
{
    public static class RandomProvider
    {
        // each time a new instance receives a new seed value
        private static ThreadLocal<Random> random =
            new ThreadLocal<Random>(() => new Random(Guid.NewGuid().GetHashCode())); // globally unique identifier
        
        public static double getRandomValue()
        {
            return random.Value.NextDouble();
        }
    }

}
