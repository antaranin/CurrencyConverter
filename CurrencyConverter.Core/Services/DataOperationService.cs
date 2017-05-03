using System;
using CurrencyConverter.DataLayer.IRepositories;
using CurrencyConverter.DataLayer.Repositories;

namespace CurrencyConverter.Core.Services
{
    public class DataOperationService: IDataOperationService
    {
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
            return new DataOperation();
        }
    }
}