using System;

namespace LectureSchedule.Service.Exceptions
{
    public class NotFoundException : Exception
    {
        public int Id { get; }
        public override string Message => $"Could not find id {Id}";

        public NotFoundException(int id)
        {
            Id = id;
        }
    }
}
