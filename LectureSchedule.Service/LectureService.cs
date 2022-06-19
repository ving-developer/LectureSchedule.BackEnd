using AutoMapper;
using LectureSchedule.Data.Persistence.Interface;
using LectureSchedule.Domain;
using LectureSchedule.Service.DTO;
using LectureSchedule.Service.Exceptions;
using LectureSchedule.Service.Interface;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace LectureSchedule.Service
{
    public class LectureService : ILectureService
    {
        private readonly IUnitOfWork _unit;
        private readonly IMapper _mapper;
        private readonly IUploadService _uploadService;

        public LectureService(IUnitOfWork unit, IMapper mapper, IUploadService uploadService)
        {
            _unit = unit;
            _mapper = mapper;
            _uploadService = uploadService;
        }

        public async Task<LectureDTO> GetByIdAsync(int lectureId)
        {
            try
            {
                var lecture = await _unit.LectureRepository.GetSingleByFilterAsync(lec => lec.Id == lectureId);
                return _mapper.Map<LectureDTO>(lecture);
            }
            catch
            {
                throw;
            }
        }

        public async Task<LectureDTO> AddLecture(LectureDTO lectureDTO)
        {
            try
            {
                var lecture = _mapper.Map<Lecture>(lectureDTO);
                _unit.LectureRepository.Add(lecture);
                if (await _unit.CommitAsync())
                {
                    return _mapper.Map<LectureDTO>(lecture);
                }
                return null;
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> DeleteLecture(int lectureId)
        {
            try
            {
                var lecture = await _unit.LectureRepository.GetSingleByFilterAsync(lec => lec.Id == lectureId);
                if (lecture is null) throw new NotFoundException(lectureId);
                _unit.LectureRepository.Delete(lecture);

                return await _unit.CommitAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task<LectureDTO> UpdateLecture(int lectureId, LectureDTO model)
        {
            try
            {
                var lecture = await _unit.LectureRepository.GetSingleByFilterAsync(lec => lec.Id == lectureId);
                if (lecture is null) return null;
                model.Id = lecture.Id;
                _mapper.Map(model, lecture);
                _unit.LectureRepository.Update(lecture);
                if (await _unit.CommitAsync())
                {
                    return model;
                }
                return null;

            }
            catch
            {
                throw;
            }
        }

        public async Task<LectureDTO[]> GetAllAsync()
        {
            try
            {
                var lectures = await _unit.LectureRepository.GetAllAsync();
                return _mapper.Map<LectureDTO[]>(lectures);
            }
            catch
            {
                throw;
            }
        }

        public async Task<LectureDTO[]> GetAllLecturesSpeakersAsync()
        {
            try
            {
                var lectures = await _unit.LectureRepository.GetAllLecturesSpeakersAsync();
                return _mapper.Map<LectureDTO[]>(lectures);
            }
            catch
            {
                throw;
            }
        }

        public async Task<LectureDTO[]> GetLecturesByThemeAsync(string theme)
        {
            try
            {
                var lectures = await _unit.LectureRepository.GetLecturesByThemeAsync(theme);
                return _mapper.Map<LectureDTO[]>(lectures);
            }
            catch
            {
                throw;
            }
        }

        public async Task<string> UploadLectureImage(int lectureId, IFormFile imageFile)
        {
            try
            {
                if (imageFile.Length > 0)
                {
                    var lecture = await _unit.LectureRepository.GetSingleByFilterAsync(lec => lec.Id == lectureId);
                    if (lecture is not null)
                    {
                        _ = _uploadService.DeleteImage(lecture.ImageUrl);
                        var newProfoleImage = await _uploadService.UploadImage(imageFile);
                        lecture.ImageUrl = newProfoleImage;
                        _unit.LectureRepository.Update(lecture);
                        await _unit.CommitAsync();
                        return newProfoleImage;
                    }
                }
                return null;
            }
            catch
            {
                throw;
            }
        }
    }
}
