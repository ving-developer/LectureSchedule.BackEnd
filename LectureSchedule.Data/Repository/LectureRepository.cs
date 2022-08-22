using LectureSchedule.Data.Context;
using LectureSchedule.Data.Pagination;
using LectureSchedule.Data.Repository.Interface;
using LectureSchedule.Domain;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace LectureSchedule.Data.Repository
{
    public class LectureRepository : Repository<Lecture>, ILectureRepository
    {
        public LectureRepository(LectureScheduleContext context) : base(context){}

        public async Task<PageList<Lecture>> GetAllAsync(int userId, PageParams pageParams, bool includeSpeakers)
        {
            var query = _context.Lectures
                           .Include(lec => lec.TicketLots)
                           .Include(lec => lec.PublicityCampaigns)
                           .Where(lec => lec.Theme.ToLower().Contains(pageParams.Term.ToLower())
                                && lec.UserId == userId);

            if (includeSpeakers)
            {
                query = query.Include(l => l.SpeakerLectures)
                    .ThenInclude(sl => sl.Speaker);
            }

            return await PageList <Lecture>.CreateAsync(query.OrderBy(lec => lec.Id), pageParams.PageNumber, pageParams.PageSize);
        }
    }
}
