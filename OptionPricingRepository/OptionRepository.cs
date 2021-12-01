using LoggerLog4net;
using OptionPricingDAO;
using OptionPricingDAO.DTOs;
using OptionPricingDomain;
using System.Collections.Generic;
using System.Linq;

namespace OptionPricingRepository
{
    public interface IOptionRepository
    {
        void InsertOptionParameters(Option option);
        void InsertPrice(Price price);
        List<Option> GetAllOptions();
        double? GetPriceByOptionAndPricingModel(Option option, PricingModelEnum pricingModel);
        void DeleteOption(Option optionDTO, PricingModelEnum pricingModel);
        void DeletePrice(Price price);
        List<Price> GetAllPrices();
    }

    public class OptionRepository : IOptionRepository
    {
        private readonly IOptionDAO optionDao;
        private static readonly ILogger logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public OptionRepository(IOptionDAO optionDAO)
        {
            this.optionDao = optionDAO;

        }
        public void DeleteOption(Option option, PricingModelEnum pricingModel)
        {
            logger.Debug($"DeleteOption, option : {option}, pricing model : {pricingModel}");
            optionDao.DeleteOption(GetOptionParametersDTO(option), OptionUtils.GetPricingModelDTO(pricingModel));
        }

        public void DeletePrice(Price price)
        {
            logger.Debug($"DeletePrice : {price}");
            optionDao.DeletePrice(OptionUtils.GetPriceDTOFromPrice(price));
        }

        public List<Option> GetAllOptions()
        {
            logger.Debug($"GetAllOptions");
            List<OptionParametersDTO> optionDTOList = optionDao.GetAllOptions();
            return optionDTOList
                    .Select(x => OptionUtils.GetOptionFromDTO(x))
                    .ToList();
        }

        public List<Price> GetAllPrices()
        {
            logger.Debug($"GetAllPrices");
            List<PriceDTO> priceDTOList = optionDao.GetAllPrices();
            return priceDTOList
                    .Select(x => OptionUtils.GetPriceFromDTO(x))
                    .ToList();
        }

        public double? GetPriceByOptionAndPricingModel(Option option, PricingModelEnum pricingModel)
        {
            logger.Debug($"GetPriceByOptionAndPricingModel, option : {option}, pricing model : {pricingModel}");
            OptionParametersDTO optionDTO = GetOptionParametersDTO(option);
            OptionPricingDAO.Common.PricingModelEnum pricingModelDTO = OptionUtils.GetPricingModelDTO(pricingModel);
            return optionDao.GetPriceByOptionAndPricingModel(optionDTO, pricingModelDTO);
        }

        public void InsertOptionParameters(Option option)
        {
            logger.Debug($"InsertOptionParameters : {option}");
            OptionParametersDTO optionDTO = GetOptionParametersDTO(option);
            optionDao.InsertOptionParameters(optionDTO);
        }

        public void InsertPrice(Price price)
        {
            logger.Debug($"InsertPrice : {price}");
            PriceDTO priceDTO = OptionUtils.GetPriceDTOFromPrice(price);
            optionDao.InsertPrice(priceDTO);
        }

        private OptionParametersDTO GetOptionParametersDTO(Option option)
        {
            logger.Debug($"GetOptionParametersDTO from : {option}");
            OptionPricingDAO.Common.ContractEnum contractType = OptionPricingDAO.Common.ContractEnum.FromString(option.OptionType.ToString());
            Underlying udl = option.UnderlyingObj;
            OptionPricingDAO.Common.UnderlyingTypeEnum udlType = OptionUtils.GetUnderlyingTypeDTO(udl.UnderlyingType);
            return new OptionParametersDTO(contractType, option.Strike, option.RiskFreeRate, option.Maturity.ToDateTime(), udl.Volatility,
                                            udl.UnderlyingName, udl.Spot, udlType);
        }
    }
}
