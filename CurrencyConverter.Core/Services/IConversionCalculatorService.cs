using System.Collections.Generic;

namespace CurrencyConverter.Core.Services
{
    public interface IConversionCalculator
    {
        List<string> ConversionCurrencies { get; }

        string Convert(string preConvertAmount, int fromConvertPosition, int toConvertPosition);
    }
}