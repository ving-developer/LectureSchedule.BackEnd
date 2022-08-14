using AutoMapper;
using LectureSchedule.Data.Persistence.Interface;
using LectureSchedule.Domain.Identity;
using LectureSchedule.Service.DTO;
using LectureSchedule.Service.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace LectureSchedule.Service
{
    public class UserService : IUserService
    {
        public readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unit;
        private readonly ITokenService _tokenService;

        public UserService(UserManager<User> userManager,
                           SignInManager<User> signInManager,
                           IMapper mapper,
                           IUnitOfWork unit,
                           ITokenService tokenService)
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
            this._mapper = mapper;
            this._unit = unit;
            this._tokenService = tokenService;
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

        public async Task<object> CreateAccountAsync(UserDTO newUser)
        {
            try
            {
                if(await CheckUserExistAsync(newUser.UserName))
                {
                    throw new TaskCanceledException("This username already in use.");
                }
                var user = _mapper.Map<User>(newUser);
                var result = await _userManager.CreateAsync(user,newUser.Password);
                if (result.Succeeded)
                {
                    var userToGetToken = _mapper.Map<UpdateUserDTO>(user);
                    return new
                    {
                        userName = user.UserName,
                        firstName = user.FirstName,
                        token = _tokenService.GetToken(userToGetToken).Result
                    };
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

        public async Task<object> UpdateAccountAsync(UpdateUserDTO updateUserDTO)
        {
            try
            {
                var user = await _userManager.Users.FirstOrDefaultAsync(x => x.UserName == updateUserDTO.UserName);
                if(user is not null)
                {
                    updateUserDTO.Id = user.Id;
                    _mapper.Map(updateUserDTO, user);
                    if(updateUserDTO.Password != null)
                    {
                        var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                        await _userManager.ResetPasswordAsync(user, token, updateUserDTO.Password);
                    }
                    _unit.UserRepository.Update(user);
                    if(await _unit.CommitAsync())
                    {
                        var userToGetToken = _mapper.Map<UpdateUserDTO>(user);
                        return new
                        {
                            userName = user.UserName,
                            firstName = user.FirstName,
                            token = _tokenService.GetToken(userToGetToken).Result
                        };
                    }
                }
                return null;
            }
            catch
            {
                throw;
            }
        }
        
        public async Task<object> LoginUserAsync(UserLoginDTO userLoginDTO)
        {
            try
            {
                var user = await GetUserByUserNameAsync(userLoginDTO.UserName);
                var userDTO = _mapper.Map<UserDTO>(user);
                if (user is null) throw new TaskCanceledException("User not found");
                var loginResult = await CheckUserPasswordAsync(userDTO, userLoginDTO.Password);
                if (loginResult.Succeeded)
                    return new
                    {
                        userName = user.UserName,
                        firstName = user.FirstName,
                        token = _tokenService.GetToken(user).Result
                    };
                return null;
            }
            catch
            {
                throw;
            }
        }
    }
}
