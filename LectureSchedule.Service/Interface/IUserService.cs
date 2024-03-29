﻿using LectureSchedule.Service.DTO;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace LectureSchedule.Service.Interface
{
    public interface IUserService
    {
        Task<bool> CheckUserExistAsync(string username);

        Task<UpdateUserDTO> GetUserByUserNameAsync(string username);

        Task<SignInResult> CheckUserPasswordAsync(UserDTO user, string password);

        Task<object> CreateAccountAsync(UserDTO newUser);

        Task<object> UpdateAccountAsync(UpdateUserDTO updateUserDTO);

        Task<object> LoginUserAsync(UserLoginDTO updateUserDTO);
    }
}
