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

        public Task<string> Convert(string preConvertAmount, string fromConvertType,
            string toConvertType)
        {
            throw new System.NotImplementedException();
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