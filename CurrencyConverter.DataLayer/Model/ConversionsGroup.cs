using System;
using System.Collections.Generic;
using MoreLinq;
using Realms;
using SQLite;

namespace CurrencyConverter.DataLayer.Model
{
    public class ConversionsGroup: RealmObject, IDeepCloneable<ConversionsGroup>
    {
        [Realms.PrimaryKey, SQLite.PrimaryKey]
        public string Id { get; set; }
        public DateTimeOffset Date { get; set; }
        public string BaseConversionName { get; set; }
        //The realm getter handles this
        public IList<ConversionRate> ConversionRates { get; }

        public ConversionsGroup DeepClone(int innerDepth)
        {
            var group =
                new ConversionsGroup
                {
                    Id = Id,
                    Date = Date,
                    BaseConversionName = BaseConversionName,
                };

            if (innerDepth != 0)
                ConversionRates.ForEach(
                    cr => group.ConversionRates.Add(cr.DeepClone(innerDepth - 1)));

            return group;
        }

        public override string ToString()
        {
            return $"{nameof(Id)}: {Id}, {nameof(Date)}: {Date}, {nameof(BaseConversionName)}: {BaseConversionName}, {nameof(ConversionRates)}: {ConversionRates}";
        }

        protected bool Equals(ConversionsGroup other)
        {
            return string.Equals(Id, other.Id) && Date.Equals(other.Date) && string.Equals(BaseConversionName, other.BaseConversionName) && Equals(ConversionRates, other.ConversionRates);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((ConversionsGroup) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (Id != null ? Id.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ Date.GetHashCode();
                hashCode = (hashCode * 397) ^ (BaseConversionName != null ? BaseConversionName.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (ConversionRates != null ? ConversionRates.GetHashCode() : 0);
                return hashCode;
            }
        }
    }
}