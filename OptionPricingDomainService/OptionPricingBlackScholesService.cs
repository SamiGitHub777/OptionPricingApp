using C_sharp_training.Common.OptionPricing.BlackScholes;
using LoggerLog4net;
using OptionPricingDomain;
using OptionPricingRepository;
using System.Diagnostics;
using System.Threading;

namespace OptionPricingDomainService
{
    public class OptionPricingBlackScholesService : IOptionPricingMethodService
    {
        private static readonly ILogger logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public double Price(Option option)
        {
            logger.Info("Starting Black Scholes pricing");
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            double timeToMatu = OptionUtils.TimeToMaturity(option);
            BlackScholesPricer bs = new BlackScholesPricer(OptionUtils.OptionTypeToPricingEnum(option), option.UnderlyingObj.Spot, option.Strike, 
                                    timeToMatu, option.UnderlyingObj.Volatility, option.RiskFreeRate, 0d);
            stopwatch.Stop();
            logger.Info($"Black Scholes pricing done in {stopwatch.Elapsed}");
            return bs.premium().Value;
        }
    }
}
