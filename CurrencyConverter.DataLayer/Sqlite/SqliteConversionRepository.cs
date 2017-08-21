using CurrencyConverter.DataLayer.IRepositories;
using SQLite;

namespace CurrencyConverter.DataLayer.Sqlite
{
    public class SqliteConversionRepository: SqliteRepository, IConversionRepository
    {
        public SqliteConversionRepository(SQLiteConnection database)
            : base(database)
        {
        }
    }
}