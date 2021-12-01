using GalaSoft.MvvmLight.Command;
using OptionPricingWPFClient.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace OptionPricingWPFClient.ViewModel
{
    public class PricesListViewModel : ViewModelBase
    {


        private ObservableCollection<PriceModel> itemsCollection = new ObservableCollection<PriceModel>();
        public ObservableCollection<PriceModel> ItemsCollection
        {
            get
            {
                if (itemsCollection == null)
                {
                    itemsCollection = new ObservableCollection<PriceModel>();
                }
                return itemsCollection;
            }
            set
            {
                itemsCollection = value;
            }
        }


        public ICommand DeleteCommand { get; set; }
        public ICommand RefreshCommand { get; set; }
        private readonly IOptionPricingModel optionPricingModel;


        public PricesListViewModel(IOptionPricingModel optionPricingModel)
        {
            this.optionPricingModel = optionPricingModel;
            this.RefreshCommand = new RelayCommand(OnClickRefreshCommand);
            this.DeleteCommand = new RelayCommand(OnClickDeleteCommand);

        }

        public void OnClickRefreshCommand()
        {
            UpdateSectionsList(optionPricingModel.GetAllPrices());
        }

        public void OnClickDeleteCommand() // TODO
        {
            /*
            if (MessageBox.Show("Are you sure you want to delete this option and its price ?", "Confirm",
                   MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                Button b = sender as Button;
                PriceModel priceModel = b.CommandParameter as PriceModel;
                //MessageBox.Show(priceModel.PricingModel.ToString());
            }
            UpdateSectionsList(optionPricingModel.GetAllPrices());
            */
        }

        private void UpdateSectionsList(List<PriceModel> list)
        {
            // clear the list before and reload the object you want to display
            itemsCollection.Clear();
            if (list.Count > 0)
            {
                foreach (PriceModel item in list)
                {
                    itemsCollection.Add(item);
                }
            }
        }

    }
}