using OptionPricingDomain;
using OptionPricingInfrastructure;
using OptionPricingWPFClient.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptionPricingWPFClient.Models
{
    public interface IOptionPricingModel
    {
        PriceModel PriceOption(PriceModel priceModel);
        List<OptionModel> GetAllOptions();
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
            // TODO
            //List<OptionModel> res = new List<OptionModel>();
            //res.Add(new OptionModel(OptionsPricingViewModel.OptionTypeEnum.AmericanCall,
            //   DateTime.Now, 0, 0, new UnderlyingModel("fromBackEnd", 0, OptionsPricingViewModel.UnderlyingTypeEnum.STOCK, 0)));
            return optionPricingTcpTransportManager.GetAllOption()
            .Select(x => modelDomainConverter.ToOptionModel(x))
            .ToList(); ;
        }
    }
}
