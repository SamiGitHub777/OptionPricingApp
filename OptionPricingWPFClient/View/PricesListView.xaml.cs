using OptionPricingWPFClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace OptionPricingWPFClient.View
{
    /// <summary>
    /// Interaction logic for PricesList.xaml
    /// </summary>
    public partial class PricesListView : UserControl
    {
        public PricesListView()
        {
            InitializeComponent();
        }

        private void HandlePreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (!e.Handled)
            {
                e.Handled = true;
                var eventArg = new MouseWheelEventArgs(e.MouseDevice, e.Timestamp, e.Delta);
                eventArg.RoutedEvent = UIElement.MouseWheelEvent;
                eventArg.Source = sender;
                var parent = ((Control)sender).Parent as UIElement;
                parent.RaiseEvent(eventArg);
            }
        }

        private void DeleteCategory(object sender, RoutedEventArgs e)  // TODO : should be on PricesListViewModel
        {
            if (MessageBox.Show("Are you sure you want to delete this option and its price ?", "Confirm",
                   MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                Button b = sender as Button;
                PriceModel priceModel = b.CommandParameter as PriceModel;
                MessageBox.Show(priceModel.PricingModel.ToString()); // TODO
            }
        }
    }
}
