
namespace C_sharp_training.Common.OptionPricing.MonteCarlo
{
    public interface IDiscretizationMonteCarlo
    {
        string getName();
        double increment(double value);
    }
}
