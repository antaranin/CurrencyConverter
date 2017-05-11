using System;
using System.IO;
using Android.Content;
using CurrencyConverter.Core;
using CurrencyConverter.Core.Services;
using MvvmCross.Droid.Platform;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;
using MvvmCross.Platform.Platform;

namespace CurrencyConverter.Droid
{
	public class Setup : MvxAndroidSetup
	{
		public Setup(Context applicationContext) : base(applicationContext)
		{
		}

		protected override IMvxApplication CreateApp()
		{
			return new App();
		}

		protected override IMvxTrace CreateDebugTrace()
		{
			return new DebugTrace();
		}

		protected override void InitializeLastChance()
		{
			base.InitializeLastChance();
			Mvx.RegisterSingleton<IFileHelper>(new DroidFileHelper());
		}
	}
	public class DroidFileHelper : IFileHelper
	{
		public string GetLocalFilePath(string filename)
		{
			string path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
			return Path.Combine(path, filename);
		}
	}
}
