// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace CurrencyConverter.iOS.Views
{
	[Register("FirstView")]
	partial class FirstView
	{
		[Outlet]
		[GeneratedCode("iOS Designer", "1.0")]
		UIKit.UITextField AmountToConvertTF { get; set; }

		[Outlet]
		[GeneratedCode("iOS Designer", "1.0")]
		UIKit.UIButton ClosePickerButton { get; set; }

		[Outlet]
		[GeneratedCode("iOS Designer", "1.0")]
		UIKit.UILabel ConvertedAmountLabel { get; set; }

		[Outlet]
		[GeneratedCode("iOS Designer", "1.0")]
		UIKit.UIPickerView CurrencyPicker { get; set; }

		[Outlet]
		[GeneratedCode("iOS Designer", "1.0")]
		UIKit.UILabel FromLabel { get; set; }

		[Outlet]
		[GeneratedCode("iOS Designer", "1.0")]
		UIKit.UIView OverlayView { get; set; }

		[Outlet]
		[GeneratedCode("iOS Designer", "1.0")]
		UIKit.UILabel ToLabel { get; set; }

		void ReleaseDesignerOutlets()
		{
			if (AmountToConvertTF != null)
			{
				AmountToConvertTF.Dispose();
				AmountToConvertTF = null;
			}

			if (ClosePickerButton != null)
			{
				ClosePickerButton.Dispose();
				ClosePickerButton = null;
			}

			if (ConvertedAmountLabel != null)
			{
				ConvertedAmountLabel.Dispose();
				ConvertedAmountLabel = null;
			}

			if (CurrencyPicker != null)
			{
				CurrencyPicker.Dispose();
				CurrencyPicker = null;
			}

			if (FromLabel != null)
			{
				FromLabel.Dispose();
				FromLabel = null;
			}

			if (OverlayView != null)
			{
				OverlayView.Dispose();
				OverlayView = null;
			}

			if (ToLabel != null)
			{
				ToLabel.Dispose();
				ToLabel = null;
			}
		}
	}
}
