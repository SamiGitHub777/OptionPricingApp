using NetMQ;
using OptionPricingDomain;
using OptionPricingDomainService;
using OptionPricingInfrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptionPricingInterfaceService.RequestHandlers
{
    public class PriceOptionRequestHandler : IRequestHandler
    {
        private readonly IOptionPricingJsonSerializer<Price> serializerPrice;
        private readonly IOptionPricingPersistenceService optionPricingPersistenceService;

        public PriceOptionRequestHandler(IOptionPricingJsonSerializer<Price> serializerPrice, IOptionPricingPersistenceService optionPricingPersistenceService)
        {
            this.serializerPrice = serializerPrice;
            this.optionPricingPersistenceService = optionPricingPersistenceService;
        }
        public string HandleRequest(string message, IDependencyInjectionManager dependencyInjectionManager)
        {
            Price price = serializerPrice.Deserialize(message);
            optionPricingPersistenceService.InsertOption(price.OptionObj);
            PricingModelEnum pricingModel = price.PricingModel;
            IOptionPricingMethodService pricer = dependencyInjectionManager.ResolveWithKey<IOptionPricingMethodService>(pricingModel.ToString());
            Price priceComputed = new Price(pricer.Price(price.OptionObj), price.PricingModel, price.OptionObj);
            optionPricingPersistenceService.InsertPrice(priceComputed);
            return serializerPrice.Serialize(priceComputed);
        }
    }
}
