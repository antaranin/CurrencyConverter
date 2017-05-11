using System;

namespace CurrencyConverter.DataLayer.IRepositories
{
    public interface IDataOperation: IDisposable
    {
        bool IsTransaction { get; }

        IConversionRepository ConversionRepository{ get; }

        IConversionsGroupRepository ConversionsGroupRepository { get; }

        void RunAsTransaction();
    }
}