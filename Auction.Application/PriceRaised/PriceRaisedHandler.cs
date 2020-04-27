using Auction.Core;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Auction.Application.PriceRaised
{
    public class PriceRaisedHandler : IRequestHandler<PriceRaisedCommand>
    {
        private readonly IItemRepository _repository;

        public PriceRaisedHandler(IItemRepository repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(PriceRaisedCommand request, CancellationToken cancellationToken)
        {
            var itemToBeRaised = await _repository.GetItem(request.ItemId.ToString());
            itemToBeRaised.PriceRaised(request.ItemId.ToString(), request.Price);
            await _repository.SaveAsync(itemToBeRaised);
            return Unit.Value;
        }
    }
}
