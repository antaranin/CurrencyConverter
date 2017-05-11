using System;
using CurrencyConverter.DataLayer.IRepositories;

namespace CurrencyConverter.DataLayer.Sqlite
{
    public class SqliteDataOperation : IDataOperation
    {
        private readonly SqliteDatabase _database;
        private bool _isInTransaction;

        public bool IsTransaction
        {
            get { return _isInTransaction; }
        }

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

        public SqliteDataOperation(string databasePath)
        {
            _database = SqliteDatabase.GetInstance(databasePath);
            _conversionRepository =
                new Lazy<IConversionRepository>(
                    () => new SqliteConversionRepository(this, _database.Database));

            _conversionsGroupRepository = new Lazy<IConversionsGroupRepository>(
                () => new SqliteConversionGroupRepository(this, _database.Database));
        }


        public void RunAsTransaction()
        {
            _database.Database.BeginTransaction();
            _isInTransaction = true;

        }

        public void Dispose()
        {
            if(IsTransaction)
                _database.Database.Commit();
        }
    }
}