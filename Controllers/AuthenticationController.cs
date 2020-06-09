using CloudTrader.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace CloudTrader.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthenticationController : Controller
    {
        [HttpPost("register")]
        public ActionResult Register(AuthenticationModel model)
        {
            return Ok(model);
        }
    }
}
