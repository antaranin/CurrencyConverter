using CurrencyConverter.DataLayer.IRepositories;
using CurrencyConverter.DataLayer.Model;
using Realms;

namespace CurrencyConverter.DataLayer.Repositories
{
    public class RealmConversionGroupRepository
        : Repository<ConversionsGroup>, IConversionsGroupRepository
    {
        public RealmConversionGroupRepository(Realm realm) : base(realm)
        {
        }
    }
}