using EnsureThat;
using UIKit;

namespace CurrencyConverter.iOS
{
	public class Application
	{
		// This is the main entry point of the application.
		static void Main(string[] args)
		{
			// if you want to use a different Application Delegate class from "AppDelegate"
			// you can specify it here.
			#if !DEBUG
				Ensure.Off();
			#endif
			UIApplication.Main(args, null, "AppDelegate");
		}
	}
}
