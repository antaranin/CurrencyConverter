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
            var query = Realm.All<ConversionsGroup>();
            return !query.Any() ? null : query.MaxBy(m => m.Date).DeepClone(depth);
        }
    }
}