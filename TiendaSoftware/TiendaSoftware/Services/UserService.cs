using AutoMapper;
using TiendaSoftware.DataBase;
using TiendaSoftware.DataBase.Entities;
using TiendaSoftware.DTOS.Users;
using TiendaSoftware.DTOS.Common;
using TiendaSoftware.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace TiendaSoftware.Services
{
    public class UserService : IUserService
    {
        private readonly TiendaSoftwareContext _context;
        private readonly IMapper _mapper;
        private readonly int PAGE_SIZE;

        public UserService(TiendaSoftwareContext context, IMapper mapper, IConfiguration configuration)
        {
            this._context = context;
            this._mapper = mapper;
            PAGE_SIZE = configuration.GetValue<int>("PageSize");
        }

        public async Task<ResponseDto<PaginationDto<List<UserDto>>>> GetUsersListAsync(string searchTerm = "", int page = 1)
        {
            int startIndex = (page - 1) * PAGE_SIZE;

            var UsersEntityQuery = _context.Users
                .Where(x => x.Name.ToLower().Contains(searchTerm.ToLower()));

            int totalUsers = await UsersEntityQuery.CountAsync();
            int totalPages = (int)Math.Ceiling((double)totalUsers / PAGE_SIZE);

            var UsersEntity = await UsersEntityQuery
                .OrderBy(u => u.Name)
                .Skip(startIndex)
                .Take(PAGE_SIZE)
                .ToListAsync();

            var UsersDtos = _mapper.Map<List<UserDto>>(UsersEntity);

            return new ResponseDto<PaginationDto<List<UserDto>>>
            {
                StatusCode = 200,
                Status = true,
                Message = "Registros Encontrados",
                Data = new PaginationDto<List<UserDto>>
                {
                    CurrentPage = page,
                    PageSize = PAGE_SIZE,
                    TotalItems = totalUsers,
                    TotalPages = totalPages,
                    Items = UsersDtos,
                    HasPreviousPage = page > 1,
                    HasNextPage = page < totalPages,
                }
            };

        }
        public async Task<ResponseDto<UserDto>> GetUserByIdAsync(Guid id)
        {
            var UserEntity = await _context.Users.FirstOrDefaultAsync(c => c.Id == id);
            if (UserEntity == null)
            {
                return new ResponseDto<UserDto>
                {
                    StatusCode = 404,
                    Status = false,
                    Message = "No se encontro el registro.",
                };
            }
            var UserDto = _mapper.Map<UserDto>(UserEntity);

            return new ResponseDto<UserDto>
            {
                StatusCode = 200,
                Status = true,
                Message = "Registro encontrado.",
                Data = UserDto
            };
        }

        public async Task<ResponseDto<UserDto>> CreateAsync(UserCreateDto dto)
        {
            var UserEntity = _mapper.Map<UserEntity>(dto);

            _context.Users.Add(UserEntity);

            await _context.SaveChangesAsync();

            var UserDto = _mapper.Map<UserDto>(UserEntity);

            return new ResponseDto<UserDto>
            {
                StatusCode = 201,
                Status = true,
                Message = "Registro creado correctamente.",
                Data = UserDto
            };
        }

        public async Task<ResponseDto<UserDto>> EditAsync(UserEditDto dto, Guid id)
        {
            var UserEntity = await _context.Users.FirstOrDefaultAsync(c => c.Id == id);
            if (UserEntity == null)
            {
                return new ResponseDto<UserDto>
                {
                    StatusCode = 404,
                    Status = false,
                    Message = "No se encontro el registro.",
                };
            }
            _mapper.Map<UserEditDto, UserEntity>(dto, UserEntity);
            _context.Users.Update(UserEntity);
            await _context.SaveChangesAsync();
            var UserDto = _mapper.Map<UserDto>(UserEntity);

            return new ResponseDto<UserDto>
            {
                StatusCode = 200,
                Status = true,
                Message = "Registro modificado correctamente.",
                Data = UserDto
            };
        }

        public async Task<ResponseDto<UserDto>> DeleteAsync(Guid id)
        {
            var UserEntity = await _context.Users
                .FirstOrDefaultAsync(x => x.Id == id);

            if (UserEntity == null)
            {
                return new ResponseDto<UserDto>
                {
                    StatusCode = 404,
                    Status = false,
                    Message = "No se encontro el registro.",
                };
            }

            _context.Users.Remove(UserEntity);
            await _context.SaveChangesAsync();

            return new ResponseDto<UserDto>
            {
                StatusCode = 200,
                Status = true,
                Message = "Registro borrado correctamente"
            };
        }

    }
}