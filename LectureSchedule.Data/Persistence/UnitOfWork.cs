using LectureSchedule.Data.Context;
using LectureSchedule.Data.Persistence.Interface;
using LectureSchedule.Data.Repository;
using LectureSchedule.Data.Repository.Interface;
using System.Threading.Tasks;

namespace LectureSchedule.Data.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private LectureScheduleContext _context;
        private LectureRepository _lectureRepository;
        private SpeakerRepository _speakerRepository;

        public UnitOfWork(LectureScheduleContext context)
        {
            _context = context;
        }

        public ILectureRepository LectureRepository
        {
            get { return _lectureRepository ?? new LectureRepository(_context); }
        }

        public ISpeakerRepository SpeakerRepository
        {
            get { return _speakerRepository ?? new SpeakerRepository(_context); }
        }

        public async Task CommitAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}
