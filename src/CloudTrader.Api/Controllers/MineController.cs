using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Swashbuckle.AspNetCore.Annotations;
using Microsoft.AspNetCore.Authorization;
using CloudTrader.Api.Service.Services;
using System;

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
    }
}
