using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

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
