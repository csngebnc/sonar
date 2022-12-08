using CSONGE.Application.Pagination;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shipping.Api.Common;
using Shipping.Application.Features.Administrator.Commands.ExtraOptionAggregate;
using Shipping.Application.Features.Administrator.Queries.ExtraOptionAggregate;
using System;
using System.Threading.Tasks;

namespace Shipping.Api.Controllers
{
    [Route(ApiResources.ExtraOption.BasePath)]
    public class ExtraOptionController : PublicControllerBase
    {
        private readonly IMediator mediator;

        public ExtraOptionController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [Authorize(Policy = "Administrator")]
        [HttpGet]
        public async Task<IPagedList<OptionDto>> GetOptions([FromQuery] GetExtraOptionQuery query)
        {
            return await mediator.Send(query);
        }

        [Authorize(Policy = "Administrator")]
        [HttpPost]
        public async Task SaveExtraOption(SaveExtraOptionCommand command)
        {
            await mediator.Send(command);
        }

        [Authorize(Policy = "Administrator")]
        [HttpPut]
        public async Task UpdateExtraOption(UpdateExtraOptionCommand command)
        {
            await mediator.Send(command);
        }

        [Authorize(Policy = "Administrator")]
        [HttpDelete("{id}")]
        public async Task DeleteExtraOption(Guid id)
        {
            await mediator.Send(new DeleteExtraOptionCommand
            {
                Id = id
            });
        }
    }
}
