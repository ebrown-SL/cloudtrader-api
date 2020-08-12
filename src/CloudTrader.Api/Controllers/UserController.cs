using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CloudTrader.Api.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Swashbuckle.AspNetCore.Annotations;
using Microsoft.AspNetCore.Authorization;
using CloudTrader.Api.Service.Services;
using CloudTrader.Api.Service.Models;

namespace CloudTrader.Api.Controllers
{
    [ApiController]
    [Authorize]
    [Route("[controller]")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly IMineApiService _mineApiService;


        public UserController(
            IUserService userService,
            IMineApiService mineApiService)
        {
            _userService = userService;
            _mineApiService = mineApiService;
        }

        [HttpGet("current")]
        [SwaggerOperation(
            Summary = "Get current user's details",
            Description = "Returns a user object containing the details of the currently logged-in user")]
        [SwaggerResponse(StatusCodes.Status200OK, "Success", typeof(User))]
        public async Task<IActionResult> GetUser()
        {
            var userId = int.Parse(User.Identity.Name);

            return Ok(await _userService.GetUser(userId));
        }

        [HttpGet("current/balance")]
        [SwaggerOperation(
            Summary = "Get current user's balance",
            Description = "Returns an int of the balance of currently logged-in user")]
        [SwaggerResponse(StatusCodes.Status200OK, "Success", typeof(int))]
        public async Task<IActionResult> GetBalance()
        {
            var userId = int.Parse(User.Identity.Name);

            return Ok(await _userService.GetBalanceOfUser(userId));
        }

        [HttpGet("current/stock/{mineId}")]
        [SwaggerOperation(
            Summary = "Return user's stock of a particular mine",
            Description = "Update the current user's balance; update the current user's stock; update the mine's stock")]
        [SwaggerResponse(StatusCodes.Status200OK, "Success", typeof(int))]
        public async Task<IActionResult> GetStockOfMine(int mineId)
        {
            var userId = int.Parse(User.Identity.Name);

            return Ok(await _userService.GetUsersStockForMine(userId, mineId));
        }


        [HttpPost("current/stock/buy")]
        [SwaggerOperation(
            Summary = "Process purchase request",
            Description = "Update the current user's balance; update the current user's stock; update the mine's stock")]
        [SwaggerResponse(StatusCodes.Status200OK, "Success", typeof(void))]
        public async Task<IActionResult> ProcessTransaction(PurchaseObject purchaseObject)
        {
            var userId = int.Parse(User.Identity.Name);

            await _userService.ProcessTransaction(
                userId, 
                purchaseObject.mineId, 
                purchaseObject.quantity, 
                purchaseObject.purchaseAmount
            );

            return Ok();
        }
    }
}
