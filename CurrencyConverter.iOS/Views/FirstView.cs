using System.Diagnostics;
using CurrencyConverter.Core.Converters;
using CurrencyConverter.Core.ViewModels;
using FluentAssertions;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Binding.iOS.Views;
using MvvmCross.iOS.Views;
using MvvmCross.Plugins.Visibility;

namespace CurrencyConverter.iOS.Views
{
    public partial class FirstView : MvxViewController
    {
        public FirstView() : base("FirstView", null)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            var set = this.CreateBindingSet<FirstView, FirstViewModel>();
            var model = new MvxPickerViewModel(CurrencyPicker);
            CurrencyPicker.Model = model;

            set.Bind(AmountToConvertTF)
                .For(v => v.Text)
                .To(vm => vm.PreConvertAmount)
                .OneWayToSource();

            set.Bind(model)
                .For(v => v.ItemsSource)
                .To(vm => vm.ConversionCurrencies);

            set.Bind(model)
                .For(v => (string) v.SelectedItem)
                .To(vm => vm.CurrentSelectedCurrency);

            set.Bind(OverlayView)
                .For(v => v.Hidden)
                .To(vm => vm.ShowCurrencyPicker)
                .WithConversion(new MvxVisibilityValueConverter())
                .OneWay();

            set.Bind(ClosePickerButton)
                .To("StopSelectingCurrency");

            set.Bind(FromLabel)
                .For(v => v.Text)
                .To(vm => vm.FromConvertType)
                .WithConversion(new PrefixValueConverter(), "From: ")
                .OneWay();

            set.Bind(ToLabel)
                .For(v => v.Text)
                .To(vm => vm.ToConvertType)
                .WithConversion(new PrefixValueConverter(), "To: ")
                .OneWay();

            set.Bind(FromLabel.Tap())
                .For(v => v.Command)
                .To("SelectFromCurrencyType");

            set.Bind(ToLabel.Tap())
                .For(v => v.Command)
                .To("SelectToCurrencyType");

            set.Bind(ConvertedAmountLabel)
                .For(v => v.Text)
                .To(vm => vm.ProcessedPostConvertAmount)
                .OneWay();

            set.Apply();
        }
    }
}
