namespace LectureSchedule.Domain
{
    public class PublicityCampaign
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string URL { get; set; }

        public int LectureId { get; set; }

        public Lecture Lecture { get; set; }
    }
}
