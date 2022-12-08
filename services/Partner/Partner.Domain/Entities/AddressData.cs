using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Partner.Domain.Entities
{
    public class AddressData
    {
        public string Country { get; set; }
        public string County { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public string StreetAndNumber { get; set; }
        public string Other { get; set; }
    }
}
