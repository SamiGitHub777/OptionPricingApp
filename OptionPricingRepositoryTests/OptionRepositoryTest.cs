using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using OptionPricingRepository;
using OptionPricingDomain;
using OptionPricingDAO;
using NSubstitute;
using OptionPricingDAO.DTOs;
using System.Collections.Generic;
using System.Linq;

namespace OptionPricingDAOTests
{
    [TestClass]
    public class OptionRepositoryTest
    {
        private readonly IOptionRepository optionRepo;
        private readonly IOptionDAO optionDAO;

        public OptionRepositoryTest()
        {
            this.optionDAO = Substitute.For<IOptionDAO>();
            this.optionRepo = new OptionRepository(optionDAO);
        }

        [TestMethod]
        public void InsertAndGetOption()
        {
            // Arrange
            Option option = DummyOption("US_SPX_TEST");
            OptionPricingDAO.Common.ContractEnum contractType = OptionPricingDAO.Common.ContractEnum.FromString(option.OptionType.ToString());
            Underlying udl = option.UnderlyingObj;
            OptionPricingDAO.Common.UnderlyingTypeEnum udlType = OptionPricingDAO.Common.UnderlyingTypeEnum.FromString(option.UnderlyingObj.UnderlyingType.ToString());
            OptionParametersDTO expectedOptionDTO =  new OptionParametersDTO(contractType, option.Strike, option.RiskFreeRate, option.Maturity.ToDateTime(), udl.Volatility,
                                            udl.UnderlyingName, udl.Spot, udlType);
            //Act
            optionRepo.InsertOptionParameters(option);
            //Assert
            optionDAO.Received(1).InsertOptionParameters(expectedOptionDTO); // Check a call was received a specific number of times with a specific arg
        }

        [TestMethod]
        public void InsertAndGetPriceByOption()
        {
            string udlName = "US_SPX_TEST";
            Option option = DummyOption(udlName);
            PricingModelEnum pricingModel = PricingModelEnum.MonteCarlo;
            double priceExpected = 10.574d;
            Price price = DummyPrice(udlName, pricingModel, priceExpected);
            OptionPricingDAO.Common.ContractEnum contractType = OptionPricingDAO.Common.ContractEnum.FromString(option.OptionType.ToString());
            Underlying udl = option.UnderlyingObj;
            OptionPricingDAO.Common.UnderlyingTypeEnum udlType = OptionPricingDAO.Common.UnderlyingTypeEnum.FromString(option.UnderlyingObj.UnderlyingType.ToString());
            OptionParametersDTO expectedOptionDTO = new OptionParametersDTO(contractType, option.Strike, option.RiskFreeRate, option.Maturity.ToDateTime(), udl.Volatility,
                                            udl.UnderlyingName, udl.Spot, udlType);
            PriceDTO expectedPriceDTO = new PriceDTO(expectedOptionDTO, priceExpected, OptionPricingDAO.Common.PricingModelEnum.FromString(pricingModel.ToString()));

            optionRepo.InsertPrice(price);
            optionDAO.Received(1).InsertPrice(expectedPriceDTO);
        }

        [TestMethod]
        public void GetAllOption()
        {
            string udlName = "MockedUdl";
            Option option = DummyOption(udlName);
            PricingModelEnum pricingModel = PricingModelEnum.MonteCarlo;
            double priceExpected = 10.574d;
            Price price = DummyPrice(udlName, pricingModel, priceExpected);
            OptionPricingDAO.Common.ContractEnum contractType = OptionPricingDAO.Common.ContractEnum.FromString(option.OptionType.ToString());
            Underlying udl = option.UnderlyingObj;
            OptionPricingDAO.Common.UnderlyingTypeEnum udlType = OptionPricingDAO.Common.UnderlyingTypeEnum.FromString(option.UnderlyingObj.UnderlyingType.ToString());
            OptionParametersDTO expectedOptionDTO = new OptionParametersDTO(contractType, option.Strike, option.RiskFreeRate, option.Maturity.ToDateTime(), udl.Volatility,
                                            udl.UnderlyingName, udl.Spot, udlType);
            List<OptionParametersDTO> listOptionDTO = new List<OptionParametersDTO>();
            listOptionDTO.Add(expectedOptionDTO);
            optionDAO.GetAllOptions().Returns(listOptionDTO);

            List<Option> expectedList = new List<Option>();
            expectedList.Add(option);
            var result = optionRepo.GetAllOptions();
            Assert.IsNotNull(result);
            Assert.AreEqual(expectedList.Count, result.Count);
            Assert.IsTrue(expectedList.First().Equals(result.First()));
        }

        private Option DummyOption(string underlyingName)
        {
            DateTime today = DateTime.Now;
            Maturity maturity = new Maturity(today.Year + 2, today.Month, today.Day);
            Underlying udl = new Underlying(underlyingName, 16000d, UnderlyingTypeEnum.INDEX, 0.3d);
            return new Option(ContractEnum.AmericanCall, maturity, 15000d, 0.04d, udl);
        }

        private Price DummyPrice(string underlyingName, PricingModelEnum pricingModel, double priceValue)
        {
            Option option = DummyOption(underlyingName);
            return new Price(priceValue, pricingModel, option);
        }

    }
}
