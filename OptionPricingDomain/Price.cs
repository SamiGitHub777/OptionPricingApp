using System;
using System.Collections.Generic;

namespace OptionPricingDomain
{
    public class Price
    {
        public double? PriceValue { get; private set; }
        public PricingModelEnum PricingModel { get; private set; }
        public Option OptionObj { get; private set; }


        public Price(double? priceValue, PricingModelEnum pricingModel, Option optionObj)
        {
            IsPriceValid(priceValue, pricingModel, optionObj);
            this.PriceValue = priceValue;
            this.PricingModel = pricingModel;
            this.OptionObj = optionObj;
        }

        private void IsPriceValid(double? price, PricingModelEnum pricingModel, Option option)
        {
            if (price < 0)
            {
                throw new ArgumentException("Price cannot be negative");
            }
            if (PricingModelEnum.UNKNOWN == pricingModel)
            {
                //throw new ArgumentException("Pricing model not allowed"); can be unknown if we have options inserted in db but not priced and we need to wrap this option in a price
            }
            if (option == null)
            {
                throw new ArgumentException("Cannot have price for null option");
            }
        }

        public override bool Equals(object obj)
        {
            return obj is Price price &&
                   PriceValue == price.PriceValue &&
                   PricingModel == price.PricingModel &&
                   OptionObj.Equals(price.OptionObj);
        }

        public override int GetHashCode()
        {
            int hashCode = -1027711010;
            hashCode = hashCode * -1521134295 + PriceValue.GetHashCode();
            hashCode = hashCode * -1521134295 + PricingModel.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<Option>.Default.GetHashCode(OptionObj);
            return hashCode;
        }

        public override string ToString()
        {
            return $"Price : {PriceValue}; Pricing method : {PricingModel}; Option : {OptionObj}";
        }
    }
}
