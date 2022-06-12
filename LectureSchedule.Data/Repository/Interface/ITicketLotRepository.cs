using LectureSchedule.Domain;
using System.Threading.Tasks;

namespace LectureSchedule.Data.Repository.Interface
{
    public interface ITicketLotRepository : IRepository<TicketLot>
    {
        Task<TicketLot[]> GetTicketLotsByLectureId(int lectureId);

        Task<TicketLot> GetTicketLotById(int ticketLotId,int lectureId);
    }
}
