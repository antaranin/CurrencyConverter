using System.Threading.Tasks;
using CurrencyConverter.DataLayer.Model;

namespace CurrencyConverter.Core.Services
{
    public interface IConversionGroupProviderService
    {
        Task<ConversionsGroup> ProvideConversionGroup();
    }
}