using System;

namespace CurrencyConverter.DataLayer.Sqlite.Model
{
    public class SqliteConversionGroup
    {

        public string Id { get; set; }
        public DateTimeOffset Date { get; set; }
        public string BaseConversionName { get; set; }
    }
}