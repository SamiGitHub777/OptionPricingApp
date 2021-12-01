using OptionPricingDAO.Common;
using System;

namespace OptionPricingDAO.DTOs
{
    public class PriceDTO
    {
        private double? price;
		private PricingModelEnum model;
		private ContractEnum optionType;
		private double strike;
		private double riskFreeRate;
		private DateTime maturity;
		private double volatility;
		private string underlying;
		private double spot;
		private UnderlyingTypeEnum underlyingType;

        public double? Price {
			get { return this.price; }
			set { this.price = value; }
		}
		public PricingModelEnum Model {
			get { return this.model; }
			set { this.model = value; }
		}
		public ContractEnum ContractType {
			get { return this.optionType; }
			set { this.optionType = value; }
		}
		public double Strike {
			get { return this.strike; }
			set { this.strike = value; }
		}
		public double RiskFreeRate {
			get { return this.riskFreeRate; }
			set { this.riskFreeRate = value; }
		}
		public DateTime Maturity {
			get { return this.maturity; }
			set { this.maturity = value; }
		}
		public double Volatility {
			get { return this.volatility; }
			set { this.volatility = value; }
		}
		public string Underlying {
			get { return this.underlying; }
			set { this.underlying = value; }
		}
		public double Spot {
			get { return this.spot; }
			set { this.spot = value; }
		}
		public UnderlyingTypeEnum UnderlyingType
		{
			get { return this.underlyingType; }
			set { this.underlyingType = value; }
		}

		public PriceDTO(double? price, PricingModelEnum model, ContractEnum optionType, double strike, double riskFreeRate, DateTime maturity,
		double volatility, string underlying, double spot, UnderlyingTypeEnum udlType)
        {
			this.price = price;
			this.model = model;
			this.optionType = optionType;
			this.strike = strike;
			this.riskFreeRate = riskFreeRate;
			this.maturity = maturity;
			this.volatility = volatility;
			this.underlying = underlying;
			this.spot = spot;
			this.underlyingType = udlType;
        }

		public PriceDTO(OptionParametersDTO optionParametersDTO, double? price, PricingModelEnum model)
        {
			this.price = price;
			this.model = model;
			this.optionType = optionParametersDTO.OptionType;
			this.strike = optionParametersDTO.Strike;
			this.riskFreeRate = optionParametersDTO.RiskFreeRate;
			this.maturity = optionParametersDTO.Maturity;
			this.volatility = optionParametersDTO.Volatility;
			this.underlying = optionParametersDTO.Underlying;
			this.spot = optionParametersDTO.Spot;
			this.underlyingType = optionParametersDTO.UnderlyingType;
		}

        public override bool Equals(object obj)
        {
            return obj is PriceDTO dTO &&
                   price == dTO.price &&
                   model.Equals(dTO.model) &&
                   optionType.Equals(dTO.optionType) &&
                   strike == dTO.strike &&
                   riskFreeRate == dTO.riskFreeRate &&
                   maturity.CompareTo(dTO.maturity) == 0 &&
                   volatility == dTO.volatility &&
                   underlying == dTO.underlying &&
                   spot == dTO.spot &&
                   underlyingType.Equals(dTO.underlyingType);
        }

        public override int GetHashCode()
        {
            int hashCode = 1808784971;
            hashCode = hashCode * -1521134295 + price.GetHashCode();
            hashCode = hashCode * -1521134295 + model.GetHashCode();
            hashCode = hashCode * -1521134295 + optionType.GetHashCode();
            hashCode = hashCode * -1521134295 + strike.GetHashCode();
            hashCode = hashCode * -1521134295 + riskFreeRate.GetHashCode();
            hashCode = hashCode * -1521134295 + maturity.GetHashCode();
            hashCode = hashCode * -1521134295 + volatility.GetHashCode();
            hashCode = hashCode * -1521134295 + underlying.GetHashCode();
            hashCode = hashCode * -1521134295 + spot.GetHashCode();
            hashCode = hashCode * -1521134295 + underlyingType.GetHashCode();
            return hashCode;
        }
    }
}
