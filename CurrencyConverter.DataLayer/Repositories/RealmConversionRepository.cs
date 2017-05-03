using CurrencyConverter.Core.Services;
using CurrencyConverter.DataLayer.Model;
using Realms;

namespace CurrencyConverter.DataLayer.Repositories
{
    public class RealmConversionRepository: Repository<ConversionRate>, IConversionRepository
    {
        public RealmConversionRepository(Realm realm) : base(realm)
        {
        }
    }
}