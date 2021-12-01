using GalaSoft.MvvmLight.Command;
using OptionPricingWPFClient.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace OptionPricingWPFClient.ViewModel
{
    public class OptionsListViewModel : ViewModelBase
    {
        private ObservableCollection<OptionModel> itemsCollection = new ObservableCollection<OptionModel>();
        public ObservableCollection<OptionModel> ItemsCollection
        {
            get
            {
                if (itemsCollection == null)
                {
                    itemsCollection = new ObservableCollection<OptionModel>();
                }
                return itemsCollection;
            }
            set
            {
                itemsCollection = value;
            }
        }


        public ICommand RefreshCommand { get; set; }
        private readonly IOptionPricingModel optionPricingModel;


        public OptionsListViewModel(IOptionPricingModel optionPricingModel)
        {
            this.optionPricingModel = optionPricingModel;
            this.RefreshCommand = new RelayCommand(OnClickRefreshCommand);

        }

        public void OnClickRefreshCommand()
        {
            UpdateSectionsList(optionPricingModel.GetAllOptions());
        }

        private void UpdateSectionsList(List<OptionModel> list)
        {
            // clear the list before and reload the object you want to display
            itemsCollection.Clear();
            if (list.Count > 0)
            {
                foreach (OptionModel item in list)
                {
                    itemsCollection.Add(item);
                }
            }
        }

    }
}
