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
    public class LectureController : ControllerBase
    {
        private readonly DataContext _context;

        public LectureController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Lecture>> Get()
        {
            return _context.Lectures;
        }

        [HttpGet("{id}")]
        public ActionResult<Lecture> GetById([FromRoute]int id)
        {
            return _context.Lectures.FirstOrDefault(lec => lec.LectureId == id);
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
