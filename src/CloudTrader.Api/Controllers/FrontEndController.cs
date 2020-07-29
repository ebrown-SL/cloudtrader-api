using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CloudTrader.Api.Service.Models;
using CloudTrader.Api.Data;
using CloudTrader.Api.Service.Interfaces;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Http;
using CloudTrader.Api.Service.Helpers;

namespace CloudTrader.Api.Controllers
{
    [ApiController]
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
        public async Task<IActionResult> GetUser()
        {
            // Capture the authentication token from the http request using the relevant header
            var authToken = GetAuthTokenFrom(Request);

            // Decode the token to give the userId
            var userId = _tokenGenerator.DecodeToken(authToken);

            // Return the user by sending a GET request with the userId
            var user = await _userRepository.GetUser(userId);
            return Ok(user);
        }

        [HttpGet("currentUserBalance")]
        public async Task<IActionResult> GetBalance()
        {
            var authToken = GetAuthTokenFrom(Request);
            var userId = _tokenGenerator.DecodeToken(authToken);
            var currentUser = await _userRepository.GetUser(userId);
            var currentUserTraderId = currentUser.TraderId;
            var balance = await _traderApiService.GetTrader(currentUserTraderId);

            return Ok(balance);
        }

        private static string GetAuthTokenFrom(HttpRequest request)
        {
            var match = new Regex("^Bearer (.+)")
                .Match(request.Headers["Authorization"].ToString());

            return match.Groups[0].Value;
        }
    }
}
