using System;
using System.Collections.Generic;

namespace OptionPricingDAO.Common
{
	public class ContractEnum
	{
		private static string EUROPEANCALLvalue = "EuropeanCall";
		private static string EUROPEANPUTvalue = "EuropeanPut";
		private static string AMERICANCALLvalue = "AmericanCall";
		private static string AMERICANPUTvalue = "AmericanPut";

		
        private ContractEnum(string value) { Value = value; }
		public string Value { get; private set; }
		public static ContractEnum EUROPEANCALL { get { return new ContractEnum(EUROPEANCALLvalue); } }
		public static ContractEnum EUROPEANPUT { get { return new ContractEnum(EUROPEANPUTvalue); } }
		public static ContractEnum AMERICANCALL { get { return new ContractEnum(AMERICANCALLvalue); } }
		public static ContractEnum AMERICANPUT { get { return new ContractEnum(AMERICANPUTvalue); } }

		public static ContractEnum FromString(string str)
        {
			if (EUROPEANCALLvalue.Equals(str, StringComparison.InvariantCultureIgnoreCase))
            {
				return EUROPEANCALL;
            }
			if (EUROPEANPUTvalue.Equals(str, StringComparison.InvariantCultureIgnoreCase))
			{
				return EUROPEANPUT;
			}
			if (AMERICANCALLvalue.Equals(str, StringComparison.InvariantCultureIgnoreCase))
			{
				return AMERICANCALL;
			}
			if (AMERICANPUTvalue.Equals(str, StringComparison.InvariantCultureIgnoreCase))
			{
				return AMERICANPUT;
			}
			return null;
		}

        public override bool Equals(object obj)
        {
            return obj is ContractEnum @enum &&
                   Value.Equals(@enum.Value);
        }

        public override int GetHashCode()
        {
            return -1937169414 + Value.GetHashCode();
        }
    }
}
