using LectureSchedule.Data.Repository.Interface;
using System.Threading.Tasks;

namespace LectureSchedule.Data.Persistence.Interface
{
    public interface IUnitOfWork
    {
        ILectureRepository LectureRepository { get; }
        ISpeakerRepository SpeakerRepository { get; }
        Task CommitAsync();
    }
}