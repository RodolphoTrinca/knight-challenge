using Knight.Application.Interface;
using Knight.DTOs;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace Knight.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class KnightsController : ControllerBase
    {
        private readonly IKnightService _service;
        private readonly ILogger _logger;

        public KnightsController(IKnightService service, ILogger<KnightsController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetById(string id)
        {
            try
            {
                var objId = new ObjectId(id);
                var knight = _service.GetById(objId);

                if (knight is null)
                {
                    return NotFound();
                }

                return Ok(knight);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error to get knight id: {id} ");
                return BadRequest();
            }
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Get([FromQuery]string filters = "", [FromQuery] int skip = 0, [FromQuery] int take = 100)
        {
            try
            {
                var knights = _service.GetAll(filters, skip, take);

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
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Save(KnightDTO knight)
        {
            try
            {
                var knightData = knight.ToKnight();
                _service.Save(knightData);

                return CreatedAtAction(nameof(GetById), new { id = knightData.Id }, knightData);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error to save knight: ");
                return BadRequest();
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                var objId = new ObjectId(id);
                var knight = _service.GetById(objId);

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

        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromRoute] string id, [FromBody] KnightDTO knightDTO)
        {
            try
            {
                var objId = new ObjectId(id);
                if (_service.GetById(objId) is null)
                    return NotFound();

                var knightUpdate = knightDTO.ToKnight();
                knightUpdate.Id = objId;
                
                _service.Save(knightUpdate);

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error to update knight:");
                return BadRequest();
            }
        }

        // [HttpPatch("{id}")]
        // public async Task<IActionResult> Patch([FromRoute] string id, JsonPatchDocument<KnightDTO> knight)
        // {
        //     try
        //     {
        //         var objId = new ObjectId(id);
        //         var knightUpdate = _service.GetById(objId);
        //         if (knightUpdate is null)
        //             return NotFound();

        //         var knightDTO = new KnightDTO(knightUpdate);

        //         knight.ApplyTo(knightDTO, ModelState);

        //         if(!TryValidateModel(knightDTO)){
        //             return ValidationProblem(ModelState);
        //         }

        //         _service.Save(knightDTO.ToKnight());
        //         return NoContent();
        //     }
        //     catch (Exception ex)
        //     {
        //         _logger.LogError(ex, "Error to update knight: ");
        //         return BadRequest(ex.Message);
        //     }
        // }
    }
}
