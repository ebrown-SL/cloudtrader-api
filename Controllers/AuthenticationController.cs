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

        public AuthenticationController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost("register")]
        public ActionResult Register(AuthenticationModel model)
        {
            byte[] passwordHash, passwordSalt;
            _authenticationService.CreatePasswordHash(model.Password, out passwordHash, out passwordSalt);

            var user = new UserModel
            {
                Username = model.Username,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt
            };

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
