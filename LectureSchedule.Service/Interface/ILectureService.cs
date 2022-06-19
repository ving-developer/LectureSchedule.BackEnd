using LectureSchedule.Service.DTO;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace LectureSchedule.Service.Interface
{
    public interface ILectureService
    {
        Task<LectureDTO> GetByIdAsync(int lectureId);

        Task<LectureDTO> AddLecture(LectureDTO lectureDTO);

        Task<LectureDTO> UpdateLecture(int lectureId, LectureDTO model);

        Task<bool> DeleteLecture(int lectureId);

        Task<LectureDTO[]> GetAllAsync();

        Task<LectureDTO[]> GetLecturesByThemeAsync(string theme);

        Task<LectureDTO[]> GetAllLecturesSpeakersAsync();

        Task<string> UploadLectureImage(int lectureId, IFormFile imageFile);
    }
}
