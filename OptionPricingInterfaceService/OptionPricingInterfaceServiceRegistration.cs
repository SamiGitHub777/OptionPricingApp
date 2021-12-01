using OptionPricingDAO;
using OptionPricingDomain;
using OptionPricingDomainService;
using OptionPricingInfrastructure;
using OptionPricingRepository;
using System.Collections.Generic;

namespace OptionPricingInterfaceService
{
    public class OptionPricingInterfaceServiceRegistration
    {
        public IDependencyInjectionManager DependencyInjectionManager { get; private set; }
        public OptionPricingInterfaceServiceRegistration()
        {
            this.DependencyInjectionManager = new DependencyInjectionManager();
        }

        public void Register()
        {
            DependencyInjectionManager.RegisterType<IOptionPricingJsonSerializer<Price>, OptionPricingJsonSerializer<Price>>();
            DependencyInjectionManager.RegisterType<IOptionPricingJsonSerializer<List<Option>>, OptionPricingJsonSerializer<List<Option>>>();
            DependencyInjectionManager.RegisterType<IOptionPricingJsonSerializer<List<Price>>, OptionPricingJsonSerializer<List<Price>>>();
            DependencyInjectionManager.RegisterType<IOptionDAO, OptionDAO>();
            DependencyInjectionManager.RegisterType<IOptionRepository, OptionRepository>();
            DependencyInjectionManager.RegisterType<IOptionPricingPersistenceService, OptionPricingPersistenceService>();
            DependencyInjectionManager.RegisterTypeWithKey<IOptionPricingMethodService, OptionPricingBinomialTreeService>(PricingModelEnum.BinomialTree.ToString());
            DependencyInjectionManager.RegisterTypeWithKey<IOptionPricingMethodService, OptionPricingBlackScholesService>(PricingModelEnum.BlackScholes.ToString());
            DependencyInjectionManager.RegisterTypeWithKey<IOptionPricingMethodService, OptionPricingMonteCarloService>(PricingModelEnum.MonteCarlo.ToString());
        }
    }
}
