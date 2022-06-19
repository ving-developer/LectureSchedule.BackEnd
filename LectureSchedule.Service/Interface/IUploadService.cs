using LectureSchedule.Service.DTO;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace LectureSchedule.Service.Interface
{
    public interface IUploadService
    {

        Task<string> UploadImage(IFormFile imageFile);

        Task DeleteImage(string imageName);
    }
}
