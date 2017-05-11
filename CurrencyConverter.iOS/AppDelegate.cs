using System;
using MvvmCross.Core.ViewModels;
using MvvmCross.iOS.Platform;
using MvvmCross.Platform;
using Foundation;
using CurrencyConverter.Core.Services;
using UIKit;
using System.IO;

namespace CurrencyConverter.iOS
{
	[Register("AppDelegate")]
	public partial class AppDelegate : MvxApplicationDelegate
	{
		public override UIWindow Window
		{
			get;
			set;
		}

		public override bool FinishedLaunching(UIApplication app, NSDictionary options)
		{
			Window = new UIWindow(UIScreen.MainScreen.Bounds);

			var setup = new Setup(this, Window);
			setup.Initialize();

			var startup = Mvx.Resolve<IMvxAppStart>();
			Mvx.RegisterSingleton<IFileHelper>(new IosFileHelper());
			startup.Start();

			Window.MakeKeyAndVisible();

			return true;
		}
	}
}

public class IosFileHelper : IFileHelper
{
	public string GetLocalFilePath(string filename)
	{
		string docFolder = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
		string libFolder = Path.Combine(docFolder, "..", "Library", "Databases");

		if (!Directory.Exists(libFolder))
		{
			Directory.CreateDirectory(libFolder);
		}

		return Path.Combine(libFolder, filename);
	}
}
