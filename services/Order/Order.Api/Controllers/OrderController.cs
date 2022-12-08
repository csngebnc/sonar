using MediatR;
using Microsoft.AspNetCore.Mvc;
using Order.Api.Common;
using Order.Application.Features.ExtraOptionAggregate.Queries;
using Order.Application.Features.LockerAggregate.Queries;
using Order.Application.Features.ParcelAggregate.Commands;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Order.Api.Controllers
{
    [Route(ApiResources.Order.BasePath)]
    public class OrderController : PublicControllerBase
    {
        private readonly IMediator mediator;

        public OrderController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost("parcel")]
        public async Task<List<string>> SendParcel(SendParcelsCommand command)
        {
            return await mediator.Send(command);
        }

        [HttpGet("options")]
        public async Task<ExtraOptionsResponse> GetExtraOptions()
        {
            return await mediator.Send(new GetExtraOptionsQuery());
        }

        [HttpGet("lockers")]
        public async Task<LockerResponse> GetLockers()
        {
            return await mediator.Send(new GetLockersQuery());
        }
    }
}
