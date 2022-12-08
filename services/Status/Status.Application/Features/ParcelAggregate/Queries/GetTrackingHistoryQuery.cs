using MediatR;
using SharedKernel.Enums;
using Status.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Status.Application.Features.ParcelAggregate.Queries
{
    public class GetTrackingHistoryQuery : IRequest<TrackingHistory>
    {
        public string TrackingNumber { get; set; }
    }

    public class TrackingHistory
    {
        public List<TrackingHistoryItem> Events { get; set; }

        public class TrackingHistoryItem
        {
            public DateTime Time { get; set; }
            public ParcelState State { get; set; }
        }
    }

    public class GetTrackingHistoryQueryHandler : IRequestHandler<GetTrackingHistoryQuery, TrackingHistory>
    {
        private readonly IParcelRepository parcelRepository;

        public GetTrackingHistoryQueryHandler(IParcelRepository parcelRepository)
        {
            this.parcelRepository = parcelRepository;
        }

        public async Task<TrackingHistory> Handle(GetTrackingHistoryQuery request, CancellationToken cancellationToken)
        {
            var parcel = await parcelRepository.FindByTrackingNumberAsync(request.TrackingNumber);

            return new()
            {
                Events = parcel.Events
                    .OrderByDescending(@event => @event.CreationDate)
                    .Select(@event => new TrackingHistory.TrackingHistoryItem
                    {
                        Time = @event.CreationDate,
                        State = @event.State
                    })
                    .ToList()
            };
        }
    }
}
