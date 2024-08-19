using TiendaSoftware.DTOS.Common;
using TiendaSoftware.DTOS.Users;
using TiendaSoftware.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TiendaSoftware.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _usersService;

        public UsersController(IUserService usersService)
        {
            this._usersService = usersService;
        }

        [HttpGet]
        public async Task<ActionResult<ResponseDto<PaginationDto<List<UserDto>>>>> PaginationList(
            string searchTerm, int page = 1)
        {
            var response = await _usersService.GetUsersListAsync(searchTerm, page);

            return StatusCode(response.StatusCode, new
            {
                response.Status,
                response.Message,
                response.Data
            });
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseDto<UserDto>>> GetOneById(Guid id)
        {
            var response = await _usersService.GetUserByIdAsync(id);

            return StatusCode(response.StatusCode, new
            {
                response.Status,
                response.Message,
                response.Data
            });
        }

        [HttpPost]
        public async Task<ActionResult<ResponseDto<UserDto>>> Create(UserCreateDto dto)
        {
            var response = await _usersService.CreateAsync(dto);

            return StatusCode(response.StatusCode, new
            {
                response.Status,
                response.Message,
            });
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ResponseDto<UserDto>>> Edit(UserEditDto dto,
            Guid id)
        {
            var response = await _usersService.EditAsync(dto, id);

            return StatusCode(response.StatusCode, new
            {
                response.Status,
                response.Message,
            });
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ResponseDto<UserDto>>> Delete(Guid id)
        {
            var response = await _usersService.DeleteAsync(id);

            return StatusCode(response.StatusCode, new
            {
                response.Status,
                response.Message,
            });
        }
    }
}