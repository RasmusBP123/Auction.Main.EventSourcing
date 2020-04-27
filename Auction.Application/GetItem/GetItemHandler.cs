using Auction.Application.GetITem;
using Auction.Core;
using Domain.Queries;
using System.Threading;
using System.Threading.Tasks;
using Tactical.DDD;

namespace Auction.Application.GetItem
{
    public class GetItemHandler : IQueryHandler<GetItemQuery, Item>
    {
        private readonly IItemRepository _repository;

        public GetItemHandler(IItemRepository repository)
        {
            _repository = repository;
        }
        public async Task<Item> Handle(GetItemQuery request, CancellationToken cancellationToken)
        {
            var result = await _repository.GetItem(request.ItemId.ToString());
            return result;
        }
    }
}
