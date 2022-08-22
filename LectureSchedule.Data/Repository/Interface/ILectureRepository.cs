using LectureSchedule.Data.Pagination;
using LectureSchedule.Domain;
using System.Threading.Tasks;

namespace LectureSchedule.Data.Repository.Interface
{
    public interface ILectureRepository : IRepository<Lecture>
    {
        Task<PageList<Lecture>> GetAllAsync(int userId, PageParams pageParams, bool includeSpeakers);
    }
}
