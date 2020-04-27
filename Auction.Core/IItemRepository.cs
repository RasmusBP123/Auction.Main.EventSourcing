using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Tactical.DDD;

namespace Auction.Core
{
    public interface IItemRepository
    {
        Task<ItemId> SaveAsync(Item item);
        Task<Item> GetItem(string aggregateId);
    }
}
