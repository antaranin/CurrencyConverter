using System.Collections.Generic;
using System.Linq;

namespace CurrencyConverter.Core.Services
{
    public class LocalConversionCalculator: IConversionCalculator
    {
        public List<string> ConversionCurrencies { get; }

        private readonly IDictionary<string, double> _toUsdConversions;

        public LocalConversionCalculator()
        {
            _toUsdConversions = new Dictionary<string, double>
            {
                {"USD", 1.0},
                {"DKK", 0.15},
                {"PLN", 0.26},
                {"EUR", 1.09}
            };
            ConversionCurrencies = _toUsdConversions.Keys.ToList();
        }

        public string Convert(string preConvertAmount,
            int fromConvertPosition, int toConvertPosition)
        {
            var amount = ConvertToNumber(preConvertAmount);
            var ratio = CurrencyRatio(fromConvertPosition, toConvertPosition);
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

        private double CurrencyRatio(int fromConvertPosition, int toConvertPosition)
        {
            var fromUsdRatio = ConvertPositionToToUsdRatio(fromConvertPosition);
            var toUsdRatio = ConvertPositionToToUsdRatio(toConvertPosition);

            return fromUsdRatio / toUsdRatio;
        }

        private double ConvertPositionToToUsdRatio(int convertPosition)
        {
            return _toUsdConversions[ConversionCurrencies[convertPosition]];
        }
    }
}