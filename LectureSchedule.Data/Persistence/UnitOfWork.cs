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
        private TicketLotRepository _ticketLotRepository;
        private UserRepository _userRepository;

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

        public ITicketLotRepository TicketLotRepository
        {
            get { return _ticketLotRepository ?? new TicketLotRepository(_context); }
        }

        public IUserRepository UserRepository
        {
            get { return _userRepository ?? new UserRepository(_context); }
        }

        public async Task<bool> CommitAsync()
        {
            return (await _context.SaveChangesAsync() > 0);
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}
