using Microsoft.VisualStudio.TestTools.UnitTesting;
using OptionPricingDomain;
using OptionPricingInfrastructure;
using System;

namespace OptionPricingInterfaceServiceTests
{
    [TestClass]
    public class OptionPricingJsonSerializerTests
    {
        [TestMethod]
        public void Serialize()
        {
            DateTime today = DateTime.Now;
            Maturity maturity = new Maturity(today.Year + 2, today.Month, today.Day);
            Underlying udl = new Underlying("toto", 16000d, UnderlyingTypeEnum.INDEX, 0.3d);
            Option option = new Option(ContractEnum.AmericanCall, maturity, 15000d, 0.04d, udl);
            OptionPricingJsonSerializer<Option> serializer = new OptionPricingJsonSerializer<Option>();

            string optionSerialized = serializer.Serialize(option);
            Assert.IsTrue(optionSerialized.Contains("toto"));
        }

        [TestMethod]
        public void SerializePrice()
        {
            DateTime today = DateTime.Now;
            Maturity maturity = new Maturity(today.Year + 2, today.Month, today.Day);
            Underlying udl = new Underlying("DE_DAX_TEST", 16000d, UnderlyingTypeEnum.INDEX, 0.3d);
            Option option = new Option(ContractEnum.AmericanCall, maturity, 15000d, 0.04d, udl);
            Price price = new Price(11.2d, PricingModelEnum.BlackScholes, option);
            OptionPricingJsonSerializer<Price> serializer = new OptionPricingJsonSerializer<Price>();
            string priceSerialized = serializer.Serialize(price);
            Assert.IsTrue(priceSerialized.Contains("DE_DAX_TEST"));
        }

        [TestMethod]
        public void Deserialized()
        {
            DateTime today = DateTime.Now;
            Maturity maturity = new Maturity(today.Year + 2, today.Month, today.Day);
            Underlying udl = new Underlying("toto", 16000d, UnderlyingTypeEnum.INDEX, 0.3d);
            Option optionExpected = new Option(ContractEnum.AmericanCall, maturity, 15000d, 0.04d, udl);
            OptionPricingJsonSerializer<Option> serializer = new OptionPricingJsonSerializer<Option>();

            string optionSerialized = serializer.Serialize(optionExpected);
            Option option = serializer.Deserialize(optionSerialized);
            Assert.IsTrue(optionExpected.Equals(option));
        }
        

        [TestMethod]
        public void DeserializedUnderlying()
        {
            string udlSerialized = "{\"UnderlyingName\":\"toto\",\"Spot\":16000.0,\"UnderlyingType\":2,\"Volatility\":0.3}";
            OptionPricingJsonSerializer<Underlying> serializer = new OptionPricingJsonSerializer<Underlying>();
            Underlying udl = serializer.Deserialize(udlSerialized);
            Assert.AreEqual(udl.UnderlyingName, "toto");
            
        }

        [TestMethod]
        public void DeserializedMaturity()
        {
            string maturitySerialized = "{\"Year\":2021,\"Month\":12,\"Day\":25}";
            OptionPricingJsonSerializer<Maturity> serializer = new OptionPricingJsonSerializer<Maturity>();
            Maturity maturity = serializer.Deserialize(maturitySerialized);
            Assert.IsTrue(new Maturity(2021, 12 , 25).Equals(maturity));

        }

    }
}
