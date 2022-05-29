using LectureSchedule.Data.Context;
using LectureSchedule.Data.Repository.Interface;
using LectureSchedule.Domain;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace LectureSchedule.Data.Repository
{
    public class SpeakerRepository : Repository<Speaker>, ISpeakerRepository
    {
        public SpeakerRepository(LectureScheduleContext context) : base(context){}

        public async Task<Speaker[]> GetAllAsync()
        {
            return await _context.Speakers
                           .Include(speak => speak.SpeakerLectures)
                           .ThenInclude(sl => sl.Speaker)
                           .OrderBy(speak => speak.Id)
                           .ToArrayAsync();
        }

        public async Task<Speaker[]> GetAllSpeakerLectureAsync()
        {
            return await _context.Speakers
                           .Include(speak => speak.SpeakerLectures)
                           .ThenInclude(sl => sl.Lecture)
                           .OrderBy(speak => speak.Id)
                           .ToArrayAsync();
        }

        public async Task<Speaker[]> GetAllSpeakersByNameAsync(string name)
        {
            return await _context.Speakers
                           .Include(speak => speak.SpeakerLectures)
                           .ThenInclude(sl => sl.Speaker)
                           .OrderBy(speak => speak.Id)
                           .Where(speak => speak.Name.ToLower().Contains(name.ToLower()))
                           .ToArrayAsync();
        }
    }
}
