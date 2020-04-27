using System;
using System.Collections.Generic;
using System.Text;
using Tactical.DDD;
using Tactical.DDD.EventSourcing;

namespace Auction.Core
{
    public class Item : Tactical.DDD.EventSourcing.AggregateRoot<ItemId>
    {
        public Item(IEnumerable<IDomainEvent> events) : base(events)
        {
        }

        public override ItemId Id { get; protected set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }

        private Item() { }

        public static Item Create(string name, string description, double price)
        {
            var item = new Item();
            item.Apply(new ItemCreatedEvent(new ItemId().ToString(), name, description, price));
            return item;
        }

        public void PriceRaised(string id, double price)
        {
            Apply(new PriceRaisedEvent(id, price));
        }

        public void On(ItemCreatedEvent @event)
        {
            Id = new ItemId(@event.ItemId);
            Name = @event.Name;
            Description = @event.Description;
            Price = @event.Price;
        }

        public void On(PriceRaisedEvent @event)
        {
            Id = new ItemId(@event.ItemId);
            Price = @event.Price;
        }
    }
}
