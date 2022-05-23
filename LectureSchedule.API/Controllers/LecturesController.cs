using LectureSchedule.API.Data;
using LectureSchedule.API.Models;
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
        private readonly DataContext _context;

        public LecturesController(DataContext context)
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
            return _context.Lectures.FirstOrDefault(lec => lec.LectureId == id);
        }

        [HttpPost]
        public ActionResult<Lecture> Post([FromBody] Lecture lecture)
        {
            _context.Lectures.Add(lecture);
            _context.SaveChanges();
            return CreatedAtRoute("FindLecture", new { id = lecture.LectureId }, lecture);
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
