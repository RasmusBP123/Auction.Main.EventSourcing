using MediatR;
using System;

namespace Auction.Application.PriceRaised
{
    public class PriceRaisedCommand : IRequest
    {
        public PriceRaisedCommand(Guid itemId, double price)
        {
            ItemId = itemId;
            Price = price;
        }

        public Guid ItemId { get; }
        public double Price { get; }
    }
}
