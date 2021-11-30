using System;
using OptionPricingDAO;
using OptionPricingDAO.DTOs;
using OptionPricingDAO.Common;
using System.Collections.Generic;
using LoggerLog4net;


namespace C_sharp_training
{
    class Program
    {
        private static readonly ILogger logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        static void Main(string[] args)
        {

            DateTime today = DateTime.Now;
            OptionParametersDTO optionParametersDTO = new OptionParametersDTO(ContractEnum.EUROPEANCALL, 250d, 0.03d,
                today.AddYears(1),
                0.15d, "FR_BNP", 300d, UnderlyingTypeEnum.STOCK);
            OptionDAO optionDao = new OptionDAO();
            optionDao.InsertOptionParameters(optionParametersDTO);

            PriceDTO price = new PriceDTO(optionParametersDTO, 58, PricingModelEnum.BlackScholes);
            optionDao.InsertPrice(price);

            List<OptionParametersDTO> optionList = optionDao.GetAllOptions();
            optionList.ForEach(option => logger.Info(option.ToString()));

            double? priceValue = optionDao.GetPriceByOptionAndPricingModel(optionList[0], PricingModelEnum.BlackScholes);
            logger.Info("Option price is : " + priceValue);

            /*
            double spot = 300;
            double strike = 250;
            double timeToExpiration = 1;
            double sigma = 0.15;
            double riskFreeRate = 0.03;
            double dividendYield = 0;

            // Black Scholes
            BlackScholesPricer pricerBSCall = new BlackScholesPricer(Common.ContractEnum.EUROPEANCALL, spot, strike, timeToExpiration, sigma, riskFreeRate, dividendYield);
            BlackScholesPricer pricerBSPut = new BlackScholesPricer(Common.ContractEnum.EUROPEANPUT, spot, strike, timeToExpiration, sigma, riskFreeRate, dividendYield);
            double? premiumCall = pricerBSCall.premium();
            double? premiumPut = pricerBSPut.premium();
            logger.Info($"Call Premium is {premiumCall} and Put Premium is {premiumPut}");
            //pricerBSCall.premiumChart();

            // Greeks Black Scholes
            GreeksBS greeksCall = new GreeksBS(Common.ContractEnum.EUROPEANCALL, spot, strike, timeToExpiration, sigma, riskFreeRate, dividendYield);
            GreeksBS greeksPut = new GreeksBS(Common.ContractEnum.EUROPEANPUT, spot, strike, timeToExpiration, sigma, riskFreeRate, dividendYield);
            logger.Info($"Call delta is {greeksCall.delta()}, Call gamma is {greeksCall.gamma()}" +
                $", Call rho is {greeksCall.rho()}, Call vega is {greeksCall.vega()}, Call theta is {greeksCall.theta()}");
            logger.Info($"Put delta is {greeksPut.delta()}, Put gamma is {greeksPut.gamma()}" +
                $", Put rho is {greeksPut.rho()}, Put vega is {greeksPut.vega()}, Put theta is {greeksPut.theta()}");

            // Binominal tree 
            int steps = 10;
            double timeStep = (double) timeToExpiration/steps;
            BinomialTreePricer tree = new BinomialTreePricer(spot, strike, timeStep, sigma, riskFreeRate, Common.ContractEnum.EUROPEANCALL, steps);
            logger.Info($"Call : Black Scholes price {pricerBSCall.premium()}, Binomial Tree price : {tree.premium()}");


            // Monte carlo
            IDiscretizationMonteCarlo eulerScheme = new EulerDiscretizationMonteCarlo();
            MonteCarloPricing monteCarloPricing = new MonteCarloPricing(Common.ContractEnum.EUROPEANCALL, eulerScheme, riskFreeRate, sigma, spot, strike, 100, 10000);
            logger.Info($"Call : Monte carlo price {monteCarloPricing.premium()}, standard error {monteCarloPricing.standardError()}");
            */

        }
    }
}
