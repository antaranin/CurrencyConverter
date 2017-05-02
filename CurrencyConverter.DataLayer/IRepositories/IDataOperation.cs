using System;
using CurrencyConverter.Core.Services;

namespace CurrencyConverter.DataLayer.IRepositories
{
    public interface IDataOperation: IDisposable
    {
        IConversionRepository ConversionRepository{ get; }
    }
}