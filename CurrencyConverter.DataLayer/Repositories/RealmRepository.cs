using System;
using System.Collections.Immutable;
using System.Linq;
using System.Linq.Expressions;
using CurrencyConverter.DataLayer.IRepositories;
using CurrencyConverter.DataLayer.Model;
using EnsureThat;
using Realms;

namespace CurrencyConverter.DataLayer.Repositories
{
    public class Repository<T> : IRepository<T> where T : RealmObject, IDeepCloneable<T>
    {
        protected Realm _realm;
        public Repository(Realm realm)
        {
            Ensure.That(realm).IsNotNull();
            _realm = realm;
        }

        public T GetSingle(Expression<Func<T, bool>> whereClause, int depth = 0)
        {
            return _realm.All<T>().Where(whereClause).First().DeepClone(depth);
        }

        public T GetByPrimaryKey(string primaryKey, int depth = 0)
        {
            return _realm.Find<T>(primaryKey).DeepClone(depth);
        }

        public T GetByPrimaryKey(int primaryKey, int depth = 0)
        {
            return _realm.Find<T>(primaryKey).DeepClone(depth);
        }

        public ImmutableList<T> GetMultiple(Expression<Func<T, bool>> whereClause, int depth = 0)
        {
            return _realm.All<T>()
                .Where(whereClause)
                .Select(t => t.DeepClone(depth))
                .ToImmutableList();
        }

        public ImmutableList<T> GetAll(int depth = 0)
        {
            return _realm.All<T>()
                .Select(t => t.DeepClone(depth))
                .ToImmutableList();
        }

        public void Upsert(T insertObject)
        {
            _realm.Add(insertObject, true);
        }

        public void Update(Action<T> updateOperation, Expression<Func<T, bool>> whereClause)
        {
            var itemsToUpdate = _realm.All<T>()
                .Where(whereClause);
            foreach (var itemToUpdate in itemsToUpdate)
            {
                updateOperation(itemToUpdate);
            }
        }

        public void Delete(Expression<Func<T, bool>> whereClause)
        {
            IQueryable<T> itemsToDelete = _realm.All<T>().Where(whereClause)
                .Where(whereClause);
            _realm.RemoveRange(itemsToDelete);

        }
    }
}