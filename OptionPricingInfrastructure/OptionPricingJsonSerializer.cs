using Newtonsoft.Json;


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
