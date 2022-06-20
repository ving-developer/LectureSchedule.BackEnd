using AutoMapper;
using LectureSchedule.Data.Persistence.Interface;
using LectureSchedule.Domain.Identity;
using LectureSchedule.Service.DTO;
using LectureSchedule.Service.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace LectureSchedule.Service
{
    public class UserService : IUserService
    {
        public readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unit;

        public UserService(UserManager<User> userManager,
                           SignInManager<User> signInManager,
                           IMapper mapper,
                           IUnitOfWork unit)
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
            this._mapper = mapper;
            this._unit = unit;
        }


        public async Task<bool> CheckUserExistAsync(string UserName)
        {
            try
            {
                return await _userManager.Users
                                         .AnyAsync(usr => usr.UserName == UserName.ToLower());
            }
            catch
            {
                throw;
            }
        }

        public async Task<SignInResult> CheckUserPasswordAsync(UserDTO userDTO, string password)
        {
            try
            {
                var user = await _userManager.Users
                    .SingleOrDefaultAsync(user => user.UserName == userDTO.UserName.ToLower());

                return await _signInManager.CheckPasswordSignInAsync(user, password, false);
            }
            catch
            {
                throw;
            }
        }

        public async Task<UserDTO> CreateAccountAsync(UserDTO newUser)
        {
            try
            {
                var user = _mapper.Map<User>(newUser);
                var result = await _userManager.CreateAsync(user,newUser.Password);
                if (result.Succeeded)
                {
                    var userToReturn = _mapper.Map<UserDTO>(user);
                    return userToReturn;
                }

                return null;
            }
            catch
            {
                throw;
            }
        }

        public async Task<UpdateUserDTO> GetUserByUserNameAsync(string username)
        {
            try
            {
                var user = await _unit.UserRepository.GetSingleByFilterAsync(usr => usr.UserName == username);
                if(user is not null)
                {
                    var userDTO = _mapper.Map<UpdateUserDTO>(user);

                    return userDTO;
                }

                return null;
            }
            catch
            {
                throw;
            }
        }

        public async Task<UpdateUserDTO> UpdateAccountAsync(UpdateUserDTO updateUserDTO)
        {
            try
            {
                var user = await _unit.UserRepository.GetSingleByFilterAsync(usr => usr.UserName == updateUserDTO.UserName);
                if(user is not null)
                {
                    _mapper.Map(updateUserDTO, user);
                    var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                    var result = _userManager.ResetPasswordAsync(user, token, updateUserDTO.Password);
                    _unit.UserRepository.Update(user);
                    if(await _unit.CommitAsync())
                    {
                        var userReturn = _mapper.Map<UpdateUserDTO>(user);
                        return userReturn;
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
