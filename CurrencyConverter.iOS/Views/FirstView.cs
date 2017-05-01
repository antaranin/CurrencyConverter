using System.Windows.Input;
using CurrencyConverter.Core.ViewModels;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Binding.iOS.Views;
using MvvmCross.iOS.Views;
using MvvmCross.Plugins.Visibility;
using UIKit;

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
            set.Bind(model).For(v => v.ItemsSource).To(vm => vm.ConversionCurrencies);
            set.Bind(model).For(v => v.SelectedItem).To(vm => vm.CurrentSelectedCurrency);
            set.Bind(OverlayView)
                .For(v => v.Hidden)
                .To(vm => vm.ShowCurrencyPicker)
                .WithConversion(new MvxInvertedVisibilityValueConverter());
            set.Bind(ClosePickerButton)
                .To(vm => vm.StopSelectingCurrency);
            set.Bind(FromLabel).For(v => v.Text).To(vm => vm.FromConvertPosition);
            set.Bind(ToLabel).For(v => v.Text).To(vm => vm.ToConvertPosition);
            set.Bind(FromLabel.Tap()).For(v => v.Command).To(vm => vm.SelectFromCurrencyType);
            set.Bind(ToLabel.Tap()).For(v => v.Command).To(vm => vm.SelectToCurrencyType);
            set.Apply();
        }
    }
}
