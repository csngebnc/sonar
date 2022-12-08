using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Status.Api.Common;
using Status.Application.Features.ParcelAggregate.Queries;
using System.Threading.Tasks;

namespace Status.Api.Controllers
{
    [Route(ApiResources.Status.BasePath)]
    public class StatusController : PublicControllerBase
    {
        private readonly IMediator mediator;

        public StatusController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet("history/{trackingNumber}")]
        public async Task<TrackingHistory> GetTrackingHistory(string trackingNumber)
        {
            return await mediator.Send(new GetTrackingHistoryQuery()
            {
                TrackingNumber = trackingNumber
            });
        }
    }
}
