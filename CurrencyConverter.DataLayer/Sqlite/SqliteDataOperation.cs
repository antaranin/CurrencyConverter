using CurrencyConverter.Core.Services;
using CurrencyConverter.DataLayer.IRepositories;
using Todo;

namespace CurrencyConverter.DataLayer.Sqlite
{
    public class SqliteDataOperation: IDataOperation
    {
        private readonly SqliteDatabase _database;

        public bool IsTransaction { get; }
        public IConversionRepository ConversionRepository { get; }
        public IConversionsGroupRepository ConversionsGroupRepository { get; }

        public SqliteDataOperation(IFileHelper fileHelper)
        {
            _database = SqliteDatabase.GetInstance(fileHelper.GetLocalFilePath("Database.db3"));
        }


        public void RunAsTransaction()
        {
            throw new System.NotImplementedException();
        }

        public void Dispose()
        {
            //Pass
        }
    }
}