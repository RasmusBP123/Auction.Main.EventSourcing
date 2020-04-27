using Auction.Persistence.Factories;
using Auction.Persistence.Services;
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
        private static string EventStoreListOfColumnsInsert = "[Id], [CreatedAt], [Version], [Name], [AggregateId], [Data], [Aggregate]";
        private static readonly string EventStoreListOfColumnsSelect = $"{EventStoreListOfColumnsInsert},[Sequence]";

        private readonly ISQLConnectionFactory _connectionFactory;
        private readonly JsonSerializerService _serializerService;

        public EventStoreRepository(ISQLConnectionFactory connectionFactory, JsonSerializerService serializerService)
        {
            _connectionFactory = connectionFactory;
            _serializerService = serializerService;
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

            using (var connection = _connectionFactory.SqlConnection())
            {
                var events = (await connection.QueryAsync<EventStoreDao>(query.ToString(), new {AggregateId = aggregateId.ToString() }));
                var domainEvents = events.Select(_serializerService.TransformEvent).Where(@event => @event != null).ToList().AsReadOnly();
                return domainEvents;
            }
        }

        public async Task SaveAsync(IEntityId aggregateId, int originalVersion, IReadOnlyCollection<IDomainEvent> events, string name = "Aggregate name")
        {
            if (events.Count == 0) 
                return;

            var query = $@"INSERT INTO {EventStoreTableName} ({EventStoreListOfColumnsInsert})
                           VALUES (@Id, @CreatedAt, @Version, @Name, @AggregateId, @Data, @Aggregate)";

            var listOfEvents = events.Select(ev => new
            {
                Id = Guid.NewGuid(),
                Data = JsonConvert.SerializeObject(ev, Formatting.Indented,_serializerService._jsonSerializerSettings),
                Aggregate = name,
                ev.CreatedAt,
                ev.GetType().Name,
                AggregateId = aggregateId.ToString(),
                Version = ++originalVersion
            });

            using(var connection = _connectionFactory.SqlConnection())
            {
                await connection.ExecuteAsync(query, listOfEvents);
            }

        }
    }
}
