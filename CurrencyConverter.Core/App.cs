using System.Linq;
using CurrencyConverter.Core.Services;
using CurrencyConverter.Core.ViewModels;
using MvvmCross.Platform;
using MvvmCross.Platform.IoC;

namespace CurrencyConverter.Core
{
    public class App : MvvmCross.Core.ViewModels.MvxApplication
    {
        public override void Initialize()
        {
            CreatableTypes()
                .EndingWith("Service")
                .AsInterfaces()
                .RegisterAsLazySingleton();

            Mvx.RegisterSingleton<IConversionCalculator>(new LocalConversionCalculatorService());

            RegisterAppStart<ViewModels.FirstViewModel>();
        }
    }
}
