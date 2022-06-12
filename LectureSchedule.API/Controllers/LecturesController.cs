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
    public class LecturesController : ControllerBase
    {
        private readonly ILectureService _lectureService;

        public LecturesController(ILectureService lectureService)
        {
            _lectureService = lectureService;
        }

        [HttpGet]
        public async Task<ActionResult<LectureDTO[]>> Get()
        {
            try
            {
                var lectures = await _lectureService.GetAllLecturesSpeakersAsync();
                if (lectures is null) return NoContent();
                return lectures;
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Same error when get lectures");
            }
        }

        [HttpGet("{id}",Name = "FindLecture")]
        public async Task<ActionResult<LectureDTO>> GetByIdAsync([FromRoute]int id)
        {
            try
            {
                var lecture = await _lectureService.GetByIdAsync(id);
                if (lecture is null) return NoContent();
                return lecture;
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Same error when find lecture by id");
            }
        }

        [HttpGet("search-theme")]
        public async Task<ActionResult<LectureDTO[]>> FindByThemeAsync([FromQuery] string theme)
        {
            try
            {
                var lectures = await _lectureService.GetLecturesByThemeAsync(theme);
                if (lectures is null) return NoContent();
                return lectures;
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Same error when find lectures by theme");
            }
        }

        [HttpPost]
        public async Task<ActionResult<LectureDTO>> PostAsync([FromBody] LectureDTO model)
        {
            try
            {
                var createdLecture = await _lectureService.AddLecture(model);
                if(createdLecture == null) return StatusCode(StatusCodes.Status500InternalServerError, $"Error in commit when post lecture: {model}");
                return CreatedAtRoute("FindLecture", new { id = createdLecture.Id }, createdLecture);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Same error when post lecture: {model}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync([FromRoute]int id, [FromBody] LectureDTO model)
        {
            try
            {
                var createdLecture = await _lectureService.UpdateLecture(id, model);
                if (createdLecture == null) return BadRequest();
                return NoContent();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Same error when updating lecture from id: {id}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                return await _lectureService.DeleteLecture(id) ?
                    Ok() :
                    StatusCode(StatusCodes.Status500InternalServerError, $"Error in commit when deleting lecture from id: {id}");
            }
            catch(NotFoundException ex) {
                return BadRequest(ex.Message);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Same error when deleting lecture from id: {id}");
            }
        }
    }
}
