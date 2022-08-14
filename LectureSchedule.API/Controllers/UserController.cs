using LectureSchedule.API.Extensions;
using LectureSchedule.Service.DTO;
using LectureSchedule.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace LectureSchedule.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("get-user")]
        public async Task<ActionResult<UpdateUserDTO>> GetUserAsync()
        {
            try
            {
                var username = User.GetUserName();
                var user = await _userService.GetUserByUserNameAsync(username);
                return Ok(user);
            }catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Same error when get user");
            }
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<ActionResult<object>> RegisterAsync(UserDTO user)
        {
            try
            {
                var createdUser = await _userService.CreateAccountAsync(user);
                if(createdUser is null) return BadRequest("Fail to create user, try again");
                return Ok(createdUser);
            }
            catch(TaskCanceledException ex)
            {
                return BadRequest(ex.Message);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Same error when register user");
            }
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<ActionResult<UpdateUserDTO>> LoginUserAsync(UserLoginDTO loginDTO)
        {
            try
            {
                var result = await _userService.LoginUserAsync(loginDTO);
                if(result is null)
                    return Unauthorized();
                return Ok(result);
            }
            catch (TaskCanceledException ex)
            {
                return Unauthorized(ex.Message);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Same error when login user");
            }
        }

        [HttpPut("update")]
        public async Task<ActionResult<UserDTO>> UpdateAsync(UpdateUserDTO updateUserDto)
        {
            try
            {
                if (updateUserDto.UserName != User.GetUserName())
                    return Unauthorized();
                var updatedUser = await _userService.UpdateAccountAsync(updateUserDto);
                if (updatedUser is null) return NotFound("Can't be find user");
                return Ok(updatedUser);
            }
            catch (TaskCanceledException ex)
            {
                return BadRequest(ex.Message);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Same error when register user");
            }
        }

    }
}
