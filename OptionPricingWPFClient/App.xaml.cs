using OptionPricingWPFClient.Models;
using OptionPricingWPFClient.ViewModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace OptionPricingWPFClient
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly PriceModel price;

        public App()
        {
            /*
            Underlying underlying = new Underlying("FR_BNP", 150, OptionsPricingViewModel.UnderlyingTypeEnum.STOCK, 0.3);
            Option option = new Option(OptionsPricingViewModel.OptionTypeEnum.AmericanCall, new DateTime(), 100, 0.1, underlying);
            price = new Price(999, OptionsPricingViewModel.PricingModelEnum.BlackScholes, option);
            */
        }

        protected override void OnStartup(StartupEventArgs e)
        {/*
            MainWindow = new MainWindow()
            {
                DataContext = new MainViewModel(price)
            };
            MainWindow.Show();
            base.OnStartup(e);
            */
        }
    }
}
