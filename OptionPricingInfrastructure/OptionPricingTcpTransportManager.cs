using LoggerLog4net;
using NetMQ;
using NetMQ.Sockets;
using OptionPricingDomain;
using System.Collections.Generic;

namespace OptionPricingInfrastructure
{
    public enum RequestType
    {
        PriceOption,
        GetAllOptions,
        GetAllPrices
    }

    public interface IOptionPricingTcpTransportManager
    {
        Price PriceOption(Price clientInitialPrice);
        List<Option> GetAllOption();
        List<Price> GetAllPrices();

    }
    public class OptionPricingTcpTransportManager : IOptionPricingTcpTransportManager
    {
        private static readonly ILogger logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private readonly string address;
        private readonly IOptionPricingJsonSerializer<Price> optionPricingSerializerPrice;
        private readonly IOptionPricingJsonSerializer<List<Option>> optionPricingSerializerOptionList;
        private readonly IOptionPricingJsonSerializer<List<Price>> optionPricingSerializerPriceList;


        public OptionPricingTcpTransportManager(string endPoint, int port, IOptionPricingJsonSerializer<Price> optionPricingSerializerPrice,
            IOptionPricingJsonSerializer<List<Option>> optionPricingSerializerOptionList,
            IOptionPricingJsonSerializer<List<Price>> optionPricingSerializerPriceList)
        {
            this.address = $">tcp://{endPoint}:{port}";
            this.optionPricingSerializerPrice = optionPricingSerializerPrice;
            this.optionPricingSerializerOptionList = optionPricingSerializerOptionList;
            this.optionPricingSerializerPriceList = optionPricingSerializerPriceList;
        }

        public List<Option> GetAllOption()
        {
            logger.Debug("Request to get all options");
            using (var requestSocket = new RequestSocket(address))
            {
                var message = new NetMQMessage();
                message.Append(RequestType.GetAllOptions.ToString());
                message.Append(NetMQFrame.Empty);
                logger.Debug($"Sending serialized GetAllOption request");
                requestSocket.SendMultipartMessage(message);
                var response = requestSocket.ReceiveFrameString();
                logger.Debug($"Receiving serialized GetAllOption response : {response}");
                return optionPricingSerializerOptionList.Deserialize(response);
            }
        }

        public List<Price> GetAllPrices()
        {
            logger.Debug("Request to get all prices");
            using (var requestSocket = new RequestSocket(address))
            {
                var message = new NetMQMessage();
                message.Append(RequestType.GetAllPrices.ToString());
                message.Append(NetMQFrame.Empty);
                logger.Debug($"Sending serialized GetAllPrices request");
                requestSocket.SendMultipartMessage(message);
                var response = requestSocket.ReceiveFrameString();
                logger.Debug($"Receiving serialized GetAllPrices response : {response}");
                return optionPricingSerializerPriceList.Deserialize(response);
            }
        }

        public Price PriceOption(Price clientInitialPrice)
        {
            logger.Debug($"Request to price option : {clientInitialPrice.OptionObj}");
            using (var requestSocket = new RequestSocket(address))
            {
                string priceSerialized = optionPricingSerializerPrice.Serialize(clientInitialPrice);
                var message = new NetMQMessage();
                message.Append(RequestType.PriceOption.ToString());
                message.Append(priceSerialized);
                logger.Debug($"Sending serialized request : {priceSerialized}");
                requestSocket.SendMultipartMessage(message);
                var response = requestSocket.ReceiveFrameString();
                logger.Debug($"Receiving serialized response : {response}");
                return optionPricingSerializerPrice.Deserialize(response);
            }
        }

    }
}
