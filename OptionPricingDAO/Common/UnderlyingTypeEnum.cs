using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptionPricingDAO.Common
{
    public class UnderlyingTypeEnum
		{
		private static string StockValue = "Stock";
		private static string IndexValue = "Index";
		private static string CommodityValue = "Commodity";
		private static string FXValue = "FX";
		private static string OTHERValue = "Other";

		private UnderlyingTypeEnum(string value) { Value = value; }
			public string Value { get; private set; }
			public static UnderlyingTypeEnum STOCK { get { return new UnderlyingTypeEnum(StockValue); } }
			public static UnderlyingTypeEnum INDEX { get { return new UnderlyingTypeEnum(IndexValue); } }
			public static UnderlyingTypeEnum COMMODITY { get { return new UnderlyingTypeEnum(CommodityValue); } }
			public static UnderlyingTypeEnum FX { get { return new UnderlyingTypeEnum(FXValue); } }
			public static UnderlyingTypeEnum OTHER { get { return new UnderlyingTypeEnum(OTHERValue); } }

		public static UnderlyingTypeEnum FromString(string str)
		{
		if (StockValue.Equals(str, StringComparison.InvariantCultureIgnoreCase))
		{
			return STOCK;
		}
		if (IndexValue.Equals(str, StringComparison.InvariantCultureIgnoreCase))
		{
			return INDEX;
		}
		if (CommodityValue.Equals(str, StringComparison.InvariantCultureIgnoreCase))
		{
			return COMMODITY;
		}
		if (FXValue.Equals(str, StringComparison.InvariantCultureIgnoreCase))
		{
			return FX;
		}
		return OTHER;
		}

        public override bool Equals(object obj)
        {
            return obj is UnderlyingTypeEnum @enum &&
                   Value.Equals(@enum.Value);
        }

        public override int GetHashCode()
        {
            return -1937169414 + Value.GetHashCode();
        }
    }
}

