using LectureSchedule.API.Models;
using Microsoft.AspNetCore.Mvc;
using System;

namespace LectureSchedule.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LectureController : ControllerBase
    {
        [HttpGet]
        public ActionResult<Lecture> Get()
        {
            return new Lecture();
        }

        [HttpPost]
        public IActionResult Post()
        {
            return Ok($"LectureController Post at {DateTime.Now}");
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id)
        {
            return Ok($"LectureController Put with id {id} at {DateTime.Now}");
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return Ok($"LectureController Put with id {id} at {DateTime.Now}");
        }
    }
}
