/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocator xmlns:vm="clr-namespace:OptionPricingWPFClient"
                           x:Key="Locator" />
  </Application.Resources>
  
  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"

  You can also use Blend to do all this with the tool's support.
  See http://www.galasoft.ch/mvvm
*/

using CommonServiceLocator;
using GalaSoft.MvvmLight.Ioc;
using OptionPricingDomain;
using OptionPricingInfrastructure;
using OptionPricingWPFClient.Models;
using System;
using System.Collections.Generic;

namespace OptionPricingWPFClient.ViewModel
{
    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// </summary>
    public class ViewModelLocator
    {
        public HomeViewModel Home => ServiceLocator.Current.GetInstance<HomeViewModel>();
        public OptionsPricingViewModel OptionsPricing => ServiceLocator.Current.GetInstance<OptionsPricingViewModel>();
        public OptionsListViewModel OptionsList => ServiceLocator.Current.GetInstance<OptionsListViewModel>();
        public InformationViewModel Information => ServiceLocator.Current.GetInstance<InformationViewModel>();
        

        /// <summary>
        /// Initializes a new instance of the ViewModelLocator class.
        /// </summary>
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            ////if (ViewModelBase.IsInDesignModeStatic)
            ////{
            ////    // Create design time view services and models
            ////    SimpleIoc.Default.Register<IDataService, DesignDataService>();
            ////}
            ////else
            ////{
            ////    // Create run time view services and models
            ////    SimpleIoc.Default.Register<IDataService, DataService>();
            ////}

            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<HomeViewModel>();
            SimpleIoc.Default.Register<OptionsPricingViewModel>();
            SimpleIoc.Default.Register<IOptionPricingModel, OptionPricingModel>();
            SimpleIoc.Default.Register<OptionsListViewModel>();
            SimpleIoc.Default.Register<InformationViewModel>();
            SimpleIoc.Default.Register<IModelArgsValidator, ModelArgsValidator>();
            SimpleIoc.Default.Register<IOptionPricingJsonSerializer<Price>, OptionPricingJsonSerializer<Price>>();
            SimpleIoc.Default.Register<IOptionPricingJsonSerializer<List<Option>>, OptionPricingJsonSerializer<List<Option>>>();
            SimpleIoc.Default.Register<IOptionPricingTcpTransportManager>(() => new OptionPricingTcpTransportManager("localhost", 5555, 
                ServiceLocator.Current.GetInstance<IOptionPricingJsonSerializer<Price>>(),
                ServiceLocator.Current.GetInstance<IOptionPricingJsonSerializer<List<Option>>>()));
            SimpleIoc.Default.Register<IModelDomainConverter, ModelDomainConverter>();
        }

        public MainViewModel Main
        {
            get
            {
                /*
                Underlying underlying = new Underlying("FR_BNP", 150, OptionsPricingViewModel.UnderlyingTypeEnum.STOCK, 0.3);
                Option option = new Option(OptionsPricingViewModel.OptionTypeEnum.AmericanCall, new DateTime(), 100, 0.1, underlying);
                Price price = new Price(999, OptionsPricingViewModel.PricingModelEnum.BlackScholes, option);
                return new MainViewModel(price);
                */
                return ServiceLocator.Current.GetInstance<MainViewModel>();
            }
        }
        
        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }
    }
}