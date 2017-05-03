using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using CurrencyConverter.DataLayer.Model;
using MvvmCross.Platform;
using Newtonsoft.Json;

namespace CurrencyConverter.Core.Services
{
    public class ConversionGroupProviderService: IConversionGroupProviderService
    {
        public async Task<ConversionsGroup> ProvideConversionGroup()
        {
            try
            {
                HttpClient client = new HttpClient();
                var uri = new Uri("http://api.fixer.io/latest");
                var message = await client.GetAsync(uri);
                var jsonString = await message.Content.ReadAsStringAsync();
                Debug.WriteLine("Json string => " + jsonString);
                var netConversionGroup = JsonConvert.DeserializeObject<NetConversionGroup>(jsonString);

                return ConvertToLocalConversionsGroup(netConversionGroup);

            }
            catch (Exception e)
            {
                Debug.WriteLine("Exception while making online request");
                Debug.WriteLine("E => " + e);
            }
            return null;
        }

        private ConversionsGroup ConvertToLocalConversionsGroup(
            NetConversionGroup netConversionGroup)
        {
            if (netConversionGroup == null)
                return null;

            var group =  new ConversionsGroup
            {
                BaseConversionName = netConversionGroup.@base,
                Date = netConversionGroup.date
            };

            var rates = netConversionGroup.rates;
            foreach (PropertyInfo convRate in typeof(Rates).GetProperties())
            {
                var value = (double) convRate.GetValue(rates, null);
                var name = convRate.Name;
                group.ConversionRates.Add(new ConversionRate
                {
                    BaseConversionRate = value,
                    ConversionName = name
                });
            }
            return group;
        }
    }

        class Rates
        {
            public double AUD { get; set; }
            public double BGN { get; set; }
            public double BRL { get; set; }
            public double CAD { get; set; }
            public double CHF { get; set; }
            public double CNY { get; set; }
            public double CZK { get; set; }
            public double DKK { get; set; }
            public double GBP { get; set; }
            public double HKD { get; set; }
            public double HRK { get; set; }
            public double HUF { get; set; }
            public double IDR { get; set; }
            public double ILS { get; set; }
            public double INR { get; set; }
            public double JPY { get; set; }
            public double KRW { get; set; }
            public double MXN { get; set; }
            public double MYR { get; set; }
            public double NOK { get; set; }
            public double NZD { get; set; }
            public double PHP { get; set; }
            public double PLN { get; set; }
            public double RON { get; set; }
            public double RUB { get; set; }
            public double SEK { get; set; }
            public double SGD { get; set; }
            public double THB { get; set; }
            public double TRY { get; set; }
            public double USD { get; set; }
            public double ZAR { get; set; }
        }

        class NetConversionGroup
        {
            public string @base { get; set; }
            public DateTime date { get; set; }
            public Rates rates { get; set; }
        }
}