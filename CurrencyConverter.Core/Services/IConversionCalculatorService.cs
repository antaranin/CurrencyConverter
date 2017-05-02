using System.Collections.Generic;
using System.Threading.Tasks;

namespace CurrencyConverter.Core.Services
{
    public interface IConversionCalculator
    {
        Task<List<string>> GetConversionCurrencies();

        Task<string> Convert(string preConvertAmount, string fromConvertType, string toConvertType);
    }
}