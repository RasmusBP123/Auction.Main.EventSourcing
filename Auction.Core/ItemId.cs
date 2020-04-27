using System;
using Tactical.DDD;

namespace Auction.Core
{
    public class ItemId : EntityId
    {
        private Guid _guid;

        public ItemId()
        {
            _guid = Guid.NewGuid();
        }

        public ItemId(string id)
        {
            _guid = Guid.Parse(id);
        }
        public override string ToString()
        {
            return _guid.ToString();
        }
    }
}