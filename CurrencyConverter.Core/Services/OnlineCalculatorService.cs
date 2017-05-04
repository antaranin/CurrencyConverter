using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CurrencyConverter.DataLayer.IRepositories;
using CurrencyConverter.DataLayer.Model;
using EnsureThat;

namespace CurrencyConverter.Core.Services
{
    public class OnlineCalculatorService: IConversionCalculator
    {
        private readonly IDataOperationService _dataOperationService;
        private readonly IConversionGroupProviderService _conversionGroupProvider;
        private ConversionsGroup _conversionsGroup;

        public OnlineCalculatorService(IDataOperationService dataOperationService,
            IConversionGroupProviderService conversionGroupProviderService)
        {
            _dataOperationService = dataOperationService;
            _conversionGroupProvider = conversionGroupProviderService;
        }

        public Task<List<string>> GetConversionCurrencies()
        {
            return Task.Run(ExtractConversionCurrencies);
        }

        private async Task<List<string>> ExtractConversionCurrencies()
        {
            if (_conversionsGroup == null)
                _conversionsGroup = GetLatestConversionGroup();

            if (_conversionsGroup != null && IsConversionGroupUpToDate(_conversionsGroup))
                return GenerateConversionCurrencies(_conversionsGroup);

            var providedGroup = await _conversionGroupProvider.ProvideConversionGroup();
            if (providedGroup == null)
                return GenerateConversionCurrencies(_conversionsGroup);

            _conversionsGroup = providedGroup;
            AddNewestConversionGroup(providedGroup);

            return GenerateConversionCurrencies(_conversionsGroup);
        }

        private static List<string> GenerateConversionCurrencies(ConversionsGroup group)
        {
            return @group == null
                ? new List<string>()
                : @group.ConversionRates.Select(c => c.ConversionName).ToList();
        }


        private bool IsConversionGroupUpToDate(ConversionsGroup group)
        {
            Ensure.That(group).IsNotNull();
            return group.Date.Date == DateTime.Today;
        }

        public Task<string> Convert(string preConvertAmount,
            string fromConvertType, string toConvertType)
        {
            return Task.Run(
                () => ConvertCurrencies(preConvertAmount, fromConvertType, toConvertType));
        }

        private string ConvertCurrencies(string preConvertAmount,
            string fromConvertType, string toConvertType)
        {
            if (_conversionsGroup == null)
                return "0";

            Ensure.That(_conversionsGroup.ConversionRates)
                .Any(x => x.ConversionName == fromConvertType)
                .And()
                .Any(x => x.ConversionName == toConvertType);

            var amount = ConvertToNumber(preConvertAmount);
            var ratio = CurrencyRatio(fromConvertType, toConvertType);
            var convertedAmount = amount * ratio;
            return ConvertToString(convertedAmount);
        }

        private double ConvertToNumber(string currencyAmount)
        {
            double value;
            var success = double.TryParse(currencyAmount, out value);
            return success ? value : 0;
        }

        private string ConvertToString(double number)
        {
            return number.ToString("N2");
        }

        private double CurrencyRatio(string fromConvertType, string toConvertType)
        {
            var fromBaseRatio = ConvertCurrencyToBaseRatio(fromConvertType);
            var toBaseRatio = ConvertCurrencyToBaseRatio(toConvertType);

            return toBaseRatio / fromBaseRatio;
        }

        private double ConvertCurrencyToBaseRatio(string convertType)
        {
            return _conversionsGroup.ConversionRates
                .First(cr => cr.ConversionName == convertType)
                .BaseConversionRate;
        }

        private void AddNewestConversionGroup(ConversionsGroup conversionsGroup)
        {
            Ensure.That(conversionsGroup).IsNotNull();
            _dataOperationService.RunTransactionOperation(operation =>
            {
                var groupRepo = operation.ConversionsGroupRepository;
                groupRepo.Upsert(conversionsGroup);
            });
        }

        private ConversionsGroup GetLatestConversionGroup()
        {
            ConversionsGroup group = null;
            _dataOperationService.RunOperation(operation =>
            {
                var groupRepo = operation.ConversionsGroupRepository;
                group = groupRepo.FindLatest(1);
            });

            return group;
        }
    }
}