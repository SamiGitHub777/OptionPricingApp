using LoggerLog4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace C_sharp_training.Common.OptionPricing.MonteCarlo
{
    public class MonteCarloPricing : IPricer
    {
        private static readonly ILogger logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private ContractEnum contractType;
        private int NumberSimulation { get; set; }
        private List<SimulatedPrice> SimulatedPricesList { get; set; }

        public MonteCarloPricing(ContractEnum contractType, IDiscretizationMonteCarlo discretizationSchema, double drift, double vol, double spotPrice
            , double strikePrice, int steps, int numberSim)
        {
            this.contractType = contractType;
            SimulatedPrice.DiscretizationScheme = discretizationSchema;
            SimulatedPrice.Drift = drift;
            SimulatedPrice.Volatility = vol;
            SimulatedPrice.SpotPrice = spotPrice;
            SimulatedPrice.StrikePrice = strikePrice;
            SimulatedPrice.Steps = steps;
            this.NumberSimulation = numberSim;
        }

        private double pv()
        {
            switch (contractType)
            {
                case ContractEnum.EUROPEANCALL:
                    return Math.Exp(-SimulatedPrice.Drift * SimulatedPrice.Steps * SimulatedPrice.h)
                * this.SimulatedPricesList.Select(p => Math.Max(p.SimulatedPriceArray.Last() - SimulatedPrice.StrikePrice, 0)).Average();
                case ContractEnum.EUROPEANPUT:
                    return Math.Exp(-SimulatedPrice.Drift * SimulatedPrice.Steps * SimulatedPrice.h)
                 * this.SimulatedPricesList.Select(p => Math.Max(SimulatedPrice.StrikePrice - p.SimulatedPriceArray.Last(), 0)).Average();
                default:
					throw new NotSupportedException("Monte Carlo not supported for Option Type " + contractType);

            }

        }

        public double? premium()
        {
            logger.Debug("Start computing Monte Carlo price");
            SimulatedPricesList = Enumerable.Range(0, (int)this.NumberSimulation).Select(i => new SimulatedPrice()).ToList();
            Task.WaitAll(SimulatedPricesList.Select(c => c.RunSim()).ToArray());
            return pv();
        }

        /*
         * σ(M)/sqrt(M)
        * where M is the number of simulations and σ is the estimated standard deviation of the specific simulation run
         * 
         */
        public double standardError()
        {
            double[] values = this.SimulatedPricesList.Select(p => p.SimulatedPriceArray.Last()).ToArray();
            double average = values.Average();
            double std = Math.Sqrt(values.Sum(v => Math.Pow(v - average, 2)) / values.Length);
            return std / Math.Sqrt(values.Length);
        }
    }
}
