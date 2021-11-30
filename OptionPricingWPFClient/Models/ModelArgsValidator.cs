using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static OptionPricingWPFClient.ViewModel.OptionsPricingViewModel;

namespace OptionPricingWPFClient.Models
{
    public interface IModelArgsValidator
    {
        bool IsDoubleValid(double spot);
        bool IsStringValid(string name);
        bool IsOptionMatuValid(DateTime? matu);

    }
    public class ModelArgsValidator : IModelArgsValidator
    {
        public bool IsDoubleValid(double d)
        {
            return d != 0;
        }
        public bool IsStringValid(string name)
        {
            return !string.IsNullOrEmpty(name);
        }
        public bool IsOptionMatuValid(DateTime? matu)
        {
            return DateTime.Now.CompareTo(matu) <= 0;
        }
    }
}
