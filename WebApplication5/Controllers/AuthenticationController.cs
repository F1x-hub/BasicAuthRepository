using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication5.Model;
using WebApplication5.Model.Dto;
using WebApplication5.Service.Abstractions;

namespace WebApplication5.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationRepository _authenticationRepository;
        private readonly IMapper _mapper;

        public AuthenticationController(IAuthenticationRepository authenticationRepository, IMapper mapper)
        {
            _authenticationRepository = authenticationRepository;
            _mapper = mapper;
        }

        [HttpGet("get-all-user")]
        public IActionResult GetUsers()
        {
            var users = _authenticationRepository.GetUsers();
            if (users == null || users.Count == 0)
            {
                return NotFound("No users found.");
            }

            var getUserDto = _mapper.Map<IEnumerable<UserGetDto>>(users);
            return Ok(getUserDto);
        }

        [HttpGet("get-user/{id}")]
        public IActionResult GetUserId(int id)
        {
            var users = _authenticationRepository.GetUserId(id);
            if (users == null)
            {
                return NotFound("No users found.");
            }

            var getUserDto = _mapper.Map<UserGetDto>(users);
            return Ok(getUserDto);
        }

        [HttpPost("register")]
        public IActionResult RegisterUser(UserRegisterDto userRegisterDto)
        {
            
            var user = _mapper.Map<User>(userRegisterDto);

            try
            {
                
                var newUser = _authenticationRepository.Registration(user);
                var userDto = _mapper.Map<UserGetDto>(newUser);

                return Ok(userDto);
            }
            catch (InvalidOperationException ex)
            {
                
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpPost("login")]
        public IActionResult LogIn(UserLoginDto loginDto)
        {
            var user = _authenticationRepository.LogIn(loginDto.UserName, loginDto.Password);
            if (user == null)
            {
                return Unauthorized("Invalid username or password.");
            }

            var userDto = _mapper.Map<UserGetDto>(user);
            return Ok(userDto);
        }

        [HttpDelete("delete/{userId}")]
        public IActionResult DeleteUser(int userId)
        {
            var deletedUser = _authenticationRepository.DeleteUserId(userId);
            if (deletedUser == null)
            {
                return NotFound("User not found.");
            }

            return Ok(new { Message = "User deleted successfully." });
        }

        
        [HttpPut("update/{userId}")]
        public IActionResult UpdateUser(int userId, UserUpdateDto userUpdateDto)
        {
            var user = _mapper.Map<User>(userUpdateDto);

            var updatedUser = _authenticationRepository.UpdateUserId(userId, user);
            if (updatedUser == null)
            {
                return NotFound("User not found.");
            }

            var updatedUserDto = _mapper.Map<UserGetDto>(updatedUser);
            return Ok(updatedUserDto);
        }
    }
}
