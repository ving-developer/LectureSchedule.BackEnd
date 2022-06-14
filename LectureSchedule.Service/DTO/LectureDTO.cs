
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LectureSchedule.Service.DTO
{
    public class LectureDTO
    {
        public int Id { get; set; }

        public string Local { get; set; }

        public DateTime Date { get; set; }

        [Required, StringLength(50,MinimumLength = 4)]
        public string Theme { get; set; }

        [Range(1,120000)]
        public int MaxPeopleSupported { get; set; }

        public string Adress { get; set; }

        [RegularExpression(@".*\.(?:jpg|gif|png|bmp)", ErrorMessage = "{0} must be a valid image url (gif, jpeg, jpg or bmp).")]
        public string ImageUrl { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        [Required, Phone]
        public string Phone { get; set; }

        public IEnumerable<TicketLotDTO> TicketLots { get; set; }

        public IEnumerable<PublicityCampaignDTO> PublicityCampaigns { get; set; }

        public IEnumerable<SpeakerDTO> SpeakerLectures { get; set; }
    }
}
