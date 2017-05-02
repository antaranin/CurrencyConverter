using System;
using System.Collections.Generic;
using System.Linq;
using Realms;

namespace CurrencyConverter.DataLayer.Model
{
    public class ConversionsGroup: RealmObject, IDeepCloneable<ConversionsGroup>
    {
        public DateTime Date { get; set; }
        public string BaseConversionName { get; set; }
        public IList<ConversionRate> ConversionRates { get; set; }

        public ConversionsGroup DeepClone(int innerDepth)
        {
            return new ConversionsGroup
            {
                Date = Date,
                BaseConversionName = BaseConversionName,
                ConversionRates = innerDepth > 0
                    ? ConversionRates.Select(cr => cr.DeepClone(innerDepth - 1)).ToList()
                    : null
            };
        }
    }
}