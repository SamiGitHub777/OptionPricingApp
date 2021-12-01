using OptionPricingDomain;
using OptionPricingDomainService;
using OptionPricingInfrastructure;
using System.Collections.Generic;


namespace OptionPricingInterfaceService.RequestHandlers
{
    public class GetAllPricesRequestHandler : IRequestHandler
    {
        private readonly IOptionPricingJsonSerializer<List<Price>> serializerPriceList;
        private readonly IOptionPricingPersistenceService optionPricingPersistenceService;

        public GetAllPricesRequestHandler(IOptionPricingJsonSerializer<List<Price>> serializerPriceList, IOptionPricingPersistenceService optionPricingPersistenceService)
        {
            this.serializerPriceList = serializerPriceList;
            this.optionPricingPersistenceService = optionPricingPersistenceService;
        }
        public string HandleRequest(string message, IDependencyInjectionManager dependencyInjectionManager)
        {
            List<Price> priceList = optionPricingPersistenceService.GetAllPrices();
            return serializerPriceList.Serialize(priceList);
        }
    }
}
