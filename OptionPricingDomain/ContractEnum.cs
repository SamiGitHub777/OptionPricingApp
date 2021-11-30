using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptionPricingDomain
{
    public enum ContractEnum
    {
        UNKNOWN,
        EuropeanCall,
        EuropeanPut,
        AmericanCall,
        AmericanPut
    }
}
