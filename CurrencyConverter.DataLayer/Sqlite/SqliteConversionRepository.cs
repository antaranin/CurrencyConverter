using System;
using System.Collections.Immutable;
using System.Linq.Expressions;
using CurrencyConverter.Core.Services;
using CurrencyConverter.DataLayer.Model;
using MoreLinq;
using SQLite;

namespace CurrencyConverter.DataLayer.Sqlite
{
    public class SqliteConversionRepository: SqliteRepository, IConversionRepository
    {
        public SqliteConversionRepository(SqliteDataOperation dataProvider, SQLiteConnection database)
            : base(dataProvider, database)
        {
        }

        public ConversionRate GetSingle(Expression<Func<ConversionRate, bool>> whereClause, int depth = 0)
        {
            return Database.Find(whereClause);
        }

        public ConversionRate GetByPrimaryKey(string primaryKey, int depth = 0)
        {
            return Database.Get<ConversionRate>(primaryKey);
        }

        public ConversionRate GetByPrimaryKey(int primaryKey, int depth = 0)
        {
            throw new NotSupportedException("Conversion rate doesn't have an integer primary key");
        }

        public ImmutableList<ConversionRate> GetMultiple(Expression<Func<ConversionRate, bool>> whereClause, int depth = 0)
        {
            return Database.Table<ConversionRate>().Where(whereClause).ToImmutableList();
        }

        public ImmutableList<ConversionRate> GetAll(int depth = 0)
        {
            return Database.Table<ConversionRate>().ToImmutableList();
        }

        public void Upsert(ConversionRate insertObject)
        {
            Database.InsertOrReplace(insertObject, typeof(ConversionRate));
        }

        public void Update(Action<ConversionRate> updateOperation, Expression<Func<ConversionRate, bool>> whereClause)
        {
            var item = Database.Find(whereClause);
            updateOperation(item);
            Database.Update(item);
        }

        public void Delete(Expression<Func<ConversionRate, bool>> whereClause)
        {
            var items = Database.Table<ConversionRate>().Where(whereClause);
            items.ForEach(item =>  Database.Delete<ConversionRate>(item.Id));
        }
    }
}