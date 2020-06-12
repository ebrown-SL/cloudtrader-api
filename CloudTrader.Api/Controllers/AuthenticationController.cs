using System.Threading.Tasks;
using CloudTrader.Api.Models;
using CloudTrader.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace CloudTrader.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthenticationController : Controller
    {
        private IAuthenticationService _authenticationService;

        private IUserService _userService;

        public AuthenticationController(IAuthenticationService authenticationService, IUserService userService)
        {
            _authenticationService = authenticationService;
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<ActionResult> Register(AuthenticationModel authModel)
        {
            var user = await _userService.CreateUser(authModel.Username, authModel.Password);

            var token = _authenticationService.GenerateToken(user.Id);

            return Ok(new
            {
                user.Id,
                user.Username,
                token
            });
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login(AuthenticationModel authModel)
        {
            var user = await _userService.GetUser(authModel.Username);

            var verifiedPassword = _authenticationService.VerifyPassword(authModel.Password, user.PasswordHash, user.PasswordSalt);
            if (!verifiedPassword)
            {
                return Unauthorized("Username or password is incorrect");
            }

            var token = _authenticationService.GenerateToken(user.Id);

            return Ok(new
            {
                user.Id,
                user.Username,
                token
            });
        }
    }
}
