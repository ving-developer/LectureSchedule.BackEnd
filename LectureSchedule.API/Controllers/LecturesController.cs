using LectureSchedule.Service.DTO;
using LectureSchedule.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

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
                throw;
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
                throw;
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
                throw;
            }
        }

        [HttpPost]
        public async Task<ActionResult<LectureDTO>> PostAsync([FromBody] LectureDTO model)
        {
            try
            {
                var createdLecture = await _lectureService.AddLecture(model);
                if(createdLecture == null) return BadRequest();
                return CreatedAtRoute("FindLecture", new { id = createdLecture.Id }, createdLecture);
            }
            catch
            {
                throw;
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
                throw;
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                return await _lectureService.DeleteLecture(id) ?
                    Ok() :
                    BadRequest();
            }
            catch
            {
                throw new System.Exception($"Error deleting lecture from id: {id}");
            }
        }
    }
}
