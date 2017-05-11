using CurrencyConverter.DataLayer.IRepositories;

namespace CurrencyConverter.DataLayer.Realm
{
    public class RealmConversionRepository: IConversionRepository
    {
        private readonly Realms.Realm _realm;
        public RealmConversionRepository(Realms.Realm realm)
        {
            _realm = realm;
        }
    }
}