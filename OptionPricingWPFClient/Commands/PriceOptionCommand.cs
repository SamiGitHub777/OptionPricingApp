using OptionPricingWPFClient.Models;
using OptionPricingWPFClient.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptionPricingWPFClient.Commands
{
    public class PriceOptionCommand : CommandBase
    {
        private readonly OptionsPricingViewModel optionsPricingViewModel;
        private readonly PriceModel price;

        public PriceOptionCommand(OptionsPricingViewModel optionsPricingViewModel, PriceModel price)
        {
            this.optionsPricingViewModel = optionsPricingViewModel;
            this.price = price;

            this.optionsPricingViewModel.PropertyChanged += OnViewModelPropertyChanged;
        }
        public override void Execute(object parameter)
        {

        }

        public override bool CanExecute(object parameter)
        {
            return price.OptionObj.Maturity != null && price.OptionObj.UnderlyingObj != null && base.CanExecute(parameter);
        }

        private void OnViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(OptionsPricingViewModel.Maturity) 
                || e.PropertyName == nameof(OptionsPricingViewModel.UnderlyingName)
                || e.PropertyName == nameof(OptionsPricingViewModel.UnderlyingSpot)
                || e.PropertyName == nameof(OptionsPricingViewModel.UnderlyingType)
                || e.PropertyName == nameof(OptionsPricingViewModel.UnderlyingVol))
            {
                OnCanExecutedChanged();
            }
        }
    }
}
