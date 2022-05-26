using System;

namespace LectureSchedule.Domain
{
    public class TicketLot
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public int Quantity { get; set; }

        public int LectureId { get; set; }

        public Lecture Lecture { get; set; }
    }
}
