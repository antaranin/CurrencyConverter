using CurrencyConverter.DataLayer.Model;

namespace CurrencyConverter.DataLayer.IRepositories
{
    public interface IConversionsGroupRepository: IRepository<ConversionsGroup>
    {
        ConversionsGroup FindLatest(int depth);
    }
}