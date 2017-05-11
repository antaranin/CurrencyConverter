using SQLite;

namespace CurrencyConverter.DataLayer.Sqlite.Model
{
    public class SqliteConversionRate
    {
        [PrimaryKey]
        public string Id { get; set; }
        public string ConversionName { get; set; }
        public double BaseConversionRate { get; set; }
        public string ConversionGroupId { get; set; }

    }
}