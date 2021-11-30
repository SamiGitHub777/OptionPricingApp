using OptionPricingDAO.DTOs;
using OptionPricingDomain;
using System;

namespace OptionPricingRepository
{
    public class OptionUtils
    {
        public static Option GetOptionFromDTO(OptionParametersDTO optionParameters)
        {
            ContractEnum contractType = (ContractEnum)Enum.Parse(typeof(ContractEnum), optionParameters.OptionType.Value);
            DateTime optionMatu = optionParameters.Maturity;
            Maturity maturity = new Maturity(optionMatu.Year, optionMatu.Month, optionMatu.Day);
            UnderlyingTypeEnum udlType = (UnderlyingTypeEnum)Enum.Parse(typeof(UnderlyingTypeEnum), optionParameters.UnderlyingType.Value.ToUpper());
            Underlying udl = new Underlying(optionParameters.Underlying, optionParameters.Spot, udlType, optionParameters.Volatility);
            return new Option(contractType, maturity, optionParameters.Strike, optionParameters.RiskFreeRate, udl);
        }

        public static C_sharp_training.Common.ContractEnum OptionTypeToPricingEnum(Option option)
        {
            return (C_sharp_training.Common.ContractEnum)Enum.Parse(typeof(C_sharp_training.Common.ContractEnum), option.OptionType.ToString().ToUpper());
        }

        public static double TimeToMaturity(Option option)
        {
            return option.Maturity.Year - DateTime.Now.Year + (double)(option.Maturity.Month - DateTime.Now.Month) / 12 + (double)(option.Maturity.Day - DateTime.Now.Day) / 365;
        }

        public static OptionParametersDTO GetOptionDTOFromOption(Option option)
        {
            OptionPricingDAO.Common.ContractEnum contractType = OptionPricingDAO.Common.ContractEnum.FromString(option.OptionType.ToString());
            Underlying udl = option.UnderlyingObj;
            OptionPricingDAO.Common.UnderlyingTypeEnum udlType = GetUnderlyingTypeDTO(udl.UnderlyingType);
            return new OptionParametersDTO(contractType, option.Strike, option.RiskFreeRate, option.Maturity.ToDateTime(), udl.Volatility,
                                            udl.UnderlyingName, udl.Spot, udlType);
        }

        public static OptionPricingDAO.Common.PricingModelEnum GetPricingModelDTO(PricingModelEnum pricingModel)
        {
            return OptionPricingDAO.Common.PricingModelEnum.FromString(pricingModel.ToString());
        }

        public static OptionPricingDAO.Common.UnderlyingTypeEnum GetUnderlyingTypeDTO(UnderlyingTypeEnum udlType)
        {
            return OptionPricingDAO.Common.UnderlyingTypeEnum.FromString(udlType.ToString());
        }

        public static PriceDTO GetPriceDTOFromPrice(Price price)
        {
            OptionParametersDTO optionDTO = GetOptionDTOFromOption(price.OptionObj);
            OptionPricingDAO.Common.PricingModelEnum pricingModel = GetPricingModelDTO(price.PricingModel);
            return new PriceDTO(optionDTO, price.PriceValue, pricingModel);
        }

    }
}
