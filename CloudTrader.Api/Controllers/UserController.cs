using CloudTrader.Api.Models;
using CloudTrader.Users.Domain.Models;
using CloudTrader.Users.Domain.Models.TraderApiClientModels;
using CloudTrader.Users.Domain.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Threading.Tasks;

namespace CloudTrader.Api.Controllers
{
    [ApiController]
    [Authorize]
    [Route("[controller]")]
    public class UserController : Controller
    {
        private readonly IUserService userService;

        public UserController(
            IUserService userService)
        {
            this.userService = userService;
        }

        [HttpGet("current")]
        [SwaggerOperation(
            Summary = "Get current user's details",
            Description = "Returns a user object containing the details of the currently logged-in user")]
        [SwaggerResponse(StatusCodes.Status200OK, "Success", typeof(User))]
        public async Task<IActionResult> GetUser()
        {
            if (!Guid.TryParse(User.Identity.Name, out Guid userId))
                return new UnauthorizedResult();

            return Ok(await userService.GetUser(userId));
        }

        [HttpGet("current/balance")]
        [SwaggerOperation(
            Summary = "Get current user's balance",
            Description = "Returns an int of the balance of currently logged-in user")]
        [SwaggerResponse(StatusCodes.Status200OK, "Success", typeof(Guid))]
        public async Task<IActionResult> GetBalance()
        {
            if (!Guid.TryParse(User.Identity.Name, out Guid userId))
                return new UnauthorizedResult();

            return Ok(await userService.GetBalanceOfUser(userId));
        }

        [HttpGet("current/stock/{mineId}")]
        [SwaggerOperation(
            Summary = "Return user's stock of a particular mine",
            Description = "For a given mine id, return the number of stock the user has for that mine")]
        [SwaggerResponse(StatusCodes.Status200OK, "Success", typeof(int))]
        public async Task<IActionResult> GetStockOfMine(Guid mineId)
        {
            if (!Guid.TryParse(User.Identity.Name, out Guid userId))
                return Unauthorized();

            return Ok(await userService.GetUsersStockForMine(userId, mineId));
        }

        [HttpGet("current/stock")]
        [SwaggerOperation(
            Summary = "Return user's stock of all mines",
            Description = "For a given mine id, return the number of stock the uesr has for that mine")]
        [SwaggerResponse(StatusCodes.Status200OK, "Success", typeof(GetTraderMinesResponseModel))]
        public async Task<IActionResult> GetAllStock()
        {
            if (!Guid.TryParse(User.Identity.Name, out Guid userId))
                return Unauthorized();

            return Ok(await userService.GetAllUserStock(userId));
        }

        [HttpPost("current/stock/buy")]
        [SwaggerOperation(
            Summary = "Process purchase request",
            Description = "Update the current user's balance; update the current user's stock; update the mine's stock")]
        [SwaggerResponse(StatusCodes.Status200OK, "Success", typeof(void))]
        public async Task<IActionResult> ProcessTransaction(PurchaseObject purchaseObject)
        {
            if (!Guid.TryParse(User.Identity.Name, out Guid userId))
                return Unauthorized();

            await userService.ProcessTransaction(
                userId,
                purchaseObject.mineId,
                purchaseObject.quantity,
                purchaseObject.purchaseAmount
            );

            return Ok();
        }
    }
}