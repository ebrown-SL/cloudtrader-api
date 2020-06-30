using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CloudTrader.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class TestController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return Ok();
        }
    }
}
