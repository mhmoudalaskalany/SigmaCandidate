using Microsoft.AspNetCore.Mvc;

namespace Candidate.Api.Controllers.Base
{
    /// <inheritdoc />
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
    }
}