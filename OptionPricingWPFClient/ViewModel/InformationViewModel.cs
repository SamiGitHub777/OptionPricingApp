using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace OptionPricingWPFClient.ViewModel
{
    public class InformationViewModel : ViewModelBase
    {
        public static string AppVersion = $"{Assembly.GetExecutingAssembly().GetName().Version.ToString(3)}";
        public static string Title = "Option Pricing App";
        public static string Author = "Sami";

    }
}
