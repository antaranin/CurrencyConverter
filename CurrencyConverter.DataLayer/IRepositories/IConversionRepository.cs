using CurrencyConverter.DataLayer.IRepositories;
using CurrencyConverter.DataLayer.Model;

namespace CurrencyConverter.Core.Services
{
    public interface IConversionRepository : IRepository<ConversionRate>
    {

    }
}