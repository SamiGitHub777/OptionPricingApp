using CommonServiceLocator;
using GalaSoft.MvvmLight.Command;
using MahApps.Metro.IconPacks;
using OptionPricingWPFClient.Commands;
using OptionPricingWPFClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WPFHelper;

namespace OptionPricingWPFClient.ViewModel
{
    public class OptionsPricingViewModel : ViewModelBase
    {
        public enum OptionTypeEnum
        {
            EuropeanCall,
            EuropeanPut,
            AmericanCall,
            AmericanPut
        }
        public enum UnderlyingTypeEnum
        {
            STOCK,
            INDEX,
            COMMODITY,
            BASKET,
            CPI,
            FUND,
            ETF,
            FX
        }

        public enum PricingModelEnum
        {
            BlackScholes,
            BinomialTree,
            MonteCarlo
        }

        public PricingModelEnum PricingModel
        {
            get => _pricingModel;
            set => SetProperty<PricingModelEnum>(ref _pricingModel, value);
        }
        private PricingModelEnum _pricingModel;

        public double UnderlyingSpot { 
            get => _underlyingSpot; 
            set => SetProperty<double>(ref _underlyingSpot, value); }
        private double _underlyingSpot;

        public double UnderlyingVol { get => _underlyingVol; set => SetProperty<double>(ref _underlyingVol, value); }
        private double _underlyingVol;

        public string UnderlyingName { get => _underlyingName; set => SetProperty<string>(ref _underlyingName, value); }
        private string _underlyingName;

        public UnderlyingTypeEnum UnderlyingType { get => _underlyingType; set => SetProperty<UnderlyingTypeEnum>(ref _underlyingType, value); }
        private UnderlyingTypeEnum _underlyingType;

        public OptionTypeEnum OptionType { get => _optionType; set => SetProperty<OptionTypeEnum>(ref _optionType, value); }
        private OptionTypeEnum _optionType;

        public double Strike { get => _strike; set => SetProperty<double>(ref _strike, value); }
        private double _strike;

        public double RiskFreeRate { get => _riskFreeRate; set => SetProperty<double>(ref _riskFreeRate, value); }
        private double _riskFreeRate;

        public DateTime? Maturity { get => _maturity; set => SetProperty<DateTime?>(ref _maturity, value); }
        private DateTime? _maturity;

        public double Price { get => _price; set => SetProperty<double>(ref _price, value); }
        private double _price;

        /*
        public PricingModelEnum PricingModel => price.PricingModel;
        public double UnderlyingSpot => price.OptionObj.UnderlyingObj.Spot;
        public double UnderlyingVol => price.OptionObj.UnderlyingObj.Volatility;
        public string UnderlyingName => price.OptionObj.UnderlyingObj.UnderlyingName;
        public UnderlyingTypeEnum UnderlyingType => price.OptionObj.UnderlyingObj.UnderlyingType;
        public OptionTypeEnum OptionType => price.OptionObj.OptionType;
        public DateTime Maturity => price.OptionObj.Maturity;
        public double Price => price.PriceValue;
        */
        //private readonly Price price;

        public ICommand PriceCommand { get; set; }
        private readonly IModelArgsValidator modelArgsValidator;
        private readonly IOptionPricingModel optionPricingModel;


        public OptionsPricingViewModel(IModelArgsValidator modelArgsValidator, IOptionPricingModel optionPricingModel)
        {
            //this.price = price;
            this.PriceCommand = new RelayCommand(OnClickPriceCommand);
            this.modelArgsValidator = modelArgsValidator;
            this.optionPricingModel = optionPricingModel;

            /*
            Underlying underlying = new Underlying("FR_BNP", 150, OptionsPricingViewModel.UnderlyingTypeEnum.STOCK, 0.3);
            Option option = new Option(OptionsPricingViewModel.OptionTypeEnum.AmericanCall, new DateTime(), 100, 0.1, underlying);
            this.price = new Price(999, OptionsPricingViewModel.PricingModelEnum.BlackScholes, option);
            this.PriceCommand = new PriceOptionCommand(this, this.price);
            */
        }

        public void OnClickPriceCommand()
        {
            CheckArgs(modelArgsValidator.IsOptionMatuValid(_maturity), nameof(_maturity));
            CheckArgs(modelArgsValidator.IsStringValid(_underlyingName), nameof(_underlyingName));
            CheckArgs(modelArgsValidator.IsDoubleValid(_underlyingSpot), nameof(_underlyingSpot));
            CheckArgs(modelArgsValidator.IsDoubleValid(_underlyingVol), nameof(_underlyingVol));
            CheckArgs(modelArgsValidator.IsDoubleValid(_strike), nameof(_strike));
            CheckArgs(modelArgsValidator.IsDoubleValid(_riskFreeRate), nameof(_riskFreeRate));
            UnderlyingModel underlyingModel = new UnderlyingModel(_underlyingName, _underlyingSpot, _underlyingType, _underlyingVol);
            OptionModel optionModel =  new OptionModel(_optionType, _maturity, _strike, _riskFreeRate, underlyingModel);
            PriceModel priceModel = new PriceModel(_price, _pricingModel, optionModel);
            PriceModel result = optionPricingModel.PriceOption(priceModel);
            Price = result.PriceValue;
        }

        private void CheckArgs(bool b, string name)
        {
            if (!b)
            {
                throw new ArgumentException($"Invalid property : {name}");
            }
        }
    }

}
