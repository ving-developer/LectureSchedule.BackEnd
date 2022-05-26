namespace LectureSchedule.Domain
{
    public class SpeakerLecture
    {
        public int SpeakerId { get; set; }

        public Speaker Speaker { get; set; }

        public int LectureId { get; set; }

        public Lecture Lecture { get; set; }
    }
}