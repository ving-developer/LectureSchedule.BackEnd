namespace LectureSchedule.Service.DTO
{
    public class TicketLotDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public string StartDate { get; set; }

        public string EndDate { get; set; }

        public int Quantity { get; set; }

        public int LectureId { get; set; }

        public LectureDTO Lecture { get; set; }
    }
}
