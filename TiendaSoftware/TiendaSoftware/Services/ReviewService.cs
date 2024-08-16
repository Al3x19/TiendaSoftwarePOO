using AutoMapper;
using TiendaSoftware.DataBase;
using TiendaSoftware.DataBase.Entities;
using TiendaSoftware.DTOS.Reviews;
using TiendaSoftware.DTOS.Common;
using TiendaSoftware.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using TiendaSoftware.DTOS.Users;

namespace TiendaSoftware.Services
{
    public class ReviewService : IReviewService
    {
        private readonly TiendaSoftwareContext _context;
        private readonly IMapper _mapper;
        private readonly int PAGE_SIZE;

        public ReviewService(TiendaSoftwareContext context, IMapper mapper, IConfiguration configuration)
        {
            this._context = context;
            this._mapper = mapper;
            PAGE_SIZE = configuration.GetValue<int>("PageSize");
        }

        public async Task<ResponseDto<PaginationDto<List<ReviewDto>>>> GetReviewsListAsync(string searchTerm = "", int page = 1)
        {
            int startIndex = (page - 1) * PAGE_SIZE;

            var ReviewsEntityQuery = _context.Reviews
                .Where(x => x.Content.ToLower().Contains(searchTerm.ToLower()));

            int totalReviews = await ReviewsEntityQuery.CountAsync();
            int totalPages = (int)Math.Ceiling((double)totalReviews / PAGE_SIZE);

            var ReviewsEntity = await ReviewsEntityQuery
                .OrderBy(u => u.Content)
                .Skip(startIndex)
                .Take(PAGE_SIZE)
                .ToListAsync();

            var ReviewsDtos = _mapper.Map<List<ReviewDto>>(ReviewsEntity);

            return new ResponseDto<PaginationDto<List<ReviewDto>>>
            {
                StatusCode = 200,
                Status = true,
                Message = "Registros Encontrados",
                Data = new PaginationDto<List<ReviewDto>>
                {
                    CurrentPage = page,
                    PageSize = PAGE_SIZE,
                    TotalItems = totalReviews,
                    TotalPages = totalPages,
                    Items = ReviewsDtos,
                    HasPreviousPage = page > 1,
                    HasNextPage = page < totalPages,
                }
            };

        }
        public async Task<ResponseDto<ReviewDto>> GetByIdAsync(Guid id)
        {
            var ReviewEntity = await _context.Reviews.FirstOrDefaultAsync(c => c.Id == id);
            if (ReviewEntity == null)
            {
                return new ResponseDto<ReviewDto>
                {
                    StatusCode = 404,
                    Status = false,
                    Message = "No se encontro el registro.",
                };
            }
            var ReviewDto = _mapper.Map<ReviewDto>(ReviewEntity);

            return new ResponseDto<ReviewDto>
            {
                StatusCode = 200,
                Status = true,
                Message = "Registro encontrado.",
                Data = ReviewDto
            };
        }

        public async Task<ResponseDto<ReviewDto>> CreateAsync(ReviewCreateDto dto)
        {
            var ReviewEntity = _mapper.Map<ReviewEntity>(dto);

            _context.Reviews.Add(ReviewEntity);

            await _context.SaveChangesAsync();

            var ReviewDto = _mapper.Map<ReviewDto>(ReviewEntity);

            return new ResponseDto<ReviewDto>
            {
                StatusCode = 201,
                Status = true,
                Message = "Registro creado correctamente.",
                Data = ReviewDto
            };
        }

        public async Task<ResponseDto<ReviewDto>> EditAsync(ReviewEditDto dto, Guid id)
        {
            var ReviewEntity = await _context.Reviews.FirstOrDefaultAsync(c => c.Id == id);
            if (ReviewEntity == null)
            {
                return new ResponseDto<ReviewDto>
                {
                    StatusCode = 404,
                    Status = false,
                    Message = "No se encontro el registro.",
                };
            }
            _mapper.Map<ReviewEditDto, ReviewEntity>(dto, ReviewEntity);
            _context.Reviews.Update(ReviewEntity);
            await _context.SaveChangesAsync();
            var ReviewDto = _mapper.Map<ReviewDto>(ReviewEntity);

            return new ResponseDto<ReviewDto>
            {
                StatusCode = 200,
                Status = true,
                Message = "Registro modificado correctamente.",
                Data = ReviewDto
            };
        }

        public async Task<ResponseDto<ReviewDto>> DeleteAsync(Guid id)
        {
            var ReviewEntity = await _context.Reviews
                .FirstOrDefaultAsync(x => x.Id == id);

            if (ReviewEntity == null)
            {
                return new ResponseDto<ReviewDto>
                {
                    StatusCode = 404,
                    Status = false,
                    Message = "No se encontro el registro.",
                };
            }

            _context.Reviews.Remove(ReviewEntity);
            await _context.SaveChangesAsync();

            return new ResponseDto<ReviewDto>
            {
                StatusCode = 200,
                Status = true,
                Message = "Registro borrado correctamente"
            };
        }

    }
}