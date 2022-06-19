using LectureSchedule.Service.DTO;
using LectureSchedule.Service.Interface;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Threading.Tasks;

namespace LectureSchedule.Service
{
    public class UploadService : IUploadService
    {
        private readonly string IMAGES_ROOT_PATH = Path.GetFullPath("Resources/Images");

        public async Task<string> UploadImage(IFormFile imageFile)
        {
            string imageName = $"{Guid.NewGuid()}{Path.GetExtension(imageFile.FileName)}";
            var imagePath = Path.Combine(IMAGES_ROOT_PATH,imageName);
            using (var fileStream = new FileStream(imagePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(fileStream);
            }
            return imageName;
        }

        public async Task DeleteImage(string imageName)
        {
            var imagePath = Path.Combine(IMAGES_ROOT_PATH, imageName);
            if (File.Exists(imagePath))
            {
                File.Delete(imagePath);
            }
        }
    }
}
