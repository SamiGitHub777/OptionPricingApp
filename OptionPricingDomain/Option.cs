using System;
using System.Collections.Generic;

namespace OptionPricingDomain
{
    public class Option
    {
        public ContractEnum OptionType { get; private set; }
        public Maturity Maturity { get; private set; }
        public double Strike { get; private set; }
        public double RiskFreeRate { get; private set; }
        public Underlying UnderlyingObj { get; private set; }


        public Option(ContractEnum optionType, Maturity maturity, double strike, double riskFreeRate, Underlying underlyingObj)
        {
            IsOptionValid(optionType, strike, maturity, underlyingObj);
            this.OptionType = optionType;
            this.Maturity = maturity;
            this.Strike = strike;
            this.RiskFreeRate = riskFreeRate;
            this.UnderlyingObj = underlyingObj;
        }

        private void IsOptionValid(ContractEnum contractType, double strike, Maturity maturity, Underlying udl)
        {
            if (udl == null || maturity == null)
            {
                throw new ArgumentException("Option underlying or maturity shouldn't be null");
            }
            if (ContractEnum.UNKNOWN == contractType)
            {
                throw new ArgumentException("Option type is not allowed");
            }
            if (strike <= 0)
            {
                throw new ArgumentException("Option strike cannot be null or negative");
            }
        }

        public override bool Equals(object obj)
        {
            return obj is Option option &&
                   OptionType.Equals(option.OptionType) &&
                   Maturity.Equals(option.Maturity) &&
                   Strike == option.Strike &&
                   RiskFreeRate == option.RiskFreeRate &&
                   UnderlyingObj.Equals(option.UnderlyingObj);
        }

        public override int GetHashCode()
        {
            int hashCode = -411581654;
            hashCode = hashCode * -1521134295 + OptionType.GetHashCode();
            hashCode = hashCode * -1521134295 + Maturity.GetHashCode();
            hashCode = hashCode * -1521134295 + Strike.GetHashCode();
            hashCode = hashCode * -1521134295 + RiskFreeRate.GetHashCode();
            hashCode = hashCode * -1521134295 + UnderlyingObj.GetHashCode();
            return hashCode;
        }

        public override string ToString()
        {
            return $"Option type : {OptionType}; Maturity : {Maturity}; Strike : {Strike}; Risk free rate : {RiskFreeRate}; Underlying : {UnderlyingObj}";
        }

        public Option DummyOption()
        {
            var rnd = new Random();
            Strike = Strike * rnd.NextDouble();
            RiskFreeRate = RiskFreeRate * rnd.NextDouble();
            return this;
        }
    }
}
