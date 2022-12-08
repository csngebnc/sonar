using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shipping.Application.Interfaces
{
    public interface IIdentityService
    { 
        Guid GetCurrentUserId();
    }
}
