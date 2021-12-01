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
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    public class ViewModelLocator
    {
        public HomeViewModel Home => ServiceLocator.Current.GetInstance<HomeViewModel>();
        public OptionsPricingViewModel OptionsPricing => ServiceLocator.Current.GetInstance<OptionsPricingViewModel>();
        public OptionsListViewModel OptionsList => ServiceLocator.Current.GetInstance<OptionsListViewModel>();
        public PricesListViewModel PricesList => ServiceLocator.Current.GetInstance<PricesListViewModel>();
        public InformationViewModel Information => ServiceLocator.Current.GetInstance<InformationViewModel>();


        /// Initializes a new instance of the ViewModelLocator class.
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);
            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<HomeViewModel>();
            SimpleIoc.Default.Register<OptionsPricingViewModel>();
            SimpleIoc.Default.Register<IOptionPricingModel, OptionPricingModel>();
            SimpleIoc.Default.Register<OptionsListViewModel>();
            SimpleIoc.Default.Register<PricesListViewModel>();
            SimpleIoc.Default.Register<InformationViewModel>();
            SimpleIoc.Default.Register<IModelArgsValidator, ModelArgsValidator>();
            SimpleIoc.Default.Register<IOptionPricingJsonSerializer<Price>, OptionPricingJsonSerializer<Price>>();
            SimpleIoc.Default.Register<IOptionPricingJsonSerializer<List<Option>>, OptionPricingJsonSerializer<List<Option>>>();
            SimpleIoc.Default.Register<IOptionPricingJsonSerializer<List<Price>>, OptionPricingJsonSerializer<List<Price>>>();
            SimpleIoc.Default.Register<IOptionPricingTcpTransportManager>(() => new OptionPricingTcpTransportManager("localhost", 5555,
                ServiceLocator.Current.GetInstance<IOptionPricingJsonSerializer<Price>>(),
                ServiceLocator.Current.GetInstance<IOptionPricingJsonSerializer<List<Option>>>(),
                ServiceLocator.Current.GetInstance<IOptionPricingJsonSerializer<List<Price>>>()));
            SimpleIoc.Default.Register<IModelDomainConverter, ModelDomainConverter>();
        }

        public MainViewModel Main
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MainViewModel>();
            }
        }

        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }
    }
}