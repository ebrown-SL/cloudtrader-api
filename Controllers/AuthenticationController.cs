using System.Threading.Tasks;
using CloudTrader.Api.Models;
using CloudTrader.Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CloudTrader.Api.Controllers
{
    [Authorize]
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

        [HttpGet]
        public ActionResult Index()
        {
            return Ok();
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<ActionResult> Register(AuthenticationModel model)
        {
            byte[] passwordHash, passwordSalt;
            _authenticationService.CreatePasswordHash(model.Password, out passwordHash, out passwordSalt);

            var user = await _userService.Create(new UserModel
            {
                Username = model.Username,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt
            });

            string token = _authenticationService.GenerateToken(user.ID);

            return Ok(new
            {
                user.ID,
                user.Username,
                token
            });
        }
    }
}
