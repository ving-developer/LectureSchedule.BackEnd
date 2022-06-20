using LectureSchedule.Data.Context;
using LectureSchedule.Data.Repository.Interface;
using LectureSchedule.Domain.Identity;

namespace LectureSchedule.Data.Repository
{
    internal class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(LectureScheduleContext context) : base(context){ }
    }
}
