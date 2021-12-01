using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace OptionPricingWPFClient.ViewModel
{
    public class InformationViewModel : ViewModelBase
    {
        public static string AppVersion = $"{Assembly.GetExecutingAssembly().GetName().Version.ToString(3)}";
        public static string Title = "Option Pricing App";
        public static string Author = "Sami";
        public static string GitHubUrl = "https://github.com/DIGISTRAT-Team/OptionPricingApp";

        public ICommand AccessGitHub { get; set; }

        public InformationViewModel()
        {
            this.AccessGitHub = new RelayCommand(OnClickAccessGitHubCommand);

        }

        private void OnClickAccessGitHubCommand()
        {
            System.Diagnostics.Process.Start(GitHubUrl);
        }
    }
}
