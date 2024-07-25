using AutoMapper;
using TiendaSoftware.DataBase;
using TiendaSoftware.DataBase.Entities;
using TiendaSoftware.DTOS.Publishers;
using TiendaSoftware.DTOS.Common;
using TiendaSoftware.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace TiendaSoftware.Services
{
    public class PublisherService : IPublisherService
    {
        private readonly TiendaSoftwareContext _context;
        private readonly IMapper _mapper;

        public PublisherService(TiendaSoftwareContext context, IMapper mapper)
        {
            this._context = context;
            this._mapper = mapper;
        }

        public async Task<ResponseDto<List<PublisherDto>>> GetPublishersListAsync()
        {
            var PublishersEntity = await _context.Publishers.ToListAsync();

            var publishersDtos = _mapper.Map<List<PublisherDto>>(PublishersEntity);

            return new ResponseDto<List<PublisherDto>> 
            {
                StatusCode = 200,
                Status = true,
                Message = "Lista de registros obtenida correctamente.",
                Data = publishersDtos
            };

        }

        public async Task<ResponseDto<PublisherDto>> GetPublisherByIdAsync(Guid id)
        {
            var publisherEntity  = await _context.Publishers.FirstOrDefaultAsync(c => c.Id == id);
            if (publisherEntity == null) 
            {
                return new ResponseDto<PublisherDto>
                {
                    StatusCode = 404,
                    Status = false,
                    Message = "No se encontro el registro.",
                };
            }
            var publisherDto =  _mapper.Map<PublisherDto>(publisherEntity);

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