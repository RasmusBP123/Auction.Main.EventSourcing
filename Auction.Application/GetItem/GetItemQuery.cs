using Auction.Core;
using Domain.Queries;
using System;

namespace Auction.Application.GetITem
{
    public class GetItemQuery : IQuery<Item>
    {
        public Guid ItemId { get; }
        public GetItemQuery(Guid itemId)
        {
            ItemId = itemId;
        }

    }
}
