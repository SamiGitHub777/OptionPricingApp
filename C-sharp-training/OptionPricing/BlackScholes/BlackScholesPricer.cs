using System;
using MathNet.Numerics.Distributions;
using System.Data;
using LoggerLog4net;

namespace C_sharp_training.Common.OptionPricing.BlackScholes
{
    public class BlackScholesPricer : IPricer
    {
        private static readonly ILogger logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        protected double S;
        protected double K;
        protected double r;
        protected double q;
        protected double sigma;
        protected double T;
        protected ContractEnum contractType;

        public BlackScholesPricer(ContractEnum contractType, double S, double K, double T, double sigma, double r, double q)
        {
            this.contractType = contractType;
            this.S = S;
            this.K = K;
            this.T = T;
            this.sigma = sigma;
            this.r = r;
            this.q = q;
        }

        public double s { get { return S; } set { S = value; } }
        public double k { get => K; set => K = value; }
        public double R { get => r; set => r = value; }
        public double Q { get => q; set => q = value; }
        public double Sigma { get { return sigma; } set { sigma = value; } }
        public double t { get { return T; } set { T = value; } }
        public ContractEnum ContractType { get { return contractType; } set { contractType = value; } }


        public double? premium()
        {
            return premiumForSpecificSpot(S);
        }

        private double? premiumForSpecificSpot(double spot)
        {
            logger.Debug($"Computing Black Scholes price for option type : {contractType}, spot : {S}, " +
                $"strike : {K}, risk free rate : {R}, dividend rate : {Q}, Volatility : {Sigma}, time to maturity : {T}");
            if (!areOptionParamValid(spot, K, T, sigma, r, q))
            {
                logger.Error("Option parameters not valid");
                return null;
            }
            double d1Param = d1(spot, K, T, sigma, r, q);
            double d2Param = d2(d1Param, sigma, T);

            switch (contractType)
            {
                case ContractEnum.EUROPEANCALL:
                    return spot * Math.Exp(-q * T) * Normal.CDF(0, 1, d1Param) - K * Math.Exp(-r * T) * Normal.CDF(0, 1, d2Param);

                case ContractEnum.EUROPEANPUT:
                    return K * Math.Exp(-r * T) * Normal.CDF(0, 1, -d2Param) - spot * Math.Exp(-q * T) * Normal.CDF(0, 1, -d1Param);

                default:
                    throw new NotSupportedException(" Option Type Error 1 " + contractType + "Type does not exist!");
            }
        }
        public static double d1(double S, double K, double T, double sigma, double r, double q)
        {
            return (Math.Log(S / K) + (r - q + Math.Pow(sigma, 2) / 2) * T) / (sigma * Math.Sqrt(T));
        }

        public static double d2(double d1, double sigma, double T)
        {
            return d1 - sigma * Math.Sqrt(T);
        }

        public static double timeToMaturity(DateTimeOffset contractMaturity, DateTimeOffset time)
        {
            return (contractMaturity - time).TotalDays / 365.0;
        }

        private static bool areOptionParamValid(double S, double K, double T, double sigma, double r, double q)
        {
            if (T < 0 || S < 0 || K < 0 || sigma < 0)
            {
                return false;
            }
            if (r < 0)
            {
                logger.Warn("Continuously compounded risk-free interest rate negative");
                return false;
            }

            else if (q < 0)
            {
                logger.Warn("Continuously compounded dividend yield negative");
                return false;
            }
            return true;
        }

        public void premiumChart()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Spot_Value", typeof(double));
            dt.Columns.Add("PV_Value", typeof(double));

            double increment = 0.1 * K;
            logger.Info("Spot | PV");
            for (double spot = increment; spot < 2 * K; spot += increment)
            {
                double? pv = premiumForSpecificSpot(spot);
                dt.Rows.Add(spot, pv);
                logger.Info($"{spot} | {pv}");
            }

        }
    }
}
