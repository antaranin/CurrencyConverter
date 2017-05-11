using Realms;
using SQLite;

namespace CurrencyConverter.DataLayer.Model
{
    public class ConversionRate: RealmObject, IDeepCloneable<ConversionRate>
    {
        [Realms.PrimaryKey]
        public string Id { get; set; }
        public string ConversionName { get; set; }
        public double BaseConversionRate { get; set; }

        public ConversionRate DeepClone(int innerDepth)
        {
            return new ConversionRate
            {
                Id = Id,
                ConversionName = ConversionName,
                BaseConversionRate = BaseConversionRate
            };
        }

        public override string ToString()
        {
            return $"{nameof(Id)}: {Id}, {nameof(ConversionName)}: {ConversionName}, {nameof(BaseConversionRate)}: {BaseConversionRate}";
        }

        protected bool Equals(ConversionRate other)
        {
            return string.Equals(ConversionName, other.ConversionName) &&
                   BaseConversionRate.Equals(other.BaseConversionRate);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((ConversionRate) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((ConversionName != null ? ConversionName.GetHashCode() : 0) * 397) ^
                       BaseConversionRate.GetHashCode();
            }
        }
    }
}