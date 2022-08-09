using LectureSchedule.Data.Context;
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

        public async Task<Lecture[]> GetAllAsync(int userId)
        {
            return await _context.Lectures
                           .Include(lec => lec.TicketLots)
                           .Include(lec => lec.PublicityCampaigns)
                           .Where(lec => lec.UserId == userId)
                           .OrderBy(lec => lec.Id)
                           .ToArrayAsync();
        }

        public async Task<Lecture[]> GetAllLecturesSpeakersAsync(int userId)
        {
            return await _context.Lectures
                           .Include(lec => lec.TicketLots)
                           .Include(lec => lec.PublicityCampaigns)
                           .Include(lec => lec.SpeakerLectures)
                           .ThenInclude(sl => sl.Speaker)
                           .Where(lec => lec.UserId == userId)
                           .OrderBy(lec => lec.Id)
                           .ToArrayAsync();
        }

        public async Task<Lecture[]> GetLecturesByThemeAsync(int userId, string theme)
        {
            return await _context.Lectures
                           .Include(lec => lec.TicketLots)
                           .Include(lec => lec.PublicityCampaigns)
                           .Include(lec => lec.SpeakerLectures)
                           .ThenInclude(sl => sl.Speaker)
                           .Where(lec => lec.Theme.ToLower().Contains(theme.ToLower())
                                && lec.UserId == userId)
                           .OrderBy(lec => lec.Id)
                           .ToArrayAsync();
        }
    }
}
