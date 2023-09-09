using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

using System.Net.Mime;

namespace OSBM.Admin.API.Controllers.Base
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableRateLimiting("fixed")]
    [Consumes(MediaTypeNames.Application.Json)]
    [Authorize]
    public class BaseApiController : BaseApiResponse
    {
    }
}