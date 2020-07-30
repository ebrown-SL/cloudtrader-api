﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CloudTrader.Api.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Swashbuckle.AspNetCore.Annotations;
using Microsoft.AspNetCore.Authorization;

namespace CloudTrader.Api.Controllers
{
    [ApiController]
    [Authorize]
    [Route("[controller]")]
    public class FrontEndController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly ITokenGenerator _tokenGenerator;
        private readonly ITraderApiService _traderApiService;

        public FrontEndController(
            IUserRepository userRepository,
            ITokenGenerator tokenGenerator,
            ITraderApiService traderRepository)
        {
            _userRepository = userRepository;
            _tokenGenerator = tokenGenerator;
            _traderApiService = traderRepository;
        }

        [HttpGet("whoAmI")]
        [SwaggerOperation(
            Summary = "Get current user's id",
            Description = "Returns an int of the id of currently logged-in user")]
        [SwaggerResponse(StatusCodes.Status200OK, "Success", typeof(int))]
        public async Task<IActionResult> GetUser()
        {
            var userId = int.Parse(User.Identity.Name);

            // Return the user by sending a GET request with the userId
            var user = await _userRepository.GetUser(userId);
            return Ok(user);
        }

        [HttpGet("currentUserBalance")]
        [SwaggerOperation(
            Summary = "Get current user's balance",
            Description = "Returns an int of the balance of currently logged-in user")]
        [SwaggerResponse(StatusCodes.Status200OK, "Success", typeof(int))]
        public async Task<IActionResult> GetBalance()
        {
            var userId = int.Parse(User.Identity.Name);
            var currentUser = await _userRepository.GetUser(userId);
            var currentUserTraderId = currentUser.TraderId;
            var balance = await _traderApiService.GetTrader(currentUserTraderId);

            return Ok(balance);
        }
    }
}
