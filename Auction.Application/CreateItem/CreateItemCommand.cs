using Domain.Commands;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Auction.Application.CreateItem
{
    public class CreateItemCommand : IRequest
    {
        public CreateItemCommand(string name, string description, double price)
        {
            Name = name;
            Description = description;
            Price = price;
        }

        public string Name { get; }
        public string Description { get; }
        public double Price { get; }
    }
}
