using LoggerLog4net;
using System.Threading.Tasks;

namespace C_sharp_training.Common.OptionPricing.MonteCarlo
{
    public class SimulatedPrice
    {
        private static readonly ILogger logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public static IDiscretizationMonteCarlo DiscretizationScheme { get; set; } 
        public static double Volatility { get; set; } // Volatility (standard deviation) of the instrument
        public static double Drift { get; set; } // Drift (risk-free rate) of the instrument
        public static double SpotPrice { get; set; } // Starting price for the simulation
        public static double StrikePrice { get; set; } // Exercise price of the option
        public static int Steps { get; set; } // Number of steps in the simulation
        public double[] SimulatedPriceArray { get; private set; } // Simulated prices
        public const double h = 1 / 252.0; // cause vol and rates are annual

        public SimulatedPrice()
        {
            this.SimulatedPriceArray = new double[SimulatedPrice.Steps];
            this.SimulatedPriceArray[0] = SimulatedPrice.SpotPrice;
        }

        private void SimulatePrice()
        {
            logger.Debug("Start simulating price ...");
            for (int i=1; i<SimulatedPrice.Steps; i++)
            {
                SimulatedPriceArray[i] = DiscretizationScheme.increment(SimulatedPriceArray[i - 1]);
            }
        }

        public Task RunSim()
        {
            return Task.Run(() => SimulatePrice());
        }
    }
}
