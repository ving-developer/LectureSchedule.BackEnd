using LectureSchedule.Service.DTO;
using System.Threading.Tasks;

namespace LectureSchedule.Service.Interface
{
    public interface ITokenService
    {
        Task<string> GetToken(UpdateUserDTO updateUserDTO);
    }
}
