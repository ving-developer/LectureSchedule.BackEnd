using System.Collections.Generic;

namespace LectureSchedule.Domain
{
    public class Speaker
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string ImageUrl { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public IEnumerable<SpeakerLecture> SpeakerLectures { get; set; }
    }
}
