using CloudTrader.Api.ApiClients;
using CloudTrader.Api.Models;
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
    public class MineController : Controller
    {
        private readonly IMineApiClient mineApiClient;

        public MineController(
            IMineApiClient mineApiService)
        {
            mineApiClient = mineApiService;
        }

        [HttpGet("{id}/stock")]
        [SwaggerOperation(
            Summary = "Get stock of a mine",
            Description = "See how much stock a mine currently has")]
        [SwaggerResponse(StatusCodes.Status200OK, "Success", typeof(int))]
        public async Task<IActionResult> GetStockOfMine(Guid id)
        {
            return Ok(await mineApiClient.GetMineStock(id));
        }

        [HttpGet]
        [SwaggerOperation(
            Summary = "Get all mines",
            Description = "Returns an object containing an array of mines")]
        [SwaggerResponse(StatusCodes.Status200OK, "Success", typeof(GetAllMinesResponseModel))]
        public async Task<IActionResult> GetAllMines()
        {
            return Ok(await mineApiClient.GetAllMines());
        }

        [HttpGet("{id}")]
        [SwaggerOperation(
            Summary = "Get details of a mine",
            Description = "See all details of a mine")]
        [SwaggerResponse(StatusCodes.Status200OK, "Success", typeof(int))]
        public async Task<IActionResult> GetMine(Guid id)
        {
            return Ok(await mineApiClient.GetMine(id));
        }
    }
}