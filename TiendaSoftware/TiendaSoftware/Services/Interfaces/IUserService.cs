using TiendaSoftware.DTOS.Users;
using TiendaSoftware.DTOS.Common;

namespace TiendaSoftware.Services.Interfaces
{
    public interface IUserService
    {
        Task<ResponseDto<UserDto>> GetUserByIdAsync(Guid id);
        Task<ResponseDto<UserDto>> CreateAsync(UserCreateDto dto);
        Task<ResponseDto<UserDto>> EditAsync(UserEditDto dto, Guid id);
        Task<ResponseDto<UserDto>> DeleteAsync(Guid id);
        Task<ResponseDto<PaginationDto<List<UserDto>>>> GetUsersListAsync(string searchTerm = "", int page = 1);
    }
}
