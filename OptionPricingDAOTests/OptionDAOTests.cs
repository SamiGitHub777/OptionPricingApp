using Microsoft.VisualStudio.TestTools.UnitTesting;
using OptionPricingDAO;
using OptionPricingDAO.Common;
using OptionPricingDAO.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OptionPricingDAOTests
{
    [TestClass]
    public class OptionDAOTests
    {
        private readonly IOptionDAO optionDao;
        public OptionDAOTests()
        {
            optionDao = new OptionDAO();
        }
        [TestMethod]
        public void InsertAndGetOptionParameters()
        {
            // Arrange
            OptionParametersDTO optionParametersDTO = DummyOption("DE_DAX_TEST");
            int initialOptionsNbr = optionDao.GetAllOptions().Count;
            // Act
            optionDao.InsertOptionParameters(optionParametersDTO);
            List<OptionParametersDTO> result = optionDao.GetAllOptions();
            // Assert
            Assert.AreEqual(result.Count, initialOptionsNbr+1);
            OptionParametersDTO option = result.Last(); // GetAllOptions keeps insertion order
            Assert.AreEqual(option.Spot, optionParametersDTO.Spot);
            Assert.AreEqual(option.Strike, optionParametersDTO.Strike);
            Assert.AreEqual(option.Underlying, optionParametersDTO.Underlying);
            Assert.IsNotNull(option.UnderlyingType);
            Assert.AreEqual(option.UnderlyingType.Value, optionParametersDTO.UnderlyingType.Value);
            Assert.AreEqual(option.Maturity, optionParametersDTO.Maturity);
            Assert.IsNotNull(option.OptionType);
            Assert.AreEqual(option.OptionType.Value, optionParametersDTO.OptionType.Value);
            Assert.AreEqual(option.RiskFreeRate, optionParametersDTO.RiskFreeRate);
            Assert.AreEqual(option.Volatility, optionParametersDTO.Volatility);
            CleanOption(option, null); // no price inserted => no pricing model
        }

        [TestMethod]
        public void InsertAndGetPriceByOption()
        {
            string udlName = "US_SPX_TEST";
            OptionParametersDTO option = DummyOption(udlName);
            PricingModelEnum pricingModel = PricingModelEnum.BlackScholes;
            double? priceValue = optionDao.GetPriceByOptionAndPricingModel(option, pricingModel);
            Assert.IsNull(priceValue);
            double expectedPriceValue = 125.69d;
            PriceDTO priceDTO = DummyPrice(udlName, pricingModel, expectedPriceValue);
            optionDao.InsertPrice(priceDTO);
            priceValue = optionDao.GetPriceByOptionAndPricingModel(option, pricingModel);
            Assert.IsNotNull(priceValue);
            Assert.AreEqual(priceValue, expectedPriceValue);
            CleanPrice(priceDTO);
            CleanOption(option, pricingModel);
        }

        private OptionParametersDTO DummyOption(string underlyingName)
        {
            DateTime today = DateTime.Now.Date;
            return new OptionParametersDTO(ContractEnum.AMERICANCALL, 15000d, 0.04d,
                today.AddYears(3),
                0.2d, underlyingName, 16000d, UnderlyingTypeEnum.OTHER);
        }

        private PriceDTO DummyPrice(string underlyingName, PricingModelEnum pricingModel, double priceValue)
        {
            OptionParametersDTO option = DummyOption(underlyingName);
            PriceDTO price = new PriceDTO(option, priceValue, pricingModel);
            return price;
        }


        private void CleanOption(OptionParametersDTO option, PricingModelEnum pricingModel)
        {
            optionDao.DeleteOption(option, pricingModel);
        }

        private void CleanPrice(PriceDTO price)
        {
            optionDao.DeletePrice(price);
        }
    }
}
