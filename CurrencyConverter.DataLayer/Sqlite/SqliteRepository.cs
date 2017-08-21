using SQLite;

namespace CurrencyConverter.DataLayer.Sqlite
{
    public class SqliteRepository
    {
        protected readonly SQLiteConnection Database;

        public SqliteRepository(SQLiteConnection database)
        {
            Database = database;
        }
    }
}