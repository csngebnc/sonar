using Microsoft.AspNetCore.Mvc;

namespace Status.Api.Common
{
    [ApiController]
    [ApiExplorerSettings(GroupName = ApiResources.BasePath)]
    public class PublicControllerBase : ControllerBase
    {
    }
}
