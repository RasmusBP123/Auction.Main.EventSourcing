using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Tactical.DDD;

namespace Auction.Persistence.Repositories
{
    public interface IEventStore
    {
        Task SaveAsync(IEntityId aggregateId, int originalVersion, IReadOnlyCollection<IDomainEvent> events, string name = "Aggregate name");
        Task<IReadOnlyCollection<IDomainEvent>> LoadAsync(IEntityId aggregateId);
    }
}
