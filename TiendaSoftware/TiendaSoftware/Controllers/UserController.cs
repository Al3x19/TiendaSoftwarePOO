using TiendaSoftware.DataBase.Entities;
using TiendaSoftware.DTOS.Users;
using TiendaSoftware.DTOS.Common;
using TiendaSoftware.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace TiendaSoftware.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _usersService;

        public List<UserEntity> _users { get; set; }

        public UserController(IUserService usersService)
        {
            this._usersService = usersService;
        }

        [HttpGet]
        public async Task<ActionResult<ResponseDto<List<UserDto>>>> GetAll()
        {
            var response = await _usersService.GetUsersListAsync();

            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseDto<UserDto>>> Get(Guid id)
        {
            var response = await _usersService.GetUserByIdAsync(id);

            return StatusCode(response.StatusCode, response);
        }
        [HttpPost]
        public async Task<ActionResult<ResponseDto<UserDto>>> Create(UserCreateDto dto)
        {
            var response = await _usersService.CreateAsync(dto);

            return StatusCode(response.StatusCode, response);

        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ResponseDto<List<UserDto>>>> Edit(UserEditDto dto, Guid id)
        {
            var response = await _usersService.EditAsync(dto, id);

            return StatusCode(response.StatusCode, response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ResponseDto<UserDto>>> Delete(Guid id)
        {
            var category = await _usersService.GetUserByIdAsync(id);
            var response = await _usersService.DeleteAsync(id);

            return StatusCode(response.StatusCode, response);
        }
    }
}
