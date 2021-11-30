using OptionPricingDomain;

namespace OptionPricingDomainService
{
    public interface IOptionPricingMethodService
    {
        double Price(Option option);
    }
}
