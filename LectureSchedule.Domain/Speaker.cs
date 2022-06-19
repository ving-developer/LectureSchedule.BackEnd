using LectureSchedule.Domain.Identity;
using System.Collections.Generic;

namespace LectureSchedule.Domain
{
    public class Speaker
    {
        public int Id { get; set; }

        public string Description { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }

        public IEnumerable<SpeakerLecture> SpeakerLectures { get; set; }
    }
}
