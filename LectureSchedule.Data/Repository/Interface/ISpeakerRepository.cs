using LectureSchedule.Domain;
using System.Threading.Tasks;

namespace LectureSchedule.Data.Repository.Interface
{
    public interface ISpeakerRepository : IRepository<Speaker>
    {
        Task<Speaker[]> GetAllSpeakersByNameAsync(string name);

        Task<Speaker[]> GetAllSpeakerLectureAsync();
    }
}
