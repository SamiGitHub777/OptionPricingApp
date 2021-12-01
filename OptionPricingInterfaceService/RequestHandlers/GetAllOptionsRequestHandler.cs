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
    public class GetAllOptionsRequestHandler : IRequestHandler
    {
        private readonly IOptionPricingJsonSerializer<List<Option>> serializerOptionList;
        private readonly IOptionPricingPersistenceService optionPricingPersistenceService;

        public GetAllOptionsRequestHandler(IOptionPricingJsonSerializer<List<Option>> serializerOptionList, IOptionPricingPersistenceService optionPricingPersistenceService)
        {
            this.serializerOptionList = serializerOptionList;
            this.optionPricingPersistenceService = optionPricingPersistenceService;
        }
        public string HandleRequest(string message, IDependencyInjectionManager dependencyInjectionManager)
        {
            List<Option> optionList = optionPricingPersistenceService.GetAllOptions();
            return serializerOptionList.Serialize(optionList);
        }
    }
}
