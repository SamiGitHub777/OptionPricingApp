using CommonServiceLocator;
using GalaSoft.MvvmLight;
using MahApps.Metro.IconPacks;
using OptionPricingWPFClient.Models;
using System.Reflection;
using WPFHelper;

namespace OptionPricingWPFClient.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : HamburgerMenuItemViewModelBase
    {
        /// Initializes a new instance of the MainViewModel class.
        public MainViewModel()
        {
            Title = "Pricer";
            AppVersion = $"Version: {Assembly.GetExecutingAssembly().GetName().Version.ToString(3)}";
            Author = "CSharpTraining";
            CreateMenuItems();
        }
        
        private void CreateMenuItems()
        {
            AddHamburgerMenuIconItem("Home", "Home", PackIconMaterialKind.Home, ServiceLocator.Current.GetInstance<HomeViewModel>());
            AddHamburgerMenuIconItem("Options Pricing", "Options Pricing", PackIconMaterialKind.CurrencyEur, ServiceLocator.Current.GetInstance<OptionsPricingViewModel>());
            AddHamburgerMenuIconItem("Option List", "Option List", PackIconMaterialKind.Tools, ServiceLocator.Current.GetInstance<OptionsListViewModel>());
            AddHamburgerMenuIconItem("Information", "Information", PackIconMaterialKind.InformationOutline, ServiceLocator.Current.GetInstance<InformationViewModel>());
        }
    }
}