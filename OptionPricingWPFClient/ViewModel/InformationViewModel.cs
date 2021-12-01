using GalaSoft.MvvmLight.Command;
using System;
using System.Reflection;
using System.Windows.Input;

namespace OptionPricingWPFClient.ViewModel
{
    public class InformationViewModel : ViewModelBase
    {
        public static string AppVersion = $"{Assembly.GetExecutingAssembly().GetName().Version.ToString(3)}";
        public static string Title = "Option Pricing App";
        public static string Author = "Sami";
      
        public ICommand OpenHttpLinkCommand { get; }

        public InformationViewModel()
        {
            this.OpenHttpLinkCommand = new RelayCommand<object>(OnOpenHttpLinkCommand);
        }
        private void OnOpenHttpLinkCommand(object url)
        {
            try
            {
                System.Diagnostics.Process.Start(url as string);
            }
            catch (Exception)
            {
                // TODO: Error.
            }
        }
    }
}
