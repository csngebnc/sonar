using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Partner.Api.Common;
using Partner.Application.Features.Partner;
using System;
using System.Threading.Tasks;

namespace Partner.Api.Controllers
{
    [Route(ApiResources.Partners.BasePath)]
    public class PartnerController : PublicControllerBase
    {
        private readonly IMediator mediator;

        public PartnerController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [Authorize(Policy = "Administrator")]
        [HttpGet("list")]
        public async Task<ListPartnerQueryResponse> ListPartners()
        {
            return await mediator.Send(new ListPartnersQuery());
        }

        [Authorize(Policy = "Administrator")]
        [HttpGet("partner/{id}")]
        public async Task<PartnerDto> GetPartnerById(Guid id)
        {
            return await mediator.Send(new GetPartnerByIdQuery()
            {
                PartnerId = id
            });
        }

        [Authorize(Policy = "Partner")]
        [HttpGet("contacts")]
        public async Task<ListContactsQueryResponse> ListContacts(Guid partnerId)
        {
            return await mediator.Send(new ListContactsQuery
            {
                PartnerId = partnerId
            });
        }

        [Authorize(Policy = "Partner")]
        [HttpGet("contact/{id}")]
        public async Task<ContactDto> GetContactById(Guid id)
        {
            return await mediator.Send(new GetContactById
            {
                ContactId = id
            });
        }

        [Authorize(Policy = "Administrator")]
        [HttpPost("add")]
        public async Task SavePartner(SavePartnerCommand command)
        {
            await mediator.Send(command);
        }

        [Authorize(Policy = "Administrator")]
        [HttpPut("update")]
        public async Task UpdatePartnerData(UpdatePartnerDataCommand command)
        {
            await mediator.Send(command);
        }


        [Authorize(Policy = "Administrator")]
        [HttpDelete("remove/user")]
        public async Task RemoveTrackingNumberFromPartner(RemovePartnerCommand command)
        {
            await mediator.Send(command);
        }

        [Authorize(Policy = "Partner")]
        [HttpPost("tracking")]
        public async Task AddTrackingNumberToPartner(SaveTrackingNumberForPartnerCommand command)
        {
            await mediator.Send(command);
        }

        [Authorize(Policy = "Partner")]
        [HttpDelete("tracking")]
        public async Task RemoveTrackingNumberFromPartner(RemoveTrackingNumberFromPartnerCommand command)
        {
            await mediator.Send(command);
        }

        [Authorize(Policy = "Partner")]
        [HttpPost("contact")]
        public async Task AddContact(SavePartnerContactCommand command)
        {
            await mediator.Send(command);
        }

        [Authorize(Policy = "Partner")]
        [HttpPut("contact")]
        public async Task UpdateContact(UpdatePartnerContactCommand command)
        {
            await mediator.Send(command);
        }

        [Authorize(Policy = "Partner")]
        [HttpDelete("contact")]
        public async Task DeleteContact(DeletePartnerContactCommand command)
        {
            await mediator.Send(command);
        }
    }
}
