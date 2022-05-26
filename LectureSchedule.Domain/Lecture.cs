using System;
using System.Collections.Generic;

namespace LectureSchedule.Domain
{
    public class Lecture
    {
        public int Id { get; set; }

        public string Local { get; set; }

        public DateTime? Date { get; set; }

        public string Theme { get; set; }

        public int MaxPeopleSupported { get; set; }

        public string Adress { get; set; }

        public string ImageUrl { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public IEnumerable<TicketLot> TicketLots { get; set; }

        public IEnumerable<PublicityCampaign> PublicityCampaigns { get; set; }

        public IEnumerable<SpeakerLecture> SpeakerLectures { get; set; }
    }
}
