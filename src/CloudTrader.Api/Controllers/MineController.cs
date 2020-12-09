using CloudTrader.Api.Domain.Models;
using CloudTrader.Api.Domain.Services;
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
        private readonly IMineApiService _mineApiService;

        public MineController(
            IMineApiService mineApiService)
        {
            _mineApiService = mineApiService;
        }

        [HttpGet("{id}/stock")]
        [SwaggerOperation(
            Summary = "Get stock of a mine",
            Description = "See how much stock a mine currently has")]
        [SwaggerResponse(StatusCodes.Status200OK, "Success", typeof(int))]
        public async Task<IActionResult> GetStockOfMine(Guid id)
        {
            return Ok(await _mineApiService.GetMineStock(id));
        }

        [HttpGet]
        [SwaggerOperation(
            Summary = "Get all mines",
            Description = "Returns an object containing an array of mines")]
        [SwaggerResponse(StatusCodes.Status200OK, "Success", typeof(GetAllMinesResponseModel))]
        public async Task<IActionResult> GetAllMines()
        {
            return Ok(await _mineApiService.GetAllMines());
        }

        [HttpGet("{id}")]
        [SwaggerOperation(
            Summary = "Get details of a mine",
            Description = "See all details of a mine")]
        [SwaggerResponse(StatusCodes.Status200OK, "Success", typeof(int))]
        public async Task<IActionResult> GetMine(Guid id)
        {
            return Ok(await _mineApiService.GetMine(id));
        }
    }
}