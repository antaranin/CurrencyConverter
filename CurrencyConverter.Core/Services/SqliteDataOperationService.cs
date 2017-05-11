using System;
using CurrencyConverter.DataLayer.IRepositories;
using CurrencyConverter.DataLayer.Sqlite;

namespace CurrencyConverter.Core.Services
{
    public class SqliteDataOperationService: IDataOperationService
    {
        private readonly IFileHelper _fileHelper;
        public SqliteDataOperationService(IFileHelper fileHelper)
        {
            _fileHelper = fileHelper;
        }

        public void RunOperation(Action<IDataOperation> dataOperation)
        {
            using (var operation = CreateDataOperation())
            {
                dataOperation(operation);
            }
        }

        public void RunTransactionOperation(Action<IDataOperation> transactionOperation)
        {
            using (var operation = CreateDataOperation())
            {
                operation.RunAsTransaction();
                transactionOperation(operation);
            }
        }

        private IDataOperation CreateDataOperation()
        {
            return new SqliteDataOperation(_fileHelper.GetLocalFilePath("Database3"));
        }
    }
}