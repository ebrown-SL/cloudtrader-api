using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CloudTrader.Api.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Swashbuckle.AspNetCore.Annotations;
using Microsoft.AspNetCore.Authorization;
using CloudTrader.Api.Service.Services;

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

        [HttpPost("stock/{id}")]
        [SwaggerOperation(
            Summary = "Process purchase request",
            Description = "Update the current user's balance; update the current user's stock; update the mine's stock")]
        [SwaggerResponse(StatusCodes.Status200OK, "Success", typeof(int))]
        public async Task<IActionResult> GetStockOfMine(int id)
        {
            return Ok(await _mineApiService.GetMineStock(id));
        }
    }
}
