using LectureSchedule.Data.Context;
using LectureSchedule.Data.Repository.Interface;
using LectureSchedule.Domain;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace LectureSchedule.Data.Repository
{
    public class TicketLotRepository : Repository<TicketLot>, ITicketLotRepository
    {
        public TicketLotRepository(LectureScheduleContext context) : base(context){}

        public Task<TicketLot> GetTicketLotById(int ticketLotId, int lectureId)
        {
            return GetSingleByFilterAsync(lot => lot.Id == ticketLotId && lot.LectureId == lectureId);
        }

        public Task<TicketLot[]> GetTicketLotsByLectureId(int lectureId)
        {
            return GetMultipleByFilterAsync(lot => lot.LectureId == lectureId);
        }
    }
}
