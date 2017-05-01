using System.Collections.Generic;
using CurrencyConverter.Core.Services;
using FluentAssertions;
using MvvmCross.Core.ViewModels;
using MvvmCross.FieldBinding;

namespace CurrencyConverter.Core.ViewModels
{
    public class FirstViewModel 
        : MvxViewModel
    {
        private readonly IConversionCalculator _conversionCalculator;

        public INC<List<string>> ConversionCurrencies = new NC<List<string>>();
        public INC<string> PostConvertAmount = new NCString();

        private string _fromConvertType;

        public string FromConvertType
        {
            get { return _fromConvertType; }
            set
            {
                SetProperty(ref _fromConvertType, value);
                Recalculate();
            }
        }

        private string _toConvertType;

        public string ToConvertType
        {
            get { return _toConvertType; }
            set
            {
                SetProperty(ref _toConvertType, value);
                Recalculate();
            }
        }

        private int _fromConvertPosition;
        public int FromConvertPosition
        {
            get { return _fromConvertPosition; }
            set
            {
                value.Should()
                    .BeLessThan(ConversionCurrencies.Value.Count)
                    .And
                    .BeGreaterOrEqualTo(0);

                SetProperty(ref _fromConvertPosition, value);
                Recalculate();
            }
        }

        private int _toConvertPosition;
        public int ToConvertPosition
        {
            get { return _toConvertPosition; }
            set
            {
                value.Should()
                    .BeLessThan(ConversionCurrencies.Value.Count)
                    .And
                    .BeGreaterOrEqualTo(0);

                SetProperty(ref _toConvertPosition, value);
                Recalculate();
            }
        }


        private string _preConvertAmount = "";
        public string PreConvertAmount
        {
            get { return _preConvertAmount; }
            set
            {
                SetProperty(ref _preConvertAmount, value);
                Recalculate();
            }
        }

        public bool ShowCurrencyPicker { get; set; }

        public FirstViewModel(IConversionCalculator conversionCalculator)
        {
            _conversionCalculator = conversionCalculator;
            ConversionCurrencies.Value = conversionCalculator.ConversionCurrencies;
            ConversionCurrencies.Value.Should().NotBeEmpty();
        }

        private void Recalculate()
        {
            PreConvertAmount.Should().NotBeNull();
            PostConvertAmount.Value =
                _conversionCalculator.Convert(PreConvertAmount, FromConvertType, ToConvertType);
        }



    }
}
