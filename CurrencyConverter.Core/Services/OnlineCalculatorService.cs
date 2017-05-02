using System.Collections.Generic;
using System.Threading.Tasks;

namespace CurrencyConverter.Core.Services
{
    public class OnlineCalculatorService: IConversionCalculator
    {
        private readonly IConversionRepository _conversionRepository;

        public OnlineCalculatorService(IConversionRepository repository)
        {
            _conversionRepository = repository;
        }

        public Task<List<string>> GetConversionCurrencies()
        {
            throw new System.NotImplementedException();
        }

        public Task<string> Convert(string preConvertAmount, string fromConvertType, string toConvertType)
        {
            throw new System.NotImplementedException();
        }
    }
}