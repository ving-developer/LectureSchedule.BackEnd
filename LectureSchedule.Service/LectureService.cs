using LectureSchedule.Data.Persistence.Interface;
using LectureSchedule.Domain;
using LectureSchedule.Service.Interface;
using System;
using System.Threading.Tasks;

namespace LectureSchedule.Service
{
    public class LectureService : ILectureService
    {
        private readonly IUnitOfWork _unit;

        public LectureService(IUnitOfWork unit)
        {
            _unit = unit;
        }

        public async Task<Lecture> GetByIdAsync(int lectureId)
        {
            try
            {
                return await _unit.LectureRepository.GetSingleByFilterAsync(lec => lec.Id == lectureId);
            }
            catch
            {
                throw;
            }
        }

        public async Task<Lecture> AddLecture(Lecture lecture)
        {
            try
            {
                _unit.LectureRepository.Add(lecture);
                if(await _unit.CommitAsync())
                {
                    return lecture;
                }
                return null;
            }catch
            {
                throw;
            }
        }

        public async Task<bool> DeleteLecture(int lectureId)
        {
            try
            {
                var lecture = await GetByIdAsync(lectureId);
                if (lecture is null) throw new Exception("Cannot find Lecture to be deleted");
                _unit.LectureRepository.Delete(lecture);
                
                return await _unit.CommitAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task<Lecture> UpdateLecture(int lectureId, Lecture model)
        {
            try
            {
                var lecture = await GetByIdAsync(lectureId);
                if (lecture is null) return null;
                model.Id = lectureId;
                _unit.LectureRepository.Update(model);
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

        public async Task<Lecture[]> GetAllAsync()
        {
            try
            {
                return await _unit.LectureRepository.GetAllAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task<Lecture[]> GetAllLecturesSpeakersAsync()
        {
            try
            {
                return await _unit.LectureRepository.GetAllLecturesSpeakersAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task<Lecture[]> GetLecturesByThemeAsync(string theme)
        {
            try
            {
                return await _unit.LectureRepository.GetLecturesByThemeAsync(theme);
            }
            catch
            {
                throw;
            }
        }
    }
}
