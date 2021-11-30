using Unity;

namespace OptionPricingInfrastructure
{
    public interface IDependencyInjectionManager
    {
        void RegisterType<TInterface, TImplem>() where TImplem : TInterface;
        void RegisterInstance<T>(T instance);
        void RegisterTypeWithKey<TInterface, TImplem>(string key) where TImplem : TInterface;
        TInterface ResolveWithKey<TInterface>(string key);
        TInterface Resolve<TInterface>();
    }
    public class DependencyInjectionManager : IDependencyInjectionManager
    {
        private readonly IUnityContainer container;

        public DependencyInjectionManager()
        {
            this.container = new UnityContainer();
        }

        public void RegisterType<TInterface, TImplem>() where TImplem : TInterface 
        {
            container.RegisterType<TInterface, TImplem>();
        }

        public void RegisterInstance<T>(T instance)
        {
            container.RegisterInstance<T>(instance);
        }

        public void RegisterTypeWithKey<TInterface, TImplem>(string key) where TImplem : TInterface
        {
            container.RegisterType<TInterface, TImplem>(key);
        }

        public TInterface ResolveWithKey<TInterface>(string key)
        {
            return container.Resolve<TInterface>(key);
        }

        public TInterface Resolve<TInterface>()
        {
            return container.Resolve<TInterface>();
        }
    }
}
