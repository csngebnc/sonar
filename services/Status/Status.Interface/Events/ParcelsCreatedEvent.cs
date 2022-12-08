using SharedKernel.Enums;
using System;
using System.Collections.Generic;

namespace Status.Interface.Events
{
    public class ParcelsCreatedEvent : ParcelEvent
    {
        public List<ParcelItem> Parcels { get; set; }

        public class ParcelItem
        {
            public Guid ParcelId { get; set; }
            public string TrackingNumber { get; set; }
            public ParcelType Type { get; set; }
        }
    }
}
