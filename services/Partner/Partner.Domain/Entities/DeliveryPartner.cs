using CSONGE.Domain.Entities;
using System;
using System.Collections.Generic;

namespace Partner.Domain.Entities
{
    public class DeliveryPartner : Entity<Guid>
    {
        public string Name { get; set; }
        public string ContactPerson { get; set; }
        public string TaxNumber { get; set; }

        public List<TrackingNumber> TrackingNumbers { get; set; }
        public List<Contact> Contacts { get; set; }
    }
}
