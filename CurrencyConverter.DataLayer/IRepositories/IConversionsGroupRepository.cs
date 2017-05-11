using CurrencyConverter.DataLayer.Model;

namespace CurrencyConverter.DataLayer.IRepositories
{
    public interface IConversionsGroupRepository
    {
        void Insert(ConversionsGroup conversionsGroup);

        ConversionsGroup FindLatest(int depth);
    }
}