using OptionPricingDomain;
using C_sharp_training.Common.OptionPricing.BinomialTree;
using OptionPricingRepository;
using LoggerLog4net;
using System.Diagnostics;

namespace OptionPricingDomainService
{
    public class OptionPricingBinomialTreeService : IOptionPricingMethodService
    {
        private static readonly ILogger logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public double Price(Option option)
        {
            logger.Info("Starting Binomial Tree pricing");
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            double timeToMatu = OptionUtils.TimeToMaturity(option);
            int steps = 10;
            double timeStep = (double)timeToMatu / steps;
            BinomialTreePricer tree = new BinomialTreePricer(option.UnderlyingObj.Spot, option.Strike, timeStep, option.UnderlyingObj.Volatility,
                                      option.RiskFreeRate, OptionUtils.OptionTypeToPricingEnum(option), steps);
            stopwatch.Stop();
            logger.Info($"Binomial Tree pricing done in {stopwatch.Elapsed}");
            return tree.premium().Value;
        }
    }
}
