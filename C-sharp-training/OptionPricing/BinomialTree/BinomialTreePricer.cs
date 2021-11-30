using LoggerLog4net;
using System;

namespace C_sharp_training.Common.OptionPricing.BinomialTree
{
    public class BinomialTreePricer : IPricer
    {
		private static readonly ILogger logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

		private double spot = 0.0;
        private double strike = 0.0;
        private double timeStep = 0.0;
        private double volatility = 0.0;
        private ContractEnum contractType = ContractEnum.EUROPEANCALL;
        private double riskFreeRate = 0.0;
        private int steps = 0;

		
		public double Spot
		{
			get { return spot; }
			set { spot = value; }
		}

		public double Strike
		{
			get { return strike; }
			set { strike = value; }
		}

		public double TimeStep
		{
			get { return timeStep; }
			set { timeStep = value; }
		}

		public double Volatility
		{
			get { return volatility; }
			set { volatility = value; }
		}

		public ContractEnum ContractType
		{
			get { return contractType; }
			set { contractType = value; }
		}

		public double RiskFreeRate
		{
			get { return riskFreeRate; }
			set { riskFreeRate = value; }
		}

		public int Steps
		{
			get { return steps; }
			set { steps = value; }
		}

		public BinomialTreePricer(
			double spotPrice,
			double strikeParam,
			double timeStepParam,
			double volatilityParam,
			double riskFreeRateParam,
			ContractEnum contratType,
			int stepsParam)
		{
			this.spot = spotPrice;
			this.strike = strikeParam;
			this.volatility = volatilityParam;
			this.timeStep = timeStepParam;
			this.riskFreeRate = riskFreeRateParam;
			this.contractType = contratType;
			this.steps = stepsParam;
		}

		public double? premium()
		{
			logger.Info($"Computing Binomial Tree option price for spot : {spot}, strike : {strike}, volatility : {volatility}, risk free rate {riskFreeRate}, option type : {contractType}");
			decimal totalValue = 0.0m;
			double u = spotUp(timeStep, volatility, steps);
			double d = spotDown(timeStep, volatility, steps);
			double p = probability(timeStep, volatility, steps, riskFreeRate);

			decimal nodeValue = 0.0m;
			double payoffValue = 0.0;
			for (int j = 0; j <= steps; j++)
			{
				double pathProba = Math.Pow(u, j) * Math.Pow(d, steps - j); // proba j times up and (n-j) times down
				payoffValue = optionPayoff(contractType, spot * pathProba, strike);
				nodeValue = binomialNodeValue(j, steps, p);
				totalValue += nodeValue * (decimal) payoffValue; 
			}
			return (double) presentValue(totalValue, riskFreeRate, timeStep);
		}

		private decimal binomialNodeValue(int j, int n, double p)
		{
			return binomialCoefficient(j, n) * (decimal) (Math.Pow(p, j) * Math.Pow((1.0 - p), (n - j)));
		}
		private decimal binomialCoefficient(int j, int n)
		{
			return factorial(n) / (factorial(j) * factorial(n - j));
		}
		private double spotUp(double s, double t, int n)
		{
			return Math.Exp(s * Math.Sqrt(t / n));
		}
		private double spotDown(double s, double t, int n)
		{
			return 1f / spotUp(s, t, n);
		}

		private double probability(double s, double t, int n, double r)
        {
			double sDown = spotDown(s, t, n);
			return (futureValue(1, r, t, n) - sDown) / (spotUp(s, t, n) - sDown);
		}

		private double futureValue(double p, double r, double t, double n)
		{
			return p * Math.Exp(r*t/n);
		}

		private decimal presentValue(decimal f, double r, double n)
		{
			return f * (decimal) (Math.Exp(-r*n));
		}

		private double optionPayoff(ContractEnum contractType, double s, double k)
		{
			switch (contractType)
			{
				case ContractEnum.EUROPEANCALL:
					return Math.Max(s - k, 0.0);

				case ContractEnum.EUROPEANPUT:
					return Math.Max(k - s, 0.0);
				default:
					throw new NotSupportedException(" Option Type Error " + contractType + "Type is not managed !");
			}
		}

		private decimal factorial(int n)
		{
			if (n == 0 || n == 1)
				return 1;
			else
			{
				return n * factorial(n - 1);
			}
		}
    }
}