using LectureSchedule.Service.DTO;
using System.Threading.Tasks;

namespace LectureSchedule.Service.Interface
{
    public interface ITicketLotService
    {
        Task<TicketLotDTO[]> GetTicketLotsByLectureId(int lectureId);

        Task<TicketLotDTO> GetTicketLotById(int ticketLotId, int lectureId);

        Task<TicketLotDTO[]> SaveTicketLot(int lectureId, TicketLotDTO[] models);

        Task<bool> DeleteTicketLot(int ticketLotId, int lectureId);
    }
}
