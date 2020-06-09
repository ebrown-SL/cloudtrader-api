using Microsoft.AspNetCore.Mvc;

namespace CloudTrader.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ErrorController : Controller
    {
        public ActionResult Error()
        {
            return Problem();
        }
    }
}
