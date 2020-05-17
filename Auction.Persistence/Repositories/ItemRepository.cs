using Auction.Core;
using System.Threading.Tasks;

namespace Auction.Persistence.Repositories
{
    public class ItemRepository : IItemRepository
    {
        private readonly IEventStore _eventStore;

        public ItemRepository(IEventStore eventStore)
        {
            _eventStore = eventStore;
        }

        public async Task<Item> GetItem(string aggregateId)
        {
            var resultEvents = await _eventStore.LoadAsync(new ItemId(aggregateId));
            return new Item(resultEvents);
        }

        public async Task<ItemId> SaveAsync(Item item)
        {
            await _eventStore.SaveAsync(item.Id, item.Version, item.DomainEvents, item.Name);
            return item.Id;
        }
    }
}
