// WARNING
//
// This file has been generated automatically by Xamarin Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace CurrencyConverter.iOS.Views
{
    [Register ("FirstView")]
    partial class FirstView
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField AmountToConvertTF { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton ClosePickerButton { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton ConvertBtn { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel ConvertedAmountLabel { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIPickerView CurrencyPicker { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel FromLabel { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIView OverlayView { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel ToLabel { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (AmountToConvertTF != null) {
                AmountToConvertTF.Dispose ();
                AmountToConvertTF = null;
            }

            if (ClosePickerButton != null) {
                ClosePickerButton.Dispose ();
                ClosePickerButton = null;
            }

            if (ConvertBtn != null) {
                ConvertBtn.Dispose ();
                ConvertBtn = null;
            }

            if (ConvertedAmountLabel != null) {
                ConvertedAmountLabel.Dispose ();
                ConvertedAmountLabel = null;
            }

            if (CurrencyPicker != null) {
                CurrencyPicker.Dispose ();
                CurrencyPicker = null;
            }

            if (FromLabel != null) {
                FromLabel.Dispose ();
                FromLabel = null;
            }

            if (OverlayView != null) {
                OverlayView.Dispose ();
                OverlayView = null;
            }

            if (ToLabel != null) {
                ToLabel.Dispose ();
                ToLabel = null;
            }
        }
    }
}