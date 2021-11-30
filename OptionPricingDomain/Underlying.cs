using Newtonsoft.Json;
using System;
namespace OptionPricingDomain
{
    public class Underlying
    {
        public string UnderlyingName { get; private set; }
        public double Spot { get; private set; }
        public UnderlyingTypeEnum UnderlyingType { get; private set; }
        public double Volatility { get; private set; }

        [JsonConstructor]
        public Underlying(string underlyingName, double spot, UnderlyingTypeEnum underlyingType, double volatility)
        {
            IsUnderlyingValid(underlyingName, spot, underlyingType, volatility);
            this.UnderlyingName = underlyingName;
            this.Spot = spot;
            this.UnderlyingType = underlyingType;
            this.Volatility = volatility;
        }

        private void IsUnderlyingValid(string underlying, double spot, UnderlyingTypeEnum underlyingType, double vol)
        {
            if (spot <= 0)
            {
                throw new ArgumentException("Spot cannot be null or negative");
            }
            if (UnderlyingTypeEnum.UNKNOWN == underlyingType)
            {
                throw new ArgumentException("Underlying type doesn't exist");
            }
            if (vol <= 0)
            {
                throw new ArgumentException("Volatility cannot be null or negative");
            }
        }

        public override bool Equals(object that)
        {
            if (that == null)
                return false;

            if (ReferenceEquals(that, this))
                return false;

            if (that.GetType() != this.GetType())
                return false;

            return that is Underlying @udl
                && this.UnderlyingName.Equals(@udl.UnderlyingName)
                && this.Spot == @udl.Spot
                && this.UnderlyingType.Equals(@udl.UnderlyingType)
                && this.Volatility == @udl.Volatility;
        }
        public override int GetHashCode()
        {
            return this.UnderlyingName.GetHashCode()
                 ^ this.Spot.GetHashCode()
                 ^ this.UnderlyingType.GetHashCode()
                 ^ this.Volatility.GetHashCode();
        }

        public override string ToString()
        {
            return $"Underlying : {UnderlyingName}; Spot : {Spot}; Underlying type : {UnderlyingType}; Volatility : {Volatility}";
        }
    }
}
