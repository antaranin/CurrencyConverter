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
        protected Realm Realm;
        public Repository(Realm realm)
        {
            Ensure.That(realm).IsNotNull();
            Realm = realm;
        }

        public T GetSingle(Expression<Func<T, bool>> whereClause, int depth = 0)
        {
            Ensure.That(Realm.IsClosed).IsFalse();
            return Realm.All<T>().Where(whereClause).First().DeepClone(depth);
        }

        public T GetByPrimaryKey(string primaryKey, int depth = 0)
        {
            Ensure.That(Realm.IsClosed).IsFalse();
            return Realm.Find<T>(primaryKey).DeepClone(depth);
        }

        public T GetByPrimaryKey(int primaryKey, int depth = 0)
        {
            Ensure.That(Realm.IsClosed).IsFalse();
            return Realm.Find<T>(primaryKey).DeepClone(depth);
        }

        public ImmutableList<T> GetMultiple(Expression<Func<T, bool>> whereClause, int depth = 0)
        {
            Ensure.That(Realm.IsClosed).IsFalse();
            return Realm.All<T>()
                .Where(whereClause)
                .Select(t => t.DeepClone(depth))
                .ToImmutableList();
        }

        public ImmutableList<T> GetAll(int depth = 0)
        {
            Ensure.That(Realm.IsClosed).IsFalse();
            return Realm.All<T>()
                .Select(t => t.DeepClone(depth))
                .ToImmutableList();
        }

        public void Upsert(T insertObject)
        {
            Ensure.That(Realm.IsClosed).IsFalse();
            Realm.Add(insertObject.DeepClone(-1), true);
        }

        public void Update(Action<T> updateOperation, Expression<Func<T, bool>> whereClause)
        {
            Ensure.That(Realm.IsClosed).IsFalse();
            var itemsToUpdate = Realm.All<T>()
                .Where(whereClause);
            foreach (var itemToUpdate in itemsToUpdate)
            {
                updateOperation(itemToUpdate);
            }
        }

        public void Delete(Expression<Func<T, bool>> whereClause)
        {
            Ensure.That(Realm.IsClosed).IsFalse();
            IQueryable<T> itemsToDelete = Realm.All<T>().Where(whereClause)
                .Where(whereClause);
            Realm.RemoveRange(itemsToDelete);
        }
    }
}