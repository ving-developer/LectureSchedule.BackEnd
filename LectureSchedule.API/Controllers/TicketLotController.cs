using LectureSchedule.Service.DTO;
using LectureSchedule.Service.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using LectureSchedule.Service.Exceptions;

namespace LectureSchedule.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TicketLotController : ControllerBase
    {
        private readonly ITicketLotService _ticketLotService;

        public TicketLotController(ITicketLotService ticketLotService)
        {
            _ticketLotService = ticketLotService;
        }

        [HttpGet("{lectureId}")]
        public async Task<ActionResult<TicketLotDTO[]>> GetAsync(int lectureId)
        {
            try
            {
                var ticketLots = await _ticketLotService.GetTicketLotsByLectureId(lectureId);
                if (ticketLots is null) return NoContent();
                return ticketLots;
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Same error when get ticket lots");
            }
        }

        [HttpPut("{lectureId}")]
        public async Task<IActionResult> PutAsync([FromRoute]int lectureId, [FromBody] TicketLotDTO[] models)
        {
            try
            {
                var updatedTicketLots = await _ticketLotService.SaveTicketLot(lectureId, models);
                if (updatedTicketLots == null) return BadRequest();
                return NoContent();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Same error when updating ticket lots from lecture: {lectureId}");
            }
        }

        [HttpDelete("{lectureId}/{ticketLotId}")]
        public async Task<IActionResult> DeleteAsync(int lectureId, int ticketLotId)
        {
            try
            {
                return await _ticketLotService.DeleteTicketLot(ticketLotId, lectureId) ?
                    Ok() :
                    StatusCode(StatusCodes.Status500InternalServerError, $"Error in commit when deleting ticket lot from id: {ticketLotId}");
            }
            catch(NotFoundException ex) {
                return BadRequest(ex.Message);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Same error when deleting ticket lot from id: {ticketLotId}");
            }
        }
    }
}
