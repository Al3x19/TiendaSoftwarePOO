using AutoMapper;
using TiendaSoftware.Constansts;
using TiendaSoftware.DataBase;
using TiendaSoftware.DataBase.Entities;
using TiendaSoftware.DTOS.Common;
using TiendaSoftware.DTOS.Lists;
using TiendaSoftware.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

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

            var ListEntityQuery = _context.Lists.Include(x => x.Creator).Include(x => x.Softwares).ThenInclude(x => x.Software).Where(x => (x.Name + " " + x.Creator.Name + " " + x.Description).ToLower().Contains(searchTerm.ToLower()));

            if (!string.IsNullOrEmpty(searchTerm))
            {
                ListEntityQuery = ListEntityQuery.Where(x => (x.Name + " " + x.Creator.Name + " " + x.Description).ToLower().Contains(searchTerm.ToLower()));
            }

            int totalList = await ListEntityQuery.CountAsync();
            int totalPages = (int)Math.Ceiling((double)totalList / PAGE_SIZE);

            var listEntity = await ListEntityQuery.OrderByDescending(x => x.CreatedDate).Skip(startIndex).Take(PAGE_SIZE).ToListAsync();

            var listDto = _mapper.Map<List<ListDto>>(listEntity);

            return new ResponseDto<PaginationDto<List<ListDto>>>
            {
                StatusCode = 200,
                Status = true,
                Message = "Registro encontrado",
                Data = new PaginationDto<List<ListDto>>
                {
                    CurrentPage = page,
                    PageSize = PAGE_SIZE,
                    TotalItems = totalList,
                    TotalPages = totalPages,
                    Items = listDto,
                    HasPreviousPage = page > 1,
                    HasNextPage = page < totalPages,
                }
            };
        }

        public async Task<ResponseDto<ListDto>> GetByIdAsync(Guid id)
        {
            var ListEntity = await _context.Lists.Include(x => x.Creator).Include(x => x.Softwares).ThenInclude(x => x.Software).FirstOrDefaultAsync(x => x.Id == id);

            if (ListEntity is null)
            {
                return new ResponseDto<ListDto>
                {
                    StatusCode = 404,
                    Status = false,
                    Message = MessagesConstant.RECORD_NOT_FOUND
                };
            }

            var listDto = _mapper.Map<ListDto>(ListEntity);

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


                    _context.Lists.Add(ListEntity);
                    await _context.SaveChangesAsync();


                    var existingsoft = await _context.Software.Where(t => dto.Softwares.Contains(t.Name)).ToListAsync();

                    existingsoft.ForEach(x =>
                    {
                        var i = 0;
                        _context.Software.ForEachAsync(t =>
                        {

                            if (t.Name == x.Name)
                            {
                                i++;
                            }


                        });

                        if (i > 0)
                        {
                            throw new Exception("Error, se intento agregar un producto no existente");
                        }
                    });


                    // Combinar tags existentes y nuevoso.Softwares.Contains(t.Name));


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
                            Message = $"{MessagesConstant.RECORD_NOT_FOUND} {id}"
                        };
                    }

                    _mapper.Map(dto, ListEntity);

                    _context.Lists.Update(ListEntity);
                    await _context.SaveChangesAsync();

                    // Eliminar tags anteriores 
                    var existingListSoftwares = await _context.SoftwareLists.Where(t => t.ListId == id).ToListAsync();

                    _context.RemoveRange(existingListSoftwares);
                    await _context.SaveChangesAsync();

                    var existingsoft = await _context.Software.Where(t => dto.Softwares.Contains(t.Name)).ToListAsync();

                    existingsoft.ForEach(x =>
                    {
                        var i = 0;
                        _context.Software.ForEachAsync(t =>
                        {

                            if (t.Name == x.Name)
                            {
                                i++;
                            }


                        });

                        if (i > 0)
                        {
                            throw new Exception("Error, se intento agregar un producto no existente");
                        }
                    });


                    // Combinar tags existentes y nuevoso.Softwares.Contains(t.Name));


                    var listSoftwaresEntity = existingsoft.Select(t => new ListSoftwareEntity
                    {
                        ListId = ListEntity.Id,
                        SoftwareId = t.Id,
                    }).ToList();


                    _context.SoftwareLists.AddRange(listSoftwaresEntity);
                    await _context.SaveChangesAsync();

                    //throw new Exception("Error para validar el rollback");

                    await transaction.CommitAsync();

                    return new ResponseDto<ListDto>
                    {
                        StatusCode = 200,
                        Status = true,
                        Message = MessagesConstant.UPDATE_SUCCESS
                    };

                }
                catch (Exception e)
                {
                    await transaction.RollbackAsync();
                    _logger.LogError(e, MessagesConstant.UPDATE_ERROR);
                    return new ResponseDto<ListDto>
                    {
                        StatusCode = 500,
                        Status = false,
                        Message = MessagesConstant.UPDATE_ERROR
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
