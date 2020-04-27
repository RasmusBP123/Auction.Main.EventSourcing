using System;
using System.Collections.Generic;
using System.Text;
using Tactical.DDD;

namespace Auction.Core
{
    public class ItemCreatedEvent : DomainEvent
    {
        public ItemCreatedEvent(string itemId, string name, string description, double price)
        {
            ItemId = itemId;
            Name = name;
            Description = description;
            Price = price;
        }

        public string ItemId { get; }
        public string Name { get; }
        public string Description { get; }
        public double Price { get; }
    }
}
