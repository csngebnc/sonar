using CSONGE.Domain.Entities;
using SharedKernel.Enums;
using Status.Interface.Events;
using System;
using System.Collections.Generic;

namespace Status.Domain.Entities
{
    public class Parcel : Entity<Guid>
    {
        public string TrackingNumber { get; set; }
        public ParcelType Type { get; set; }
        public ParcelState State { get; set; }
        public string Location { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public ICollection<ParcelEvent> Events { get; set; }


        public Parcel()
        {
        }

        public Parcel(ParcelCreatedEvent @event)
        {
            Events = new List<ParcelEvent>();

            Id = @event.ParcelId;
            TrackingNumber = @event.TrackingNumber;
            Type = @event.Type;
            State = @event.State;
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = CreatedAt;

            Events.Add(@event);
        }
    }
}
