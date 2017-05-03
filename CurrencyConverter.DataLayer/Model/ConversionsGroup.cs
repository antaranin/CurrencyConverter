using System;
using System.Collections.Generic;
using System.Linq;
using MoreLinq;
using Realms;

namespace CurrencyConverter.DataLayer.Model
{
    public class ConversionsGroup: RealmObject, IDeepCloneable<ConversionsGroup>
    {
        public DateTimeOffset Date { get; set; }
        public string BaseConversionName { get; set; }
        //The realm getter handles this
        public IList<ConversionRate> ConversionRates { get; }

        public ConversionsGroup DeepClone(int innerDepth)
        {
            var group =
                new ConversionsGroup
                {
                    Date = Date,
                    BaseConversionName = BaseConversionName,
                };
            if (innerDepth > 0)
                ConversionRates.ForEach(
                    cr => group.ConversionRates.Add(cr.DeepClone(innerDepth - 1)));
            return group;
        }
    }
}