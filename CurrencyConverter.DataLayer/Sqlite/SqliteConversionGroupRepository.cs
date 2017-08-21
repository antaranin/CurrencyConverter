using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using CurrencyConverter.DataLayer.IRepositories;
using CurrencyConverter.DataLayer.Model;
using CurrencyConverter.DataLayer.Sqlite.Model;
using MoreLinq;
using SQLite;

namespace CurrencyConverter.DataLayer.Sqlite
{
    public class SqliteConversionGroupRepository: SqliteRepository, IConversionsGroupRepository
    {
        public SqliteConversionGroupRepository(SQLiteConnection database)
            : base(database)
        {
        }

        public void Insert(ConversionsGroup conversionsGroup)
        {
            var mapperConfig =
                new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<ConversionsGroup, SqliteConversionGroup>();
                    cfg.CreateMap<ConversionRate, SqliteConversionRate>();
                });

            IMapper mapper = new Mapper(mapperConfig);
            var sqliteGroup = mapper.Map<SqliteConversionGroup>(conversionsGroup);
            var sqliteRates = mapper.Map<IList<ConversionRate>, List<SqliteConversionRate>>(conversionsGroup.ConversionRates);
            sqliteRates.ForEach(it => it.ConversionGroupId = conversionsGroup.Id);


            Database.Insert(sqliteGroup);
            new ConversionRateDatabaseHelper().InsertRates(sqliteRates, Database);
        }

        public ConversionsGroup FindLatest(int depth)
        {
            var groupTable = Database.Table<SqliteConversionGroup>();
            if (!groupTable.Any())
                return null;
            var sqliteGroup = groupTable.MaxBy(x => x.Date);
            var mapperConfig =
                new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<SqliteConversionGroup, ConversionsGroup>();
                    cfg.CreateMap<SqliteConversionRate, ConversionRate>();
                });

            IMapper mapper = new Mapper(mapperConfig);
            var group = mapper.Map<ConversionsGroup>(sqliteGroup);

            if (depth == 0)
                return group;

            var sqliteRates =
                new ConversionRateDatabaseHelper().ExtractRatesByGroupId(group.Id, Database);
            var rates = mapper.Map<IList<SqliteConversionRate>, List<ConversionRate>>(sqliteRates);
            rates.ForEach(rate => group.ConversionRates.Add(rate));
            return group;
        }
    }
}