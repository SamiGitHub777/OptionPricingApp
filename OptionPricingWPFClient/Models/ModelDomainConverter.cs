using OptionPricingDomain;
using OptionPricingWPFClient.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static OptionPricingWPFClient.ViewModel.OptionsPricingViewModel;

namespace OptionPricingWPFClient.Models
{
    public interface IModelDomainConverter
    {
        PriceModel ToPriceModel(Price price);
        Price ToPriceDomain(PriceModel priceModel);

        Option ToOptionDomain(OptionModel optionModel);

        OptionModel ToOptionModel(Option option);
    }

    public class ModelDomainConverter : IModelDomainConverter
    {
        public Option ToOptionDomain(OptionModel optionModel)
        {

            return new Option(ConvertEnum<ContractEnum>(optionModel.OptionType.ToString()), ToDomainMaturity(optionModel), optionModel.Strike, optionModel.RiskFreeRate, ToDomainUnderlying(optionModel));
        }

        public OptionModel ToOptionModel(Option option)
        {
            DateTime? maturity = new DateTime(option.Maturity.Year, option.Maturity.Month, option.Maturity.Day);
            UnderlyingModel udl = new UnderlyingModel(option.UnderlyingObj.UnderlyingName, option.UnderlyingObj.Spot, ConvertEnum<OptionsPricingViewModel.UnderlyingTypeEnum>(option.UnderlyingObj.UnderlyingType.ToString()), option.UnderlyingObj.Volatility);
            return new OptionModel(ConvertEnum<OptionTypeEnum>(option.OptionType.ToString()), maturity, option.Strike, option.RiskFreeRate, udl);
        }

        public Price ToPriceDomain(PriceModel priceModel)
        {
            return new Price(priceModel.PriceValue, ConvertEnum<OptionPricingDomain.PricingModelEnum>(priceModel.PricingModel.ToString()), ToOptionDomain(priceModel.OptionObj));
        }

        public PriceModel ToPriceModel(Price price)
        {
            return new PriceModel(price.PriceValue, ConvertEnum<OptionsPricingViewModel.PricingModelEnum>(price.PricingModel.ToString()), ToOptionModel(price.OptionObj));
        }

        private Maturity ToDomainMaturity(OptionModel optionModel)
        {
            return new Maturity(optionModel.Maturity.Value.Year, optionModel.Maturity.Value.Month, optionModel.Maturity.Value.Day); // null maturity is TODAY
        }

        private Underlying ToDomainUnderlying(OptionModel optionModel)
        {
            return new Underlying(optionModel.UnderlyingObj.UnderlyingName, optionModel.UnderlyingObj.Spot, ConvertEnum<OptionPricingDomain.UnderlyingTypeEnum>(optionModel.UnderlyingObj.UnderlyingType.ToString()), optionModel.UnderlyingObj.Volatility);
        }

        private T ConvertEnum<T>(string name) where T : struct
        {
            if (Enum.TryParse<T>(name, out T result))
            {
                return result;
            }
            else
            {
                throw new ArgumentException("Unable to convert Model Enum to Domain Enum");
            }
        }

    }
}
