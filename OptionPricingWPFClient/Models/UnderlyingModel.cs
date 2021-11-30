using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static OptionPricingWPFClient.ViewModel.OptionsPricingViewModel;

namespace OptionPricingWPFClient.Models
{
    public class UnderlyingModel
    {
        public string UnderlyingName { get; private set; }
        public double Spot { get; private set; }
        public UnderlyingTypeEnum UnderlyingType { get; private set; }
        public double Volatility { get; private set; }

        public UnderlyingModel(string underlyingName, double spot, UnderlyingTypeEnum underlyingType, double volatility)
        {
            UnderlyingName = underlyingName;
            Spot = spot;
            UnderlyingType = underlyingType;
            Volatility = volatility;
        }
    }
}
