using System.Diagnostics;
using System.Linq;
using CurrencyConverter.DataLayer.IRepositories;
using CurrencyConverter.DataLayer.Model;
using MoreLinq;

namespace CurrencyConverter.DataLayer.Realm
{
    public class RealmConversionGroupRepository
        :  IConversionsGroupRepository
    {
        private readonly Realms.Realm _realm;
        public RealmConversionGroupRepository(Realms.Realm realm)
        {
            _realm = realm;
        }


        public void Insert(ConversionsGroup conversionsGroup)
        {
            _realm.Add(conversionsGroup.DeepClone(-1));
        }

        public ConversionsGroup FindLatest(int depth)
        {
            var query = _realm.All<ConversionsGroup>();
            Debug.WriteLine("Query results => ");
            query.ForEach(x => Debug.WriteLine("Res => " + x));
            return !query.Any() ? null : query.MaxBy(m => m.Date).DeepClone(depth);
        }
    }
}