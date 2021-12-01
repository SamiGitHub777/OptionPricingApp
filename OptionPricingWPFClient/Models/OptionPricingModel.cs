using OptionPricingDomain;
using OptionPricingInfrastructure;
using OptionPricingWPFClient.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OptionPricingWPFClient.Models
{
    public interface IOptionPricingModel
    {
        PriceModel PriceOption(PriceModel priceModel);
        List<OptionModel> GetAllOptions();
        List<PriceModel> GetAllPrices();
    }
    public class OptionPricingModel : IOptionPricingModel
    {
        private readonly IOptionPricingTcpTransportManager optionPricingTcpTransportManager;
        private readonly IModelDomainConverter modelDomainConverter;

        public OptionPricingModel(IOptionPricingTcpTransportManager optionPricingTcpTransportManager, IModelDomainConverter modelDomainConverter)
        {
            this.optionPricingTcpTransportManager = optionPricingTcpTransportManager;
            this.modelDomainConverter = modelDomainConverter;
        }

        public PriceModel PriceOption(PriceModel priceModel)
        {
            Price price = optionPricingTcpTransportManager.PriceOption(modelDomainConverter.ToPriceDomain(priceModel));
            return new PriceModel(price.PriceValue, priceModel.PricingModel, priceModel.OptionObj);
        }

        public List<OptionModel> GetAllOptions()
        {
            return optionPricingTcpTransportManager.GetAllOption()
            .Select(x => modelDomainConverter.ToOptionModel(x))
            .ToList(); ;
        }

        public List<PriceModel> GetAllPrices()
        {
            return optionPricingTcpTransportManager.GetAllPrices()
            .Select(x => modelDomainConverter.ToPriceModel(x))
            .ToList(); ;
        }
    }
}
