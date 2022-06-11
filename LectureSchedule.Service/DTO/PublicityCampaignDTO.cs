namespace LectureSchedule.Service.DTO
{
    public class PublicityCampaignDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string URL { get; set; }

        public int LectureId { get; set; }

        public LectureDTO Lecture { get; set; }
    }
}
