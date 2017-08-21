using System;
using CurrencyConverter.DataLayer.IRepositories;

namespace CurrencyConverter.DataLayer.Sqlite
{
    public class SqliteDataOperation : IDataOperation
    {
        private readonly SqliteDatabase _database;

        public bool IsTransaction { get; private set; }

        private readonly Lazy<IConversionRepository> _conversionRepository;
        public IConversionRepository ConversionRepository => _conversionRepository.Value;

        private readonly Lazy<IConversionsGroupRepository> _conversionsGroupRepository;

        public IConversionsGroupRepository ConversionsGroupRepository => _conversionsGroupRepository.Value;

        public SqliteDataOperation(string databasePath)
        {
            _database = SqliteDatabase.GetInstance(databasePath);
            _conversionRepository =
                new Lazy<IConversionRepository>(
                    () => new SqliteConversionRepository(_database.Database));

            _conversionsGroupRepository = new Lazy<IConversionsGroupRepository>(
                () => new SqliteConversionGroupRepository(_database.Database));
        }


        public void RunAsTransaction()
        {
            _database.Database.BeginTransaction();
            IsTransaction = true;

        }

        public void Dispose()
        {
            if(IsTransaction)
                _database.Database.Commit();
        }
    }
}