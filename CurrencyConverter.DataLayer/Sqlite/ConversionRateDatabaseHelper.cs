using System.Collections.Generic;
using System.Linq;
using CurrencyConverter.DataLayer.Model;
using CurrencyConverter.DataLayer.Sqlite.Model;
using SQLite;

namespace CurrencyConverter.DataLayer.Sqlite
{
    internal class ConversionRateDatabaseHelper
    {
        internal void InsertRates(IList<SqliteConversionRate> conversionRates, SQLiteConnection database)
        {
            database.InsertAll(conversionRates);
        }

        internal IList<SqliteConversionRate> ExtractRatesByGroupId(string groupId, SQLiteConnection database)
        {
            return database.Table<SqliteConversionRate>()
                .Where(x => x.ConversionGroupId == groupId)
                .ToList();
        }
    }
}