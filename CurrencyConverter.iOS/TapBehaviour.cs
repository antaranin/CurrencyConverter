using System.Windows.Input;
using UIKit;

namespace CurrencyConverter.iOS
{


    public class TapBehaviour
    {
        public ICommand Command { get;set; }

        public TapBehaviour(UIView view)
        {
            var tap = new UITapGestureRecognizer(() =>
            {
                var command = Command;
                command?.Execute(null);
            });
            view.AddGestureRecognizer(tap);
        }
    }

    public static class BehaviourExtensions
    {
        public static TapBehaviour Tap(this UIView view)
        {
            return new TapBehaviour(view);
        }
    }
}