using OptionPricingDAO.Common;
using System;
using System.Text;

namespace OptionPricingDAO.DTOs
{
    public class OptionParametersDTO
    {
		private ContractEnum optionType;
		private double strike;
		private double riskFreeRate;
		private DateTime maturity;
		private double volatility;
		private string underlying;
		private double spot;
		private UnderlyingTypeEnum underlyingType;
		public OptionParametersDTO(ContractEnum optionType, double strike, double riskFreeRate, DateTime maturity,
			double volatility, string underlying, double spot, UnderlyingTypeEnum underlyingType)
        {
			this.optionType = optionType;
			this.strike = strike;
			this.riskFreeRate = riskFreeRate;
			this.maturity = maturity;
			this.volatility = volatility;
			this.underlying = underlying;
			this.spot = spot;
			this.underlyingType = underlyingType;
        }
        public ContractEnum OptionType {
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
		public UnderlyingTypeEnum UnderlyingType {
			get { return this.underlyingType; }
			set { this.underlyingType = value; }
		}

        public override bool Equals(object obj)
        {
			return obj is OptionParametersDTO dTO &&
				   optionType.Equals(dTO.optionType) &&
				   strike == dTO.strike &&
				   riskFreeRate == dTO.riskFreeRate &&
				   maturity.CompareTo(dTO.maturity) == 0 &&
				   volatility == dTO.volatility &&
				   underlying.Equals(dTO.underlying) &&
				   spot == dTO.spot &&
				   underlyingType.Equals(dTO.UnderlyingType);
        }

        public override int GetHashCode()
        {
            int hashCode = -594186788;
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

        public override string ToString()
		{
			return new StringBuilder().Append("Option type ")
					.Append(optionType.Value)
					.Append(" Strike ")
					.Append(strike)
					.Append(" Risk free rate ")
					.Append(riskFreeRate)
					.Append(" Maturity ")
					.Append(maturity)
					.Append(" Volatility ")
					.Append(volatility)
					.Append(" Underlying ")
					.Append(underlying)
					.Append(" Spot ")
					.Append(spot)
					.Append(" Underlying type ")
					.Append(underlyingType.Value).ToString();
		}
	}
}
