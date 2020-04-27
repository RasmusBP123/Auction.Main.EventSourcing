using Dapper;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tactical.DDD;

namespace Auction.Persistence.Repositories
{
    internal class EventStoreRepository : IEventStore
    {

        private string EventStoreTableName = "Events";

        private static string EventStoreListOfColumnsInsert = "[Id], [CreatedAt], [Sequence], [Version], [Name], [AggregateId], [Data], [Aggregate]";

        private static readonly string EventStoreListOfColumnsSelect = $"{EventStoreListOfColumnsInsert},[Sequence]";

        private readonly JsonSerializerSettings _jsonSerializerSettings = new JsonSerializerSettings()
        {
            TypeNameHandling = TypeNameHandling.All,
            NullValueHandling = NullValueHandling.Ignore
        };

        private readonly DbEventStorage _context;

        public EventStoreRepository(DbEventStorage context)
        {
            _context = context;
        }

        public async Task<IReadOnlyCollection<IDomainEvent>> LoadAsync(IEntityId aggregateId)
        {
            if (aggregateId == null)
            {
               throw new Exception("Not found");
            }

            var query = new StringBuilder($@"SELECT {EventStoreListOfColumnsSelect} FROM {EventStoreTableName}");
            query.Append(" WHERE [AggregateId] = @AggregateId ");
            query.Append(" ORDER BY [Version] ASC;");

            using (var connection = new SqlConnection(GetConnectionString()))
            {
                var events = (await connection.QueryAsync<EventStoreDao>(query.ToString(), aggregateId != null ? new {AggregateId = aggregateId.ToString() } : null)).ToList();
                var domainEvents = events.Select(TransformEvent).Where(@event => @event != null).ToList().AsReadOnly();
                return domainEvents;
            }
        }

        private IDomainEvent TransformEvent(EventStoreDao eventSelected)
        {
            var o = JsonConvert.DeserializeObject(eventSelected.Data, _jsonSerializerSettings);
            var evt = (IDomainEvent)o;

            return evt;
        }

        public async Task SaveAsync(IEntityId aggregateId, int originalVersion, IReadOnlyCollection<IDomainEvent> events, string name = "Aggregate name")
        {
            if (events.Count == 0) 
                return;

            var query = $@"INSERT INTO {EventStoreTableName} ({EventStoreListOfColumnsInsert})
                           VALUES (@Id, @CreatedAt, @Sequence, @Version, @Name, @AggregateId, @Data, @Aggregate)";

            var listOfEvents = events.Select(ev => new
            {
                Aggregate = name,
                ev.CreatedAt,
                Sequence = 0,
                Data = JsonConvert.SerializeObject(ev, Formatting.Indented,_jsonSerializerSettings),
                Id = Guid.NewGuid(),
                ev.GetType().Name,
                AggregateId = aggregateId.ToString(),
                Version = ++originalVersion
            });

            var connectionString = GetConnectionString();

            using(var connection = new SqlConnection(connectionString))
            {
                await connection.ExecuteAsync(query, listOfEvents);
            }

        }
        private string GetConnectionString()
        {
            return "Server=(localdb)\\MSSQLLocalDB;Database=EventStore;Trusted_Connection=True; MultipleActiveResultSets=true";
        }
    }
}
