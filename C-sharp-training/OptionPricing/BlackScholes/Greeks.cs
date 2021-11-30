using C_sharp_training.Common;
using C_sharp_training.Common.OptionPricing.BlackScholes;
using LoggerLog4net;
using MathNet.Numerics.Distributions;
using System;

namespace C_sharp_training.OptionPricing.BlackScholes
{
    public class GreeksBS : BlackScholesPricer, IGreeks
    {
        private static readonly ILogger logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public GreeksBS(ContractEnum contractType, double S, double K, double T, double sigma, double r, double q) :
            base(contractType, S, K, T, sigma, r, q)
        {

        }
        public double delta()
        {
            logger.Debug("Computing Black Scholes Delta");
            switch (contractType)
            {
                case ContractEnum.EUROPEANCALL:
                    return Math.Exp(-q * T) * Normal.CDF(0, 1, d1(S, K, T, sigma, r, q));

                case ContractEnum.EUROPEANPUT:
                    return -Math.Exp(-q * T) * Normal.CDF(0, 1, -d1(S, K, T, sigma, r, q));

                default:
                    throw new NotSupportedException(" Option Type Error " + contractType + " no delta computation available for this contract kind");
            }
        }

        public double gamma()
        {
            logger.Debug("Computing Black Scholes Gamma");
            switch (contractType)
            {
                case ContractEnum.EUROPEANCALL:
                case ContractEnum.EUROPEANPUT:
                    return (Math.Exp(-q * T) * Math.Exp(-Math.Pow(d1(S, K, T, sigma, r, q), 2) / 2)) / (Math.Sqrt(2 * Math.PI) * S * sigma * Math.Sqrt(T));

                default:
                    throw new NotSupportedException(" Option Type Error " + contractType + " no gamma computation available for this contract kind");
            }
        }

        public double rho()
        {
            logger.Debug("Computing Black Scholes Rho");
            switch (contractType)
            {
                case ContractEnum.EUROPEANCALL:
                    return K * T * Math.Exp(-r * T) * Normal.CDF(0, 1, d2(d1(S, K, T, sigma, r, q), Sigma, T));

                case ContractEnum.EUROPEANPUT:
                    return -T * K * Math.Exp(-r * T) * Normal.CDF(0, 1, -d2(d1(S, K, T, sigma, r, q), Sigma, T));

                default:
                    throw new NotSupportedException(" Option Type Error " + contractType + " no rho computation available for this contract kind");
            }
        }

        public double theta()
        {
            logger.Debug("Computing Black Scholes Theta");
            double d1Param = d1(S, K, T, sigma, r, q);
            double d2Param = d2(d1(S, K, T, sigma, r, q), Sigma, T);
            switch (contractType)
            {
                case ContractEnum.EUROPEANCALL:
                    return (1f / T) * (-(S * sigma * Math.Exp(-q * T) * Math.Exp(-Math.Pow(d1Param, 2) / 2) / (2 * Math.Sqrt(T) * Math.Sqrt(2 * Math.PI))) -
                        r * K * Math.Exp(-r * T) * Normal.CDF(0, 1, d2Param) + q * S * Math.Exp(-q * T) * Normal.CDF(0, 1, d1Param));

                case ContractEnum.EUROPEANPUT:
                    return (1f / T) * (-(S * sigma * Math.Exp(-q * T) * Math.Exp(-Math.Pow(d1Param, 2) / 2) / (2 * Math.Sqrt(T) * Math.Sqrt(2 * Math.PI))) +
                        r * K * Math.Exp(-r * T) * Normal.CDF(0, 1, -d2Param) + q * S * Math.Exp(-q * T) * Normal.CDF(0, 1, -d1Param));
                default:
                    throw new NotSupportedException(" Option Type Error " + contractType + " no theta computation available for this contract kind");
            }
        }

        public double vega()
        {
            logger.Debug("Computing Black Scholes Vega");
            switch (contractType)
            {
                case ContractEnum.EUROPEANCALL:
                case ContractEnum.EUROPEANPUT:
                    return S * Math.Exp(-q * T) * Math.Sqrt(T) * Math.Exp(-Math.Pow(d1(S, K, T, sigma, r, q), 2) / 2) / Math.Sqrt(2 * Math.PI);

                default:
                    throw new NotSupportedException(" Option Type Error " + contractType + " no vega computation available for this contract kind");
            }
        }
    }
}
