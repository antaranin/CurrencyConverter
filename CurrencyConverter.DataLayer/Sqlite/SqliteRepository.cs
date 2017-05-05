using SQLite;

namespace CurrencyConverter.DataLayer.Sqlite
{
    public class SqliteRepository
    {
        protected readonly SqliteDataOperation DataProvider;
        protected readonly SQLiteConnection Database;

        public SqliteRepository(SqliteDataOperation dataProvider, SQLiteConnection database)
        {
            DataProvider = dataProvider;
            Database = database;
        }
    }
}