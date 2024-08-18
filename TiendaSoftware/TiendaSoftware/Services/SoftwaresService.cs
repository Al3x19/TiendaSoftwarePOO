using AutoMapper;
using TiendaSoftware.Constansts;
using TiendaSoftware.DataBase;
using TiendaSoftware.DataBase.Entities;
using TiendaSoftware.DTOS.Common;
using TiendaSoftware.DTOS.Softwares;
using TiendaSoftware.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using TiendaSoftware.DTOS.Reviews;

namespace BlogUNAH.API.Services
{
    public class SoftwaresService : ISoftwaresService
    {
        private readonly TiendaSoftwareContext _context;
        private readonly ILogger<SoftwaresService> _logger;
        private readonly IMapper _mapper;
        private readonly int PAGE_SIZE;

        public SoftwaresService(TiendaSoftwareContext context, IAuthService authService, ILogger<SoftwaresService> logger, IMapper mapper, IConfiguration configuration)
        {
            this._context = context;
            this._logger = logger;
            this._mapper = mapper;
            PAGE_SIZE = configuration.GetValue<int>("PageSize");
        }

        public async Task<ResponseDto<PaginationDto<List<SoftwareDto>>>> GetListAsync(string searchTerm = "", int page = 1)
        {
            int startIndex = (page - 1) * PAGE_SIZE;

            var softwareEntityQuery = _context.Software.Include(x => x.Desarrollador).Include(x => x.Tags).ThenInclude(x => x.tags).Where(x => (x.Name + " " + x.Desarrollador.Name + " " + x.Description).ToLower().Contains(searchTerm.ToLower()));

            if (!string.IsNullOrEmpty(searchTerm))
            {
                softwareEntityQuery = softwareEntityQuery.Where(x => (x.Name + " " + x.Desarrollador.Name + " " + x.Description).ToLower().Contains(searchTerm.ToLower()));
            }

            int totalSoftware = await softwareEntityQuery.CountAsync();
            int totalPages = (int)Math.Ceiling((double)totalSoftware / PAGE_SIZE);

            var softwareEntity = await softwareEntityQuery.OrderByDescending(x => x.CreatedDate).Skip(startIndex).Take(PAGE_SIZE).ToListAsync();

            var softwareDto = _mapper.Map<List<SoftwareDto>>(softwareEntity);

            return new ResponseDto<PaginationDto<List<SoftwareDto>>>
            {
                StatusCode = 200,
                Status = true,
                Message = MessagesConstant.RECORDS_FOUND,
                Data = new PaginationDto<List<SoftwareDto>>
                {
                    CurrentPage = page,
                    PageSize = PAGE_SIZE,
                    TotalItems = totalSoftware,
                    TotalPages = totalPages,
                    Items = softwareDto,
                    HasPreviousPage = page > 1,
                    HasNextPage = page < totalPages,
                }
            };
        }

        public async Task<ResponseDto<SoftwareDto>> GetByIdAsync(Guid id)
        {
            var softwareEntity = await _context.Software.Include(x => x.Desarrollador).Include(x => x.Tags).ThenInclude(x => x.tags).FirstOrDefaultAsync(x => x.Id == id);

            if (softwareEntity is null)
            {
                return new ResponseDto<SoftwareDto>
                {
                    StatusCode = 404,
                    Status = false,
                    Message = $"{MessagesConstant.RECORD_NOT_FOUND}"
                };
            }

            var softwareDto = _mapper.Map<SoftwareDto>(softwareEntity);

            return new ResponseDto<SoftwareDto>
            {
                StatusCode = 200,
                Status = true,
                Message = MessagesConstant.RECORD_FOUND,
                Data = softwareDto
            };
        }

        public async Task<ResponseDto<SoftwareDto>> CreateAsync(SoftwareCreateDto dto)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    var softwareEntity = _mapper.Map<SoftwareEntity>(dto);


                    var userrev = _context.Software.Where(x => x.Name == softwareEntity.Name);


                    if (!(userrev is null))
                    {

                        return new ResponseDto<SoftwareDto>
                        {
                            StatusCode = 404,
                            Status = false,
                            Message = "El nombre ya fue tomado.",
                        };

                    }



                    _context.Software.Add(softwareEntity);
                    await _context.SaveChangesAsync();

                    //Buscar tags del dto en la tabla de tags 
                    var existingTags = await _context.Tags.Where(t => dto.TagList.Contains(t.Name)).ToListAsync();

                    // Identificar tags que no existen
                    var newTagNames = dto.TagList.Except(existingTags.Select(t => t.Name)).ToList();

                    // Crear los nuevos tags
                    var newTags = newTagNames.Select(name => new TagEntity
                    {
                        Name = name,
                    }).ToList();

                    _context.Tags.AddRange(newTags);
                    await _context.SaveChangesAsync();

                    // Combinar tags existentes y nuevos
                    var allTags = existingTags.Concat(newTags).ToList();

                    var softwareTagsEntity = allTags.Select(t => new SoftwareTagsEntity
                    {
                        SoftwareId = softwareEntity.Id,
                        TagId = t.Id,
                    }).ToList();

                    _context.SoftwareTags.AddRange(softwareTagsEntity);
                    await _context.SaveChangesAsync();

                    //throw new Exception("Error"); Aqui no deberia de guardar nada cuand esta comentado


                    await transaction.CommitAsync();


                    return new ResponseDto<SoftwareDto>
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
                    return new ResponseDto<SoftwareDto>
                    {
                        StatusCode = 500,
                        Status = false,
                        Message = MessagesConstant.CREATE_ERROR,
                    };
                }
            }
        }

        public async Task<ResponseDto<SoftwareDto>> EditAsync(SoftwareEditDto dto, Guid id)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    var softwareEntity = await _context.Software.FindAsync(id);

                    if (softwareEntity is null)
                    {
                        return new ResponseDto<SoftwareDto>
                        {
                            StatusCode = 404,
                            Status = false,
                            Message = $"{MessagesConstant.RECORD_NOT_FOUND} {id}"
                        };
                    }

                    _mapper.Map(dto, softwareEntity);

                    _context.Software.Update(softwareEntity);
                    await _context.SaveChangesAsync();

                    // Eliminar tags anteriores 
                    var existingSoftwareTags = await _context.SoftwareTags.Where(t => t.SoftwareId == id).ToListAsync();

                    _context.RemoveRange(existingSoftwareTags);
                    await _context.SaveChangesAsync();

                    // Buscar tags de dto en la tabla de tags 
                    var existingTags = await _context.Tags.Where(t => dto.TagList.Contains(t.Name)).ToListAsync();

                    // Identficar tags que no existen
                    var newTagsNames = dto.TagList.Except(existingTags.Select(t => t.Name)).ToList();

                    // Crear nuevos tags 
                    var newTags = newTagsNames.Select(name => new TagEntity
                    {
                        Name = name,
                    }).ToList();

                    _context.Tags.AddRange(newTags);

                    await _context.SaveChangesAsync();

                    // Combinar tags existentes y nuevas

                    var allTags = existingTags.Concat(newTags).ToList();

                    // Agregar tags a la tabla Software_tags
                    var softwareTagsNew = allTags.Select(t => new SoftwareTagsEntity
                    {
                        SoftwareId = softwareEntity.Id,
                        TagId = t.Id,
                    }).ToList();

                    _context.SoftwareTags.AddRange(softwareTagsNew);
                    await _context.SaveChangesAsync();

                    //throw new Exception("Error para validar el rollback");

                    await transaction.CommitAsync();

                    return new ResponseDto<SoftwareDto>
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
                    return new ResponseDto<SoftwareDto>
                    {
                        StatusCode = 500,
                        Status = false,
                        Message = MessagesConstant.UPDATE_ERROR
                    };
                }
            }
        }

        public async Task<ResponseDto<SoftwareDto>> DeleteAsync(Guid id)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    var softwareEntity = await _context.Software.FindAsync(id);

                    if (softwareEntity is null)
                    {
                        return new ResponseDto<SoftwareDto>
                        {
                            StatusCode = 404,
                            Status = false,
                            Message = $"{MessagesConstant.RECORD_NOT_FOUND}"
                        };
                    }

                    _context.Software.Remove(softwareEntity);
                    await transaction.CommitAsync();

                    return new ResponseDto<SoftwareDto>
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
                    return new ResponseDto<SoftwareDto>
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
