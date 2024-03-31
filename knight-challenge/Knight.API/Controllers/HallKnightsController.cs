using Knight.Application.Interface;
using Microsoft.AspNetCore.Mvc;

namespace Knight.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class HallKnightsController : ControllerBase
    {
        private readonly IKnightService _knightService;
        private readonly ILogger<HallKnightsController> _logger;

        public HallKnightsController(IKnightService knightService, ILogger<HallKnightsController> logger)
        {
            _knightService = knightService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> ListKnights(string filter = "", int skip = 0, int take = 10){
            try
            {
                var knights = _knightService.GetHallOfKnights(filter, skip, take);

                if(knights is null){
                    return NotFound();
                }

                return Ok(knights);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error to get hall of knights");
                return BadRequest();
            }
        }
    }
}

