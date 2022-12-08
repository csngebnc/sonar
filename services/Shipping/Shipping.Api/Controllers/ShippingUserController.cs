using CSONGE.Application.Pagination;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shipping.Api.Common;
using Shipping.Application.Features.Administrator.Commands.ShippingUserAggregate;
using Shipping.Application.Features.Administrator.Queries.ShippingUserAggregate;
using System;
using System.Threading.Tasks;

namespace Shipping.Api.Controllers
{
    [Route(ApiResources.User.BasePath)]
    public class ShippingUserController : PublicControllerBase
    {
        private readonly IMediator mediator;

        public ShippingUserController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [Authorize(Policy = "Administrator")]
        [HttpGet("couriers")]
        public async Task<IPagedList<CourierDto>> GetCouriers([FromQuery] GetCouriersQuery query)
        {
            return await mediator.Send(query);
        }

        [Authorize(Policy = "Administrator")]
        [HttpGet("admins")]
        public async Task<IPagedList<AdminDto>> GetAdmins([FromQuery] GetAdministratorsQuery query)
        {
            return await mediator.Send(query);
        }

        [Authorize(Policy = "Administrator")]
        [HttpPost("courier")]
        public async Task SaveCourier(SaveCourierCommand command)
        {
            await mediator.Send(command);
        }

        [Authorize(Policy = "Administrator")]
        [HttpPut("courier")]
        public async Task UpdateCourier(UpdateCourierCommand command)
        {
            await mediator.Send(command);
        }

        [Authorize(Policy = "Administrator")]
        [HttpDelete("courier/{id}")]
        public async Task DeleteCourier(Guid id)
        {
            await mediator.Send(new DeleteCourierCommand
            {
                Id = id
            });
        }


        [Authorize(Policy = "Administrator")]
        [HttpPost("admin")]
        public async Task SaveAdmin(SaveAdminCommand command)
        {
            await mediator.Send(command);
        }

        [Authorize(Policy = "Administrator")]
        [HttpPut("admin")]
        public async Task UpdateAdmin(UpdateAdminCommand command)
        {
            await mediator.Send(command);
        }

        [Authorize(Policy = "Administrator")]
        [HttpDelete("admin/{id}")]
        public async Task DeleteAdmin(Guid id)
        {
            await mediator.Send(new DeleteAdminCommand
            {
                Id = id
            });
        }
    }
}
