using Auction.Core;
using Domain.Commands;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Auction.Application.CreateItem
{
    public class CreateItemHandler : IRequestHandler<CreateItemCommand>
    {
        private readonly IItemRepository _itemRepository;

        public CreateItemHandler(IItemRepository itemRepository)
        {
            _itemRepository = itemRepository;
        }

        public async Task<Unit> Handle(CreateItemCommand request, CancellationToken cancellationToken)
        {
            var item = Item.Create(request.Name, request.Description, request.Price);
            await _itemRepository.SaveAsync(item);

            return Unit.Value;
        }
    }
}
