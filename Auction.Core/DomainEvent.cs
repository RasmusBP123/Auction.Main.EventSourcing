using System;
using System.Collections.Generic;
using System.Text;
using Tactical.DDD;

namespace Auction.Core
{
    public class DomainEvent : IDomainEvent
    {
        public DomainEvent()
        {
            CreatedAt = DateTime.UtcNow;
        }

        public DateTime CreatedAt { get; set; }
    }
}
