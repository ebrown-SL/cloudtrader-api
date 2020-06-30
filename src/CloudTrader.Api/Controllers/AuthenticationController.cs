using System.Threading.Tasks;
using CloudTrader.Api.Models;
using CloudTrader.Api.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CloudTrader.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthenticationController : Controller
    {
        private readonly IRegisterService _registrationService;

        private readonly ILoginService _loginService;

        public AuthenticationController(IRegisterService registrationService, ILoginService loginService)
        {
            _registrationService = registrationService;
            _loginService = loginService;
        }

        [HttpPost("register")]
        public async Task<ActionResult> Register(Credentials credentials)
        {
            var authDetails = await _registrationService.Register(credentials.Username, credentials.Password);

            return Ok(authDetails);
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login(Credentials credentials)
        {         
            var authDetails = await _loginService.Login(credentials.Username, credentials.Password);

            return Ok(authDetails);
        }
    }
}
