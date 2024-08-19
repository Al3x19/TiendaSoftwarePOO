using AutoMapper;
using TiendaSoftware.Constansts;
using TiendaSoftware.DataBase;
using TiendaSoftware.DataBase.Entities;
using TiendaSoftware.DTOS.Common;
using TiendaSoftware.DTOS.Lists;
using TiendaSoftware.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using TiendaSoftware.DTOS.Publishers;

namespace BlogUNAH.API.Services
{
    public class ListsService : IListsService
    {
        private readonly TiendaSoftwareContext _context;
        private readonly ILogger<ListsService> _logger;
        private readonly IMapper _mapper;
        private readonly int PAGE_SIZE;

        public ListsService(TiendaSoftwareContext context, IAuthService authService, ILogger<ListsService> logger, IMapper mapper, IConfiguration configuration)
        {
            this._context = context;
            this._logger = logger;
            this._mapper = mapper;
            PAGE_SIZE = configuration.GetValue<int>("PageSize");
        }

        public async Task<ResponseDto<PaginationDto<List<ListDto>>>> GetListAsync(string searchTerm = "", int page = 1)
        {

            int startIndex = (page - 1) * PAGE_SIZE;

            // Start with the base query
            var listEntityQuery = _context.Lists
                .Include(x => x.Creator)
                .Include(x => x.Softwares)
                .ThenInclude(x => x.Software).AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                listEntityQuery = listEntityQuery
                    .Where(x => (x.Name + " " + x.Creator.Name + " " + x.Description)
                    .ToLower().Contains(searchTerm.ToLower()));
            }
            int totalLists = await listEntityQuery.CountAsync();
            int totalPages = (int)Math.Ceiling((double)totalLists / PAGE_SIZE);

            var listsEntity = await listEntityQuery
                .OrderByDescending(x => x.CreatedDate)
                .Skip(startIndex)
                .Take(PAGE_SIZE)
            .ToListAsync();

            var listDto = _mapper.Map<List<ListDto>>(listsEntity);

            return new ResponseDto<PaginationDto<List<ListDto>>>
            {
                StatusCode = 200,
                Status = true,
                Message = MessagesConstant.RECORDS_FOUND,
                Data = new PaginationDto<List<ListDto>>
                {
                    CurrentPage = page,
                    PageSize = PAGE_SIZE,
                    TotalItems = totalLists,
                    TotalPages = totalPages,
                    Items = listDto,
                    HasPreviousPage = page > 1,
                    HasNextPage = page < totalPages,
                }
            };

        }

        public async Task<ResponseDto<ListDto>> GetByIdAsync(Guid id)
        {
            var listEntity = await _context.Lists.Include(x => x.Creator).Include(x => x.Softwares).ThenInclude(x => x.Software).FirstOrDefaultAsync(x => x.Id == id);

            if (listEntity is null)
            {
                return new ResponseDto<ListDto>
                {
                    StatusCode = 404,
                    Status = false,
                    Message = MessagesConstant.RECORD_NOT_FOUND
                };
            }

            var listDto = _mapper.Map<ListDto>(listEntity);

            return new ResponseDto<ListDto>
            {
                StatusCode = 200,
                Status = true,
                Message = MessagesConstant.RECORD_FOUND,
                Data = listDto
            };
        }

        public async Task<ResponseDto<ListDto>> CreateAsync(ListCreateDto dto)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    var ListEntity = _mapper.Map<ListEntity>(dto);
                    var existingsoft = await _context.Software.Where(t => dto.SoftwaresList.Contains(t.Name)).ToListAsync();

                    if (existingsoft.Count() != dto.SoftwaresList.Count())
                    {
                        return new ResponseDto<ListDto>
                        {
                            StatusCode = 400,
                            Status = false,
                            Message = "Se intento agregar un software no existente"
                        };
                    }



                    _context.Lists.Add(ListEntity);
                    await _context.SaveChangesAsync();






                    var listSoftwaresEntity = existingsoft.Select(t => new ListSoftwareEntity
                    {
                        ListId = ListEntity.Id,
                        SoftwareId = t.Id,
                    }).ToList();

                    _context.SoftwareLists.AddRange(listSoftwaresEntity);
                    await _context.SaveChangesAsync();

                    //throw new Exception("Error"); Aqui no deberia de guardar nada cuand esta comentado
                    await transaction.CommitAsync();


                    return new ResponseDto<ListDto>
                    {
                        StatusCode = 201,
                        Status = true,
                        Message = MessagesConstant.CREATE_SUCCESS,
                    };
                }
                catch (Exception e)
                {
                    await transaction.RollbackAsync();
                    _logger.LogError(e, MessagesConstant.CREATE_ERROR);
                    return new ResponseDto<ListDto>
                    {
                        StatusCode = 500,
                        Status = false,
                        Message = MessagesConstant.CREATE_ERROR,
                    };
                }
            }
        }

        public async Task<ResponseDto<ListDto>> EditAsync(ListEditDto dto, Guid id)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {


                    var ListEntity = await _context.Lists.FindAsync(id);

                    if (ListEntity is null)
                    {
                        return new ResponseDto<ListDto>
                        {
                            StatusCode = 404,
                            Status = false,
                            Message = "registro no encontrado"
                        };
                    }




                    var existingsoft = await _context.Software.Where(t => dto.SoftwaresList.Contains(t.Name)).ToListAsync();

                    if (existingsoft.Count() != dto.SoftwaresList.Count())
                    {
                        return new ResponseDto<ListDto>
                        {
                            StatusCode = 400,
                            Status = false,
                            Message = "Se intento agregar un software no existente"
                        };
                    }

                    _context.Lists.Update(ListEntity);
                    await _context.SaveChangesAsync();



                    var listSoftwaresEntity = existingsoft.Select(t => new ListSoftwareEntity
                    {
                        ListId = ListEntity.Id,
                        SoftwareId = t.Id,
                    }).ToList();

                    _context.SoftwareLists.AddRange(listSoftwaresEntity);
                    await _context.SaveChangesAsync();

                    await transaction.CommitAsync();


                    return new ResponseDto<ListDto>
                    {
                        StatusCode = 201,
                        Status = true,
                        Message = MessagesConstant.CREATE_SUCCESS,
                    };
                }
                catch (Exception e)
                {
                    await transaction.RollbackAsync();
                    _logger.LogError(e, MessagesConstant.CREATE_ERROR);
                    return new ResponseDto<ListDto>
                    {
                        StatusCode = 500,
                        Status = false,
                        Message = MessagesConstant.CREATE_ERROR,
                    };
                }
            }
        }

        public async Task<ResponseDto<ListDto>> DeleteAsync(Guid id)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    var ListEntity = await _context.Lists.FindAsync(id);

                    if (ListEntity is null)
                    {
                        return new ResponseDto<ListDto>
                        {
                            StatusCode = 404,
                            Status = false,
                            Message = $"{MessagesConstant.RECORD_NOT_FOUND}"
                        };
                    }

                    _context.Lists.Remove(ListEntity);
                    await transaction.CommitAsync();

                    return new ResponseDto<ListDto>
                    {
                        StatusCode = 200,
                        Status = true,
                        Message = MessagesConstant.DELETE_SUCCESS
                    };
                }
                catch (Exception e)
                {
                    await transaction.RollbackAsync();
                    _logger.LogError(e, MessagesConstant.DELETE_ERROR);
                    return new ResponseDto<ListDto>
                    {
                        StatusCode = 500,
                        Status = false,
                        Message = MessagesConstant.DELETE_ERROR
                    };
                }
            }
        }
    }
}
