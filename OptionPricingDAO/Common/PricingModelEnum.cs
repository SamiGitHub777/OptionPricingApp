using System;
using System.Collections.Generic;

namespace OptionPricingDAO.Common
{
    public class PricingModelEnum
    {
        private static string BLACKSCHOLESVALUE = "Black Scholes";
        private static string BINOMIALTREEVALUE = "Binomial Tree";
        private static string MONTECARLOVALUE = "Monte Carlo";
		private static string UNKNOWNValue = "UNKNOWN";


		private PricingModelEnum(string value) { Value = value; }

        public string Value { get; private set; }

        public static PricingModelEnum BlackScholes { get { return new PricingModelEnum(BLACKSCHOLESVALUE); } }
        public static PricingModelEnum BinomialTree { get { return new PricingModelEnum(BINOMIALTREEVALUE); } }
        public static PricingModelEnum MonteCarlo { get { return new PricingModelEnum(MONTECARLOVALUE); } }
		public static PricingModelEnum UNKNOWN { get { return new PricingModelEnum(UNKNOWNValue); } }


		public static PricingModelEnum FromString(string str)
		{
			if (BLACKSCHOLESVALUE.Equals(str, StringComparison.InvariantCultureIgnoreCase)
				|| "BlackScholes".Equals(str, StringComparison.InvariantCultureIgnoreCase))
			{
				return BlackScholes;
			}
			if (BINOMIALTREEVALUE.Equals(str, StringComparison.InvariantCultureIgnoreCase)
				|| "BinomialTree".Equals(str, StringComparison.InvariantCultureIgnoreCase))
			{
				return BinomialTree;
			}
			if (MONTECARLOVALUE.Equals(str, StringComparison.InvariantCultureIgnoreCase) 
				 || "MonteCarlo".Equals(str, StringComparison.InvariantCultureIgnoreCase))
			{
				return MonteCarlo;
			}
			return UNKNOWN;
		}

		public override string ToString()
		{
			if (BLACKSCHOLESVALUE.Equals(this.Value, StringComparison.InvariantCultureIgnoreCase))
			{
				return "BlackScholes";
			}
			if (BINOMIALTREEVALUE.Equals(this.Value, StringComparison.InvariantCultureIgnoreCase))
			{
				return "BinomialTree";
			}
			if (MONTECARLOVALUE.Equals(this.Value, StringComparison.InvariantCultureIgnoreCase))
			{
				return "MonteCarlo";
			}
			return "UNKNOWN";
		}

		public override bool Equals(object obj)
        {
            return obj is PricingModelEnum @enum &&
                   Value.Equals(@enum.Value);
        }

        public override int GetHashCode()
        {
            return -1937169414 + Value.GetHashCode();
        }
    }

}