using LoggerLog4net;
using System;

namespace C_sharp_training.Common.OptionPricing.MonteCarlo

{
    public class EulerDiscretizationMonteCarlo : IDiscretizationMonteCarlo
    {
        private static readonly ILogger logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public string getName()
        {
            return "Euler Discretization Scheme";
        }

        public double increment(double value)
        {
            logger.Debug("Increment Monte Carlo discretization");
            return value // value at previous step
                    + (SimulatedPrice.Drift * value * SimulatedPrice.h) // Drift term
                    + (SimulatedPrice.Volatility * value * Math.Sqrt(SimulatedPrice.h) * GaussianBoxMuller.get()); // Wiener process
        }
    }
}
