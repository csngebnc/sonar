using Microsoft.AspNetCore.Mvc;

namespace Order.Api.Common
{
    [ApiController]
    [ApiExplorerSettings(GroupName = ApiResources.BasePath)]
    public class PublicControllerBase : ControllerBase
    {
    }
}