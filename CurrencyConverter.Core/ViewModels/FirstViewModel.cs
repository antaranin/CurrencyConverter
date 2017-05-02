using System.Collections.Generic;
using System.Diagnostics;
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
        public INC<bool> ShowCurrencyPicker = new NC<bool>();

        private SelectedCurrencyType _selectedCurrencyType = SelectedCurrencyType.None;

        private string _currentSelectedCurrency = "";
        public string CurrentSelectedCurrency
        {
            get { return _currentSelectedCurrency; }
            set
            {
                _selectedCurrencyType.Should()
                    .NotBe(SelectedCurrencyType.None);

                if (_selectedCurrencyType == SelectedCurrencyType.ToCurrency)
                    ToConvertType = value;
                else
                    FromConvertType = value;
                SetProperty(ref _currentSelectedCurrency, value);
            }
        }


        private string _fromConvertType;
        public string FromConvertType
        {
            get { return _fromConvertType; }
            set
            {
                value.Should()
                    .NotBeNullOrEmpty();

                ConversionCurrencies.Value.Should()
                    .Contain(value);

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
                value.Should()
                    .NotBeNullOrEmpty();

                ConversionCurrencies.Value.Should()
                    .Contain(value);

                SetProperty(ref _toConvertType, value);
                RaisePropertyChanged(() => ProcessedPostConvertAmount);
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

        private string _postConvertAmount = "0";
        private string PostConvertAmount
        {
            get { return _postConvertAmount; }
            set
            {
                _postConvertAmount = value;
                RaisePropertyChanged(() => ProcessedPostConvertAmount);
            }
        }

        public string ProcessedPostConvertAmount
        {
            get { return PostConvertAmount + " " + ToConvertType;  }
        }

        public FirstViewModel(IConversionCalculator conversionCalculator)
        {
            _conversionCalculator = conversionCalculator;
            ShowCurrencyPicker.Value = false;
            var conversionCurrencies = conversionCalculator.GetConversionCurrencies();
            conversionCurrencies.GetAwaiter().OnCompleted(() =>
                LoadData(conversionCurrencies.Result));
        }

        private void LoadData(List<string> conversionCurrencies)
        {
            ConversionCurrencies.Value = conversionCurrencies;
            ConversionCurrencies.Value.Should().NotBeEmpty();
            FromConvertType = conversionCurrencies[0];
            ToConvertType = conversionCurrencies[0];
        }

        public void StopSelectingCurrency()
        {
             _selectedCurrencyType = SelectedCurrencyType.None;
            ShowCurrencyPicker.Value = false;
        }

        public void SelectFromCurrencyType()
        {
            if (ConversionCurrencies.Value == null)
                return;
            _selectedCurrencyType = SelectedCurrencyType.FromCurrency;
            if (ConversionCurrencies.Value.Contains(FromConvertType))
                CurrentSelectedCurrency = FromConvertType;
            ShowCurrencyPicker.Value = true;
        }

        public void SelectToCurrencyType()
        {
            if (ConversionCurrencies.Value == null)
                return;
           _selectedCurrencyType = SelectedCurrencyType.ToCurrency;
            if(ConversionCurrencies.Value.Contains(ToConvertType))
                CurrentSelectedCurrency = ToConvertType;
            ShowCurrencyPicker.Value = true;
        }

        private void Recalculate()
        {
            if (string.IsNullOrEmpty(PreConvertAmount)
                || string.IsNullOrEmpty(FromConvertType)
                || string.IsNullOrEmpty(ToConvertType))
                return;

            var conversionTask = _conversionCalculator.Convert(PreConvertAmount, FromConvertType, ToConvertType);
            conversionTask.GetAwaiter().OnCompleted(() => PostConvertAmount = conversionTask.Result);
        }
    }

    enum SelectedCurrencyType
    {
        None,
        FromCurrency,
        ToCurrency
    }
}
