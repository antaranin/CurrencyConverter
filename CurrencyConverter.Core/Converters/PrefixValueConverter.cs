using System;
using System.Globalization;
using MvvmCross.Platform.Converters;

namespace CurrencyConverter.Core.Converters
{
    public class PrefixValueConverter: MvxValueConverter<string, string>
    {
        protected override string Convert(string value, Type targetType, object parameter, CultureInfo culture)
        {
            if(!(parameter is string))
                return value;
            return (string) parameter + value;
        }

        protected override string ConvertBack(string value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(parameter is string))
                return value;
            var prefix = (string) parameter;
            return value.StartsWith(prefix) ? value.Remove(0, prefix.Length) : value;
        }
    }
}