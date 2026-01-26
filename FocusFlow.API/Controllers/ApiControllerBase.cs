using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

namespace FocusFlow.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public abstract class ApiControllerBase : ControllerBase
    {
        protected string GetUserId()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return userId ?? throw new UnauthorizedAccessException("User ID not found in token");
        }
    }
}
