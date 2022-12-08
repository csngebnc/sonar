using CSONGE.Application.Pagination;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shipping.Api.Common;
using Shipping.Application.Features.Administrator.Commands.LockerAggregate;
using Shipping.Application.Features.Administrator.Queries.LockerAggregate;
using System;
using System.Threading.Tasks;

namespace Shipping.Api.Controllers
{
    [Route(ApiResources.Locker.BasePath)]
    public class LockerController : PublicControllerBase
    {
        private readonly IMediator mediator;

        public LockerController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [Authorize(Policy = "Administrator")]
        public async Task<IPagedList<LockerDto>> GetLockers([FromQuery] GetLockersQuery query)
        {
            return await mediator.Send(query);
        }

        [Authorize(Policy = "Administrator")]
        [HttpPost]
        public async Task SaveLocker(SaveLockerCommand command)
        {
            await mediator.Send(command);
        }

        [Authorize(Policy = "Administrator")]
        [HttpPut]
        public async Task UpdateLocker(UpdateLockerCommand command)
        {
            await mediator.Send(command);
        }

        [Authorize(Policy = "Administrator")]
        [HttpDelete("{id}")]
        public async Task DeleteLocker(Guid id)
        {
            await mediator.Send(new DeleteLockerCommand
            {
                Id = id
            });
        }
    }
}
