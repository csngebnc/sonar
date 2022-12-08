using SharedKernel.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Status.Interface.Events
{
    public class ParcelCreatedEvent : ParcelEvent
    {
        public Guid ParcelId { get; set; }
        public string TrackingNumber { get; set; }
        public ParcelType Type { get; set; }
    }
}
