using System;
using CurrencyConverter.DataLayer.IRepositories;

namespace CurrencyConverter.Core.Services
{
    public interface IDataOperationService
    {
        void RunOperation(Action<IDataOperation> dataOperation);

        void RunTransactionOperation(Action<IDataOperation> transactionOperation);

        //IDataOperation CreateOperation();
    }

}