using AutoMapper;
using Domain.Entities;
using Domain.Interfaces.Interfaceservices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.DTOs;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "admin")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UserController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> AddUser(User user)
        {
            var result = await _userService.AddUser(user);
            if (result.Status == true)
             return CreatedAtAction(nameof(GetUserById), new { id = user.Id }, user);

            return BadRequest(result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUser(User user)
        {
            var result = await _userService.UpdateUser(user);
            if (result.Status == true)
                return Ok(result);
            return BadRequest(result);
        }

        [HttpPatch("UpdateWalletBalance/{id}")]
        public async Task<IActionResult> UpdateBalance(int id, decimal value)
        {
            var result = await _userService.UpdateWalletBalance(id, value);
            if (result.Status == true)
                return Ok(result);
            return BadRequest(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var result = await _userService.DeleteUser(id);
            if (result.Status == true)
                return Ok(result);
            return NotFound(result);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetAllUsers()
        {
            var users = await _userService.GetAllUsers();
            var userDtos = _mapper.Map<List<UserDto>>(users);
            return Ok(userDtos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var user = await _userService.GetUserById(id);
            if (user != null)
            {
                var userDto = _mapper.Map<UserDto>(user);
                return Ok(userDto);
            }
            return NotFound();
        }

        [HttpGet("email/{email}")]
        public async Task<IActionResult> GetUserByEmail(string email)
        {
            var user = await _userService.GetUserByEmail(email);
            if (user != null)
            {
                var userDto = _mapper.Map<UserDto>(user);
                return Ok(userDto);
            }
            return NotFound();
        }
    }
}