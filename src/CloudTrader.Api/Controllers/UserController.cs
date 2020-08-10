using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CloudTrader.Api.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Swashbuckle.AspNetCore.Annotations;
using Microsoft.AspNetCore.Authorization;
using System;

namespace CloudTrader.Api.Controllers
{
    [ApiController]
    [Authorize]
    [Route("[controller]")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(
            IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("current")]
        [SwaggerOperation(
            Summary = "Get current user's id",
            Description = "Returns an int of the id of currently logged-in user")]
        [SwaggerResponse(StatusCodes.Status200OK, "Success", typeof(Guid))]
        public async Task<IActionResult> GetUser()
        {
            var userId = Guid.Parse(User.Identity.Name);

            return Ok(await _userService.GetUser(userId));
        }

        [HttpGet("current/balance")]
        [SwaggerOperation(
            Summary = "Get current user's balance",
            Description = "Returns an int of the balance of currently logged-in user")]
        [SwaggerResponse(StatusCodes.Status200OK, "Success", typeof(Guid))]
        public async Task<IActionResult> GetBalance()
        {
            var userId = Guid.Parse(User.Identity.Name);

            return Ok(await _userService.GetBalanceOfUser(userId));
        }
    }
}
