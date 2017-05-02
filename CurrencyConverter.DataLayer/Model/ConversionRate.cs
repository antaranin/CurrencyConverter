using Realms;

namespace CurrencyConverter.DataLayer.Model
{
    public class ConversionRate: RealmObject, IDeepCloneable<ConversionRate>
    {
        public string ConversionName { get; set; }
        public double BaseConversionRate { get; set; }

        public ConversionRate DeepClone(int innerDepth)
        {
            return new ConversionRate
            {
                ConversionName = ConversionName,
                BaseConversionRate = BaseConversionRate
            };
        }
    }
}