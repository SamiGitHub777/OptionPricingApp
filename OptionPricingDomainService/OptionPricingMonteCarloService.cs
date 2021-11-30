using C_sharp_training.Common.OptionPricing.MonteCarlo;
using LoggerLog4net;
using OptionPricingDomain;
using OptionPricingRepository;
using System.Diagnostics;

namespace OptionPricingDomainService
{
    public class OptionPricingMonteCarloService : IOptionPricingMethodService
    {
        private static readonly ILogger logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public double Price(Option option)
        {
            logger.Info("Starting Monte Carlo pricing");
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            double timeToMatu = OptionUtils.TimeToMaturity(option);
            IDiscretizationMonteCarlo eulerScheme = new EulerDiscretizationMonteCarlo();
            MonteCarloPricing monteCarloPricing = new MonteCarloPricing(OptionUtils.OptionTypeToPricingEnum(option), eulerScheme, option.RiskFreeRate, 
                option.UnderlyingObj.Volatility, option.UnderlyingObj.Spot, option.Strike, 100, 10000); // TODO : review step and nbr simulation
            stopwatch.Stop();
            logger.Info($"Monte Carlo pricing done in {stopwatch.Elapsed}");
            return monteCarloPricing.premium().Value;
        }
    }
}
