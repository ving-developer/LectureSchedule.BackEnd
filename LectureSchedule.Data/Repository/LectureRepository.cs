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

        public async Task<Lecture[]> GetAllAsync()
        {
            return await _context.Lectures
                           .Include(lec => lec.TicketLots)
                           .Include(lec => lec.PublicityCampaigns)
                           .OrderBy(lec => lec.Id)
                           .ToArrayAsync();
        }

        public async Task<Lecture[]> GetAllLecturesSpeakersAsync()
        {
            return await _context.Lectures
                           .Include(lec => lec.TicketLots)
                           .Include(lec => lec.PublicityCampaigns)
                           .Include(lec => lec.SpeakerLectures)
                           .ThenInclude(sl => sl.Speaker)
                           .OrderBy(lec => lec.Id)
                           .ToArrayAsync();
        }

        public async Task<Lecture[]> GetLecturesByThemeAsync(string theme)
        {
            return await _context.Lectures
                           .Include(lec => lec.TicketLots)
                           .Include(lec => lec.PublicityCampaigns)
                           .Include(lec => lec.SpeakerLectures)
                           .ThenInclude(sl => sl.Speaker)
                           .OrderBy(lec => lec.Id)
                           .Where(lec => lec.Theme.ToLower().Contains(theme.ToLower()))
                           .ToArrayAsync();
        }
    }
}
