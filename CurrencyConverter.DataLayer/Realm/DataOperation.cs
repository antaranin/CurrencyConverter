using System;
using CurrencyConverter.Core.Services;
using CurrencyConverter.DataLayer.IRepositories;
using EnsureThat;
using Realms;

namespace CurrencyConverter.DataLayer.Repositories
{
    public class DataOperation: IDataOperation
    {
        private readonly Realm _realm = Realm.GetInstance();
        private Transaction _transaction;

        public bool IsTransaction { get { return _transaction != null; } }

        private Lazy<IConversionRepository> _conversionRepository;

        public IConversionRepository ConversionRepository
        {
            get { return _conversionRepository.Value; }
        }

        private Lazy<IConversionsGroupRepository> _conversionsGroupRepository;
        public IConversionsGroupRepository ConversionsGroupRepository
        {
            get { return _conversionsGroupRepository.Value; }
        }

        public DataOperation()
        {
            _conversionRepository =
                new Lazy<IConversionRepository>(() => new RealmConversionRepository(_realm));
            _conversionsGroupRepository =
                new Lazy<IConversionsGroupRepository>(
                    () => new RealmConversionGroupRepository(_realm));
        }

        public void RunAsTransaction()
        {
            _transaction = _realm.BeginWrite();
        }

        public void Dispose()
        {
            Ensure.That(_realm.IsClosed).IsFalse();

            if (IsTransaction)
            {
                _transaction.Commit();
                _transaction.Dispose();
            }
            _realm.Dispose();
        }
    }
}