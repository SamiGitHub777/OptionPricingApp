using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static OptionPricingWPFClient.ViewModel.OptionsPricingViewModel;

namespace OptionPricingWPFClient.Models
{
    public class OptionModel
    {
        public OptionTypeEnum OptionType { get; private set; }
        public DateTime? Maturity { get; private set; }
        public double Strike { get; private set; }
        public double RiskFreeRate { get; private set; }
        public UnderlyingModel UnderlyingObj { get; private set; }

        public OptionModel(OptionTypeEnum optionTypeEnum, DateTime? maturity, double strike, double riskFreeRate, UnderlyingModel underlyingObj)
        {
            OptionType = optionTypeEnum;
            Maturity = maturity;
            Strike = strike;
            RiskFreeRate = riskFreeRate;
            UnderlyingObj = underlyingObj;
        }

        public override bool Equals(object obj)
        {
            return obj is OptionModel option &&
                   OptionType.Equals(option.OptionType) &&
                   Maturity.Equals(option.Maturity) &&
                   Strike == option.Strike &&
                   RiskFreeRate == option.RiskFreeRate &&
                   UnderlyingObj.Equals(option.UnderlyingObj);
        }


        public override string ToString()
        {
            return $"Option type : {OptionType}; Maturity : {Maturity}; Strike : {Strike}; Risk free rate : {RiskFreeRate}; Underlying : {UnderlyingObj}";
        }

        public override int GetHashCode()
        {
            int hashCode = -411581654;
            hashCode = hashCode * -1521134295 + OptionType.GetHashCode();
            hashCode = hashCode * -1521134295 + Maturity.GetHashCode();
            hashCode = hashCode * -1521134295 + Strike.GetHashCode();
            hashCode = hashCode * -1521134295 + RiskFreeRate.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<UnderlyingModel>.Default.GetHashCode(UnderlyingObj);
            return hashCode;
        }
    }
}
