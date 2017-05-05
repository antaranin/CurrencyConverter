using Android.App;
using Android.OS;
using Android.Widget;
using CurrencyConverter.Core.ViewModels;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Binding.Droid.Views;
using MvvmCross.Droid.Views;

namespace CurrencyConverter.Droid.Views
{
    [Activity(Label = "View for FirstViewModel")]
    public class FirstView : MvxActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.FirstView);
            CreateBindings();
        }

        private void CreateBindings()
        {
            var set = this.CreateBindingSet<FirstView, FirstViewModel>();

            var toSpinner = FindViewById<MvxSpinner>(Resource.Id.to_spinner);
            var fromSpinner = FindViewById<MvxSpinner>(Resource.Id.from_spinner);
            var resultTextView = FindViewById<TextView>(Resource.Id.result_tv);
            var convertAmountEt = FindViewById<EditText>(Resource.Id.convert_amount_et);

            set.Bind(toSpinner)
                .For(v => v.ItemsSource)
                .To(vm => vm.ConversionCurrencies);

            set.Bind(fromSpinner)
                .For(v => v.ItemsSource)
                .To(vm => vm.ConversionCurrencies);

            set.Bind(resultTextView)
                .For(v => v.Text)
                .To(vm => vm.ProcessedPostConvertAmount)
                .OneWay();

            set.Bind(convertAmountEt)
                .For(v => v.Text)
                .To(vm => vm.PreConvertAmount)
                .OneWayToSource();

            set.Bind(toSpinner)
                .For(v => v.SelectedItem)
                .To(vm => vm.ToConvertType);

            set.Bind(fromSpinner)
                .For(v => v.SelectedItem)
                .To(vm => vm.FromConvertType);

/*            set.Bind(toSpinner)
                .For(v => v.HandleItemSelected)
                .To("StopSelectingCurrency");

            set.Bind(fromSpinner)
                .For(v => v.HandleItemSelected)
                .To("StopSelectingCurrency");*/

            set.Apply();
        }
    }
}
