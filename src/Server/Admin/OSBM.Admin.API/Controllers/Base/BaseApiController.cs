using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace OSBM.Admin.API.Controllers.Base
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableRateLimiting("fixed")]
    public class BaseApiController : BaseApiResponse
    {
    }
}