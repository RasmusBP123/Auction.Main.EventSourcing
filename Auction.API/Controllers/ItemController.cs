using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Auction.Application.CreateItem;
using Auction.Application.GetITem;
using Auction.Application.PriceRaised;
using Auction.Core;
using Domain.Commands;
using Domain.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Auction.API.Controllers
{
    [Route("api/[controller]")]
    public class ItemController : Controller
    {
        private readonly ICommandBus _commandBus;
        private readonly IQueryBus _queryBus;
        private readonly IMediator _mediator;

        public ItemController(ICommandBus commandBus, IQueryBus queryBus, IMediator mediator)
        {
            _commandBus = commandBus;
            _queryBus = queryBus;
            _mediator = mediator;
        }

        [HttpGet("zzz")]
        public async Task<IActionResult> GetItem()
        {
            var item = await _queryBus.Send<GetItemQuery, Item>(new GetItemQuery(new Guid("e6bf3034-8f4b-4e9e-aa08-14afc7a0a088")));
            return Ok(item);
        }

        [HttpPost()]
        public async Task<IActionResult> CreateItem()
        {
            var newCommand = new CreateItemCommand("Vase", "Its big", 2500);
            await _mediator.Send(newCommand);
            return Ok();
        }

        [HttpPost("price")]
        public async Task<IActionResult> PriceRaised()
        {
            await _mediator.Send(new PriceRaisedCommand(new Guid("e6bf3034-8f4b-4e9e-aa08-14afc7a0a088"), 3000));
            return Ok();
        }
    }
}