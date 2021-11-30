using LoggerLog4net;
using OptionPricingDomain;
using OptionPricingRepository;
using System.Collections.Generic;
using System.Diagnostics;

namespace OptionPricingDomainService
{
    public interface IOptionPricingPersistenceService
    {
        void InsertOption(Option option);
        void InsertPrice(Price price);

        List<Option> GetAllOptions();
    }
    public class OptionPricingPersistenceService : IOptionPricingPersistenceService
    {
        private static readonly ILogger logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private readonly IOptionRepository optionRepository;
        public OptionPricingPersistenceService(IOptionRepository optionRepository)
        {
            this.optionRepository = optionRepository;
        }

       public void InsertOption(Option option)
        {
            logger.Info("Start inserting option");
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            optionRepository.InsertOptionParameters(option);
            stopwatch.Stop();
            logger.Info($"Option insertion done in {stopwatch.Elapsed}");
        }

        public void InsertPrice(Price price)
        {
            logger.Info("Start inserting price");
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            optionRepository.InsertPrice(price);
            stopwatch.Stop();
            logger.Info($"Price insertion done in {stopwatch.Elapsed}");
        }

        public List<Option> GetAllOptions()
        {
            logger.Info("Start option list fetch");
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            List<Option> res = optionRepository.GetAllOptions();
            stopwatch.Stop();
            logger.Info($"Option list fetch done in {stopwatch.Elapsed}");
            return res;
        }
    }
}
