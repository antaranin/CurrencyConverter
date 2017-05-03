using System;
using System.Linq.Expressions;

namespace CurrencyConverter.DataLayer.IRepositories
{
    public interface IDataOperationService
    {
        void RunOperation(Action<IDataOperation> dataOperation);

        void RunTransactionOperation(Action<IDataOperation> transactionOperation);

        //IDataOperation CreateOperation();
    }

}