using AutoMapper;
using TiendaSoftware.Constansts;
using TiendaSoftware.DataBase;
using TiendaSoftware.DataBase.Entities;
using TiendaSoftware.DTOS.Common;
using TiendaSoftware.DTOS.Publishers;
using TiendaSoftware.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace TiendaSoftware.Services
{
    public class PublisherService : IPublisherService
    {
        private readonly TiendaSoftwareContext _context;
        private readonly IMapper _mapper;
        private readonly int PAGE_SIZE;

        public PublisherService(TiendaSoftwareContext context, IMapper mapper, IConfiguration configuration)
        {
            this._context = context;
            this._mapper = mapper;
            PAGE_SIZE = configuration.GetValue<int>("PageSize");
        }

        public async Task<ResponseDto<PaginationDto<List<PublisherDto>>>> GetPublishersListAsync(string searchTerm = "", int page = 1)
        {
            int startIndex = (page - 1) * PAGE_SIZE;

            var PublishersEntityQuery = _context.Publishers
                .Where(x => x.Description.ToLower().Contains(searchTerm.ToLower()));

            int totalPublishers = await PublishersEntityQuery.CountAsync();
            int totalPages = (int)Math.Ceiling((double)totalPublishers / PAGE_SIZE);

            var PublishersEntity = await PublishersEntityQuery
                .OrderBy(u => u.Description)
                .Skip(startIndex)
                .Take(PAGE_SIZE)
                .ToListAsync();

            var PublishersDtos = _mapper.Map<List<PublisherDto>>(PublishersEntity);

            return new ResponseDto<PaginationDto<List<PublisherDto>>>
            {
                StatusCode = 200,
                Status = true,
                Message = "Registros Encontrados",
                Data = new PaginationDto<List<PublisherDto>>
                {
                    CurrentPage = page,
                    PageSize = PAGE_SIZE,
                    TotalItems = totalPublishers,
                    TotalPages = totalPages,
                    Items = PublishersDtos,
                    HasPreviousPage = page > 1,
                    HasNextPage = page < totalPages,
                }
            };

        }
        public async Task<ResponseDto<PublisherDto>> GetPublisherByIdAsync(Guid id)
        {
            var publisherEntity = await _context.Publishers.FirstOrDefaultAsync(c => c.Id == id);
            if (publisherEntity == null)
            {
                return new ResponseDto<PublisherDto>
                {
                    StatusCode = 404,
                    Status = false,
                    Message = "No se encontro el registro.",
                };
            }
            var publisherDto = _mapper.Map<PublisherDto>(publisherEntity);

            return new ResponseDto<PublisherDto>
            {
                StatusCode = 200,
                Status = true,
                Message = "Registro encontrado.",
                Data = publisherDto
            };
        }

        public async Task<ResponseDto<PublisherDto>> CreateAsync(PublisherCreateDto dto)
        {


            var publisherEntity = _mapper.Map<PublisherEntity>(dto);

            _context.Publishers.Add(publisherEntity);

            await _context.SaveChangesAsync();

            var publisherDto = _mapper.Map<PublisherDto>(publisherEntity);

            return new ResponseDto<PublisherDto>
            {
                StatusCode = 201,
                Status = true,
                Message = "Registro creado correctamente.",
                Data = publisherDto
            };
        }

        public async Task<ResponseDto<PublisherDto>> EditAsync(PublisherEditDto dto, Guid id)
        {
            var publisherEntity = await _context.Publishers.FirstOrDefaultAsync(c => c.Id == id);
            if (publisherEntity == null)
            {
                return new ResponseDto<PublisherDto>
                {
                    StatusCode = 404,
                    Status = false,
                    Message = "No se encontro el registro.",
                };
            }
            _mapper.Map<PublisherEditDto, PublisherEntity>(dto, publisherEntity);
            _context.Publishers.Update(publisherEntity);
            await _context.SaveChangesAsync();
            var publisherDto = _mapper.Map<PublisherDto>(publisherEntity);

            return new ResponseDto<PublisherDto>
            {
                StatusCode = 200,
                Status = true,
                Message = "Registro modificado correctamente.",
                Data = publisherDto
            };
        }

        public async Task<ResponseDto<PublisherDto>> DeleteAsync(Guid id)
        {
            var publisherEntity = await _context.Publishers
                .FirstOrDefaultAsync(x => x.Id == id);

            if (publisherEntity == null)
            {
                return new ResponseDto<PublisherDto>
                {
                    StatusCode = 404,
                    Status = false,
                    Message = "No se encontro el registro.",
                };
            }

            _context.Publishers.Remove(publisherEntity);
            await _context.SaveChangesAsync();

            return new ResponseDto<PublisherDto>
            {
                StatusCode = 200,
                Status = true,
                Message = "Registro borrado correctamente"
            };
        }

    }
}