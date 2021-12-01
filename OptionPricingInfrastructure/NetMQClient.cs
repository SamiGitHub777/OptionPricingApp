using LoggerLog4net;
using NetMQ;
using NetMQ.Sockets;
using OptionPricingDomain;
using System;
using System.Collections.Generic;
using System.Threading;

namespace OptionPricingInfrastructure
{

    public class NetMQClient
    {
        private static readonly ILogger logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private readonly string endPoint;
        private readonly int port;

        public NetMQClient(string endPoint, int port)
        {
            this.endPoint = endPoint;
            this.port = port;
        }

        public Price StartSending(Price initialPrice)
        {
            using (var client = new RequestSocket())
            {
                client.Connect($"tcp://{endPoint}:{port}");
                IOptionPricingJsonSerializer<Price> optionPricingSerializerPrice = new OptionPricingJsonSerializer<Price>();
                IOptionPricingJsonSerializer<List<Option>> optionPricingSerializerOptionList = new OptionPricingJsonSerializer<List<Option>>();
                IOptionPricingJsonSerializer<List<Price>> optionPricingSerializerPriceList = new OptionPricingJsonSerializer<List<Price>>();
                IOptionPricingTcpTransportManager optionPricingTcpTransportManager = new OptionPricingTcpTransportManager(endPoint, port, optionPricingSerializerPrice, optionPricingSerializerOptionList, optionPricingSerializerPriceList) ;
                logger.Info("Waiting for response ...");
                Price computedPrice = optionPricingTcpTransportManager.PriceOption(initialPrice);
                logger.Info($"Client received a response : {computedPrice}");
                return computedPrice;
            }
        }

        public List<Option> StartRequestingOptionList()
        {
            using (var client = new RequestSocket())
            {
                client.Connect($"tcp://{endPoint}:{port}");
                IOptionPricingJsonSerializer<Price> optionPricingSerializerPrice = new OptionPricingJsonSerializer<Price>();
                IOptionPricingJsonSerializer<List<Option>> optionPricingSerializerOptionList = new OptionPricingJsonSerializer<List<Option>>();
                IOptionPricingJsonSerializer<List<Price>> optionPricingSerializerPriceList = new OptionPricingJsonSerializer<List<Price>>();
                IOptionPricingTcpTransportManager optionPricingTcpTransportManager = new OptionPricingTcpTransportManager(endPoint, port, optionPricingSerializerPrice, optionPricingSerializerOptionList, optionPricingSerializerPriceList);
                logger.Info("Waiting for response ...");
                var message = new NetMQMessage();
                message.Append(RequestType.GetAllOptions.ToString());
                client.SendMultipartMessage(message);
                var response = client.ReceiveFrameString();
                logger.Info($"Client received a response : {response}");
                return optionPricingSerializerOptionList.Deserialize(response);
            }
        }

        public static void ClientTaskCallBack(Object threadNumber, CountdownEvent evt)
        {
            string threadName = "Thread " + threadNumber.ToString();
            DateTime today = DateTime.Now;
            // some dummy objects
            Maturity maturity = new Maturity(today.Year + 2, today.Month, today.Day);
            Underlying udl = new Underlying("DE_DAX_TEST", 18000d, UnderlyingTypeEnum.INDEX, 0.3d);
            Option option = new Option(ContractEnum.EuropeanCall, maturity, 15000d, 0.04d, udl);
            NetMQClient clientMQ = new NetMQClient("localhost", 5555);
            for (int task = 0; task < 10; task++)
            {
                clientMQ.StartSending(new Price(0d, PricingModelEnum.BlackScholes, option.DummyOption()));
            }
            logger.Debug(threadName + " finished...");
            evt.Signal();
        }

        public static void ClientTaskCallBackGetOptionList(Object threadNumber, CountdownEvent evt)
        {
            string threadName = "Thread " + threadNumber.ToString();
            NetMQClient clientMQ = new NetMQClient("localhost", 5555);
            for (int task = 0; task < 10; task++)
            {
                clientMQ.StartRequestingOptionList();
            }
            logger.Debug(threadName + " finished...");
            evt.Signal();
        }


    }

}
