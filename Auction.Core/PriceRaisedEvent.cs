using System;
using System.Collections.Generic;
using System.Text;

namespace Auction.Core
{
    public class PriceRaisedEvent : DomainEvent
    {
        public PriceRaisedEvent(string itemId, double price)
        {
            ItemId = itemId;
            Price = price;
        }

        public string ItemId { get; }
        public double Price { get; }
    }
}
