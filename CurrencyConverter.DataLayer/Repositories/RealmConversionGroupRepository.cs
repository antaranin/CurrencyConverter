using System.Linq;
using CurrencyConverter.DataLayer.IRepositories;
using CurrencyConverter.DataLayer.Model;
using MoreLinq;
using Realms;

namespace CurrencyConverter.DataLayer.Repositories
{
    public class RealmConversionGroupRepository
        : Repository<ConversionsGroup>, IConversionsGroupRepository
    {
        public RealmConversionGroupRepository(Realm realm) : base(realm)
        {
        }

        public ConversionsGroup FindLatest(int depth)
        {
            return Realm.All<ConversionsGroup>().MaxBy(m => m.Date).DeepClone(depth);
        }
    }
}