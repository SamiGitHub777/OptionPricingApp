using LoggerLog4net;
using OptionPricingDomain;
using OptionPricingInfrastructure;
using System;
using System.Collections.Generic;

namespace OptionPricingClientConsole
{
    public class Client
    {
        private static readonly ILogger logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public static void main(string[] args)
        {
            logger.Info("Client is sending a request");
            IOptionPricingJsonSerializer<Price> optionPricingSerializerPrice = new OptionPricingJsonSerializer<Price>();
            IOptionPricingJsonSerializer<List<Option>> optionPricingSerializerOptionList = new OptionPricingJsonSerializer<List<Option>>();
            IOptionPricingTcpTransportManager optionPricingTcpTransportManager = new OptionPricingTcpTransportManager("localhost", 5555, optionPricingSerializerPrice, optionPricingSerializerOptionList);
            DateTime today = DateTime.Now;
            Maturity maturity = new Maturity(today.Year + 2, today.Month, today.Day);
            Underlying udl = new Underlying("DE_DAX_TEST", 18000d, UnderlyingTypeEnum.INDEX, 0.3d);
            Option option = new Option(ContractEnum.EuropeanCall, maturity, 15000d, 0.04d, udl);
            Price initialPrice = new Price(0d, PricingModelEnum.BlackScholes, option);
            logger.Info("Waiting for response ...");
            Price computedPrice = optionPricingTcpTransportManager.PriceOption(initialPrice);
            logger.Info($"Client received a response : {computedPrice}");
        }
    }
}
