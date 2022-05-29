using LectureSchedule.Domain;
using System.Threading.Tasks;

namespace LectureSchedule.Data.Repository.Interface
{
    public interface ILectureRepository : IRepository<Lecture>
    {
        Task<Lecture[]> GetAllAsync();

        Task<Lecture[]> GetLecturesByThemeAsync(string theme);

        Task<Lecture[]> GetAllLecturesSpeakersAsync();
    }
}
