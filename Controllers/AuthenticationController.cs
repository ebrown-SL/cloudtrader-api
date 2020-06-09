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
        public async Task<ActionResult> Register(AuthenticationModel model)
        {
            byte[] passwordHash, passwordSalt;
            _authenticationService.CreatePasswordHash(model.Password, out passwordHash, out passwordSalt);

            var existingUser = await _userService.GetByUsername(model.Username);
            if (existingUser != null)
            {
                return BadRequest("Username \"" + model.Username + "\" is already taken");
            }

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

        [HttpPost("login")]
        public async Task<ActionResult> Login(AuthenticationModel model)
        {
            var user = await _userService.GetByUsername(model.Username);
            if (user == null)
            {
                return BadRequest("Username or password is incorrect");
            }

            var authenticated = _authenticationService.VerifyPassword(model.Password, user.PasswordHash, user.PasswordSalt);
            if (!authenticated)
            {
                return BadRequest("Username or password is incorrect");
            }

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
