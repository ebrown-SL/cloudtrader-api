using CloudTrader.Api.Auth;
using CloudTrader.Api.Models;
using CloudTrader.Users.Domain.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CloudTrader.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthenticationController : Controller
    {
        private readonly IRegisterService registrationService;

        private readonly ILoginService loginService;

        private readonly ITokenGenerator tokenGenerator;

        public AuthenticationController(IRegisterService registrationService, ILoginService loginService, ITokenGenerator jwtTokenGenerator)
        {
            this.registrationService = registrationService;
            this.loginService = loginService;
            this.tokenGenerator = jwtTokenGenerator;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(Credentials credentials)
        {
            var user = await registrationService.Register(credentials.Username, credentials.Password);
            var token = tokenGenerator.GenerateToken(user.Id);
            var registerSuccess = new LoginSuccessResponse(user.Id, user.Username, token);
            return Ok(registerSuccess);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(Credentials credentials)
        {
            var user = await loginService.Login(credentials.Username, credentials.Password);
            var token = tokenGenerator.GenerateToken(user.Id);
            var loginSuccess = new LoginSuccessResponse(user.Id, user.Username, token);
            return Ok(loginSuccess);
        }
    }
}