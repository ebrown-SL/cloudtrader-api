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
            var existingUser = await _userService.GetByUsername(authModel.Username);
            if (existingUser != null)
            {
                return Conflict("Username \"" + authModel.Username + "\" is already taken");
            }

            byte[] passwordHash, passwordSalt;
            _authenticationService.CreatePasswordHash(authModel.Password, out passwordHash, out passwordSalt);

            var user = await _userService.Create(new UserModel
            {
                Username = authModel.Username,
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
        public async Task<ActionResult> Login(AuthenticationModel authModel)
        {
            var user = await _userService.GetByUsername(authModel.Username);
            if (user == null)
            {
                return BadRequest("Username or password is incorrect");
            }

            var authenticated = _authenticationService.VerifyPassword(authModel.Password, user.PasswordHash, user.PasswordSalt);
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
