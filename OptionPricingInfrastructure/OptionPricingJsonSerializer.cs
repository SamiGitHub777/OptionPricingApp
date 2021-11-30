using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptionPricingInfrastructure
{
    public interface IOptionPricingJsonSerializer<T>
    {
        string Serialize(T obj);
        T Deserialize(string obj);
    }
    public class OptionPricingJsonSerializer<T> : IOptionPricingJsonSerializer<T>
    {
        public string Serialize(T obj)
        {
            return JsonConvert.SerializeObject(obj);
        }

        public T Deserialize(string obj)
        {
            return JsonConvert.DeserializeObject<T>(obj);
        }
    }
}
