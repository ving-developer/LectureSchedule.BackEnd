using System.Collections.Generic;

namespace LectureSchedule.Service.DTO
{
    public class SpeakerDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string ImageUrl { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public IEnumerable<LectureDTO> Lectures { get; set; }
    }
}
