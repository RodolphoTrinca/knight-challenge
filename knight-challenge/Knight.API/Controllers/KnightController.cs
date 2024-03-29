using Knight.Application.Interface;
using Knight.DTOs;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace Knight.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class KnightController : ControllerBase
    {
        private readonly IKnightService _service;
        private readonly ILogger _logger;

        public KnightController(IKnightService service, ILogger<KnightController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] int skip = 0, [FromQuery] int take = 100)
        {
            try
            {
                var knights = _service.GetAll(skip, take);

                if (knights == null)
                {
                    return NotFound();
                }

                var dtos = knights.Select(k => new KnightDTO(k)).ToList();
                return Ok(dtos);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error to get knights:");
                return BadRequest();
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Save(KnightDTO knight)
        {
            try
            {
                var knightData = knight.ToKnight();
                _service.Save(knightData);

                return Ok(knightData);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error to save knight: ");
                return BadRequest();
            }
        }

        [HttpDelete("{id:string}")]
        public async Task<IActionResult> Delete(ObjectId id)
        {
            try
            {
                var knight = _service.GetById(id);

                if (knight == null)
                {
                    return NotFound();
                }

                _service.Remove(knight);

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error to delete knight: ");
                return BadRequest();
            }
        }

        [HttpPut("{id:string}")]
        public async Task<IActionResult> Put([FromRoute] ObjectId id, [FromBody] KnightDTO knightDTO)
        {
            try
            {
                if (id != knightDTO.Id)
                    return BadRequest();

                if (_service.GetById(id) is null)
                    return NotFound();

                _service.Save(knightDTO.ToKnight());

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error to update knight:");
                return BadRequest();
            }
        }

        //[HttpPatch("{id:string}")]
        //public async Task<IActionResult> Patch([FromRoute] ObjectId id, JsonPatchDocument<IncomeDTO> income)
        //{

        //}
    }
}
