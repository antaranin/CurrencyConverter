using System;
using System.Collections.Immutable;
using System.Linq.Expressions;

namespace CurrencyConverter.DataLayer.IRepositories
{
    public interface IRepository<T>
    {
        T GetSingle(Expression<Func<T, bool>> whereClause, int depth = 0);

        T GetByPrimaryKey(string primaryKey, int depth = 0);

        T GetByPrimaryKey(int primaryKey, int depth = 0);

        ImmutableList<T> GetMultiple(Expression<Func<T, bool>> whereClause, int depth = 0);

        ImmutableList<T> GetAll(int depth = 0);

        void Upsert(T insertObject);

        void Update(Action<T> updateOperation, Expression<Func<T, bool>> whereClause);

        void Delete(Expression<Func<T, bool>> whereClause);
    }
}