using CSONGE.Domain.Entities;
using System;

namespace Partner.Domain.Entities
{
    public class Contact : Entity<Guid>
    {
        public string Name { get; set; }
        public AddressData Address { get; set; }
        public DateTime CreationDate { get; set; }

        public Guid PartnerId { get; set; }
        public DeliveryPartner Partner { get; set; }
    }
}
