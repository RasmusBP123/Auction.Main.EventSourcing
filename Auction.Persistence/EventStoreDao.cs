using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Auction.Persistence
{
    public class EventStoreDao
    {
        public Guid Id { get; set; }
        public string Data { get; set; }
        public int Version { get; set; }

        public DateTimeOffset CreatedAt { get; set; }

        public int Sequence { get; set; }

        public string Name { get; set; }
        public string Aggregate { get; set; }
        public string AggregateId { get; set; }
    }
}
