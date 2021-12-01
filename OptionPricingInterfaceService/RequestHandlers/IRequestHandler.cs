using NetMQ;
using OptionPricingInfrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptionPricingInterfaceService.RequestHandlers
{
    public interface IRequestHandler
    {
        string HandleRequest(string message, IDependencyInjectionManager dependencyInjectionManager);
    }
}
