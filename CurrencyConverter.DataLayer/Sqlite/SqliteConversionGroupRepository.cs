using System;
using System.Collections.Immutable;
using System.Linq.Expressions;
using CurrencyConverter.DataLayer.IRepositories;
using CurrencyConverter.DataLayer.Model;
using SQLite.Net;
using SQLiteConnection = SQLite.SQLiteConnection;

namespace CurrencyConverter.DataLayer.Sqlite
{
    public class SqliteConversionGroupRepository: SqliteRepository, IConversionsGroupRepository
    {
        public SqliteConversionGroupRepository(SqliteDataOperation dataProvider, SQLiteConnection database)
            : base(dataProvider, database)
        {
        }

        public ConversionsGroup GetSingle(Expression<Func<ConversionsGroup, bool>> whereClause, int depth = 0)
        {
            throw new NotImplementedException();
        }

        public ConversionsGroup GetByPrimaryKey(string primaryKey, int depth = 0)
        {
            return
        }

        public ConversionsGroup GetByPrimaryKey(int primaryKey, int depth = 0)
        {
            throw new NotSupportedException("The ConversionsGroup does not have an integer primary key");
        }

        public ImmutableList<ConversionsGroup> GetMultiple(Expression<Func<ConversionsGroup, bool>> whereClause, int depth = 0)
        {
            throw new NotImplementedException();
        }

        public ImmutableList<ConversionsGroup> GetAll(int depth = 0)
        {
            throw new NotImplementedException();
        }

        public void Upsert(ConversionsGroup insertObject)
        {
            throw new NotImplementedException();
        }

        public void Update(Action<ConversionsGroup> updateOperation, Expression<Func<ConversionsGroup, bool>> whereClause)
        {
            throw new NotImplementedException();
        }

        public void Delete(Expression<Func<ConversionsGroup, bool>> whereClause)
        {
            throw new NotImplementedException();
        }

        public ConversionsGroup FindLatest(int depth)
        {
            throw new NotImplementedException();
        }
    }
}