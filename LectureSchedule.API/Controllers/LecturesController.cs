using LectureSchedule.Domain;
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
        public async Task<ActionResult<Lecture[]>> Get()
        {
            try
            {
                var lectures = await _lectureService.GetAllLecturesSpeakersAsync();
                if (lectures is null) return NotFound("No lectures found");
                return lectures;
            }
            catch
            {
                throw;
            }
        }

        [HttpGet("{id}",Name = "FindLecture")]
        public async Task<ActionResult<Lecture>> GetByIdAsync([FromRoute]int id)
        {
            try
            {
                var lecture = await _lectureService.GetByIdAsync(id);
                if (lecture is null) return NotFound("No lecture found");
                return lecture;
            }
            catch
            {
                throw;
            }
        }

        [HttpGet("search-theme")]
        public async Task<ActionResult<Lecture[]>> FindByThemeAsync([FromQuery] string theme)
        {
            try
            {
                var lectures = await _lectureService.GetLecturesByThemeAsync(theme);
                if (lectures is null) return NotFound("No lecture found");
                return lectures;
            }
            catch
            {
                throw;
            }
        }

        [HttpPost]
        public async Task<ActionResult<Lecture>> PostAsync([FromBody] Lecture model)
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
        public async Task<IActionResult> PutAsync([FromRoute]int id, [FromBody] Lecture model)
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
                throw;
            }
        }
    }
}
