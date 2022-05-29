using LectureSchedule.Domain;
using System.Threading.Tasks;

namespace LectureSchedule.Service.Interface
{
    public interface ILectureService
    {
        Task<Lecture> GetByIdAsync(int lectureId);

        Task<Lecture> AddLecture(Lecture lecture);

        Task<Lecture> UpdateLecture(int lectureId, Lecture model);

        Task<bool> DeleteLecture(int lectureId);

        Task<Lecture[]> GetAllAsync();

        Task<Lecture[]> GetLecturesByThemeAsync(string theme);

        Task<Lecture[]> GetAllLecturesSpeakersAsync();
    }
}
