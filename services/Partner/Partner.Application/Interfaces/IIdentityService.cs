using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Partner.Application.Interfaces
{
    public interface IIdentityService
    {
        Guid GetCurrentUserId();
    }
}
