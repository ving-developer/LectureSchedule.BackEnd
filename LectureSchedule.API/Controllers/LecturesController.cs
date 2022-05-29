using LectureSchedule.Data.Context;
using LectureSchedule.Domain;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LectureSchedule.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LecturesController : ControllerBase
    {
        private readonly LectureScheduleContext _context;

        public LecturesController(LectureScheduleContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Lecture>> Get()
        {
            return _context.Lectures;
        }

        [HttpGet("{id}",Name = "FindLecture")]
        public ActionResult<Lecture> GetById([FromRoute]int id)
        {
            return _context.Lectures.FirstOrDefault(lec => lec.Id == id);
        }

        [HttpPost]
        public ActionResult<Lecture> Post([FromBody] Lecture lecture)
        {
            _context.Lectures.Add(lecture);
            _context.SaveChanges();
            return CreatedAtRoute("FindLecture", new { id = lecture.Id }, lecture);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id)
        {
            return Ok($"LectureController Put with id {id} at {DateTime.Now}");
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var lecture = _context.Lectures.FirstOrDefault(lec => lec.Id == id);
            if (lecture == null)
                return NotFound();
            _context.Lectures.Remove(lecture);
            _context.SaveChanges();
            return Ok(lecture);
        }
    }
}
