using LectureSchedule.Domain;
using System.Threading.Tasks;

namespace LectureSchedule.Data.Repository.Interface
{
    public interface ILectureRepository : IRepository<Lecture>
    {
        Task<Lecture[]> GetAllAsync(int userId);

        Task<Lecture[]> GetLecturesByThemeAsync(int userId,string theme);

        Task<Lecture[]> GetAllLecturesSpeakersAsync(int userId);
    }
}
