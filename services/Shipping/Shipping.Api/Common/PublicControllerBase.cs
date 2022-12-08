using Microsoft.AspNetCore.Mvc;

namespace Shipping.Api.Common
{
    [ApiController]
    [ApiExplorerSettings(GroupName = ApiResources.BasePath)]
    public class PublicControllerBase : ControllerBase
    {
    }
}