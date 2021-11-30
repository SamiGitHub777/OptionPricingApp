using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static OptionPricingWPFClient.ViewModel.OptionsPricingViewModel;

namespace OptionPricingWPFClient.Models
{
    public class PriceModel
    {
        public double PriceValue { get; private set; }
        public PricingModelEnum PricingModel { get; private set; }
        public OptionModel OptionObj { get; private set; }


        public PriceModel(double priceValue, PricingModelEnum pricingModel, OptionModel optionObj)
        {
            this.PriceValue = priceValue;
            this.PricingModel = pricingModel;
            this.OptionObj = optionObj;
        }
    }
}
