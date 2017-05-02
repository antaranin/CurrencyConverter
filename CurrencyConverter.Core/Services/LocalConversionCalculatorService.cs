using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EnsureThat;

namespace CurrencyConverter.Core.Services
{
    public class LocalConversionCalculatorService: IConversionCalculator
    {
        private readonly IDictionary<string, double> _toUsdConversions;
        private readonly List<string> _conversionCurrencies;

        public LocalConversionCalculatorService()
        {
            _toUsdConversions = new Dictionary<string, double>
            {
                {"USD", 1.0},
                {"DKK", 0.15},
                {"PLN", 0.26},
                {"EUR", 1.09}
            };
            _conversionCurrencies = _toUsdConversions.Keys.ToList();
        }

        public Task<List<string>> GetConversionCurrencies()
        {
            return Task.FromResult(_conversionCurrencies);
        }

        public Task<string> Convert(string preConvertAmount,
            string fromConvertType, string toConvertType)
        {
            Ensure.That(_conversionCurrencies)
                .Any(x => x == fromConvertType)
                .And()
                .Any(x => x == toConvertType);

            var amount = ConvertToNumber(preConvertAmount);
            var ratio = CurrencyRatio(fromConvertType, toConvertType);
            var convertedAmount = amount * ratio;
            return Task.FromResult(ConvertToString(convertedAmount));
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
            var fromUsdRatio = ConvertCurrencyToUsdRatio(fromConvertType);
            var toUsdRatio = ConvertCurrencyToUsdRatio(toConvertType);

            return fromUsdRatio / toUsdRatio;
        }

        private double ConvertCurrencyToUsdRatio(string convertType)
        {
            return _toUsdConversions[convertType];
        }
    }
}