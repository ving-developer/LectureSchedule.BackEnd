using System;

namespace LectureSchedule.API.Models
{
    public class Lecture
    {
        public int LectureId { get; set; }

        public string Local { get; set; }

        public DateTime Date { get; set; }

        public string Theme { get; set; }

        public int MaxPeopleSupported { get; set; }

        public string Adress { get; set; }

        public string ImageUrl { get; set; }


    }
}
