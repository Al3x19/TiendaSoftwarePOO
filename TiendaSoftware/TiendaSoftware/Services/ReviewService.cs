using AutoMapper;
using TiendaSoftware.DataBase;
using TiendaSoftware.DataBase.Entities;
using TiendaSoftware.DTOS.Reviews;
using TiendaSoftware.DTOS.Common;
using TiendaSoftware.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using TiendaSoftware.DTOS.Users;
using TiendaSoftware.Constansts;
using TiendaSoftware.DTOS.Reviews;

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

            // Start with the base query
            var reviewEntityQuery = _context.Reviews
                .Include(x => x.Creator).AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                reviewEntityQuery = reviewEntityQuery
                    .Where(x => (x.Creator + " " + x.Creator.Name + " " + x.Content)
                    .ToLower().Contains(searchTerm.ToLower()));
            }
            int totalReviews = await reviewEntityQuery.CountAsync();
            int totalPages = (int)Math.Ceiling((double)totalReviews / PAGE_SIZE);

            var reviewsEntity = await reviewEntityQuery
                .OrderByDescending(x => x.CreatedDate)
                .Skip(startIndex)
                .Take(PAGE_SIZE)
            .ToListAsync();

            var reviewDto = _mapper.Map<List<ReviewDto>>(reviewsEntity);

            return new ResponseDto<PaginationDto<List<ReviewDto>>>
            {
                StatusCode = 200,
                Status = true,
                Message = MessagesConstant.RECORDS_FOUND,
                Data = new PaginationDto<List<ReviewDto>>
                {
                    CurrentPage = page,
                    PageSize = PAGE_SIZE,
                    TotalItems = totalReviews,
                    TotalPages = totalPages,
                    Items = reviewDto,
                    HasPreviousPage = page > 1,
                    HasNextPage = page < totalPages,
                }
            };

        }
        public async Task<ResponseDto<ReviewDto>> GetByIdAsync(Guid id)
        {
            var reviewEntity = await _context.Reviews.FirstOrDefaultAsync(c => c.Id == id);
            if (reviewEntity == null)
            {
                return new ResponseDto<ReviewDto>
                {
                    StatusCode = 404,
                    Status = false,
                    Message = "No se encontro el registro.",
                };
            }
            var reviewDto = _mapper.Map<ReviewDto>(reviewEntity);

            return new ResponseDto<ReviewDto>
            {
                StatusCode = 200,
                Status = true,
                Message = "Registro encontrado.",
                Data = reviewDto
            };
        }

        public async Task<ResponseDto<ReviewDto>> CreateAsync(ReviewCreateDto dto)
        {

            var reviewEntity = _mapper.Map<ReviewEntity>(dto);
            //____________________________________________________________
            var userrev = await _context.Reviews.AnyAsync(x => x.Creator == reviewEntity.Creator && x.Software == reviewEntity.Software);


            if (userrev)
            {

                return new ResponseDto<ReviewDto>
                {
                    StatusCode = 404,
                    Status = false,
                    Message = "No se puede ingresar mas de un review.",
                };

            }
            //____________________________________________________________


            _context.Reviews.Add(reviewEntity);
            await _context.SaveChangesAsync();

            return new ResponseDto<ReviewDto>
            {
                StatusCode = 201,
                Status = true,
                Message = MessagesConstant.CREATE_SUCCESS,
            };
        }

        public async Task<ResponseDto<ReviewDto>> EditAsync(ReviewEditDto dto, Guid id)
        {
            var reviewEntity = await _context.Reviews.FirstOrDefaultAsync(c => c.Id == id);
            if (reviewEntity == null)
            {
                return new ResponseDto<ReviewDto>
                {
                    StatusCode = 404,
                    Status = false,
                    Message = "No se encontro el registro.",
                };
            }
            _mapper.Map<ReviewEditDto, ReviewEntity>(dto, reviewEntity);

            var userrev = _context.Reviews.AnyAsync(x => x.Creator == reviewEntity.Creator && x.Software == reviewEntity.Software);


            if ((bool)userrev.AsyncState)
            {

                return new ResponseDto<ReviewDto>
                {
                    StatusCode = 404,
                    Status = false,
                    Message = "No se puede ingresar mas de un review.",
                };

            }
            _context.Reviews.Update(reviewEntity);
            await _context.SaveChangesAsync();
            var reviewDto = _mapper.Map<ReviewDto>(reviewEntity);

            return new ResponseDto<ReviewDto>
            {
                StatusCode = 200,
                Status = true,
                Message = "Registro modificado correctamente.",
                Data = reviewDto
            };
        }

        public async Task<ResponseDto<ReviewDto>> DeleteAsync(Guid id)
        {
            var reviewEntity = await _context.Reviews
                .FirstOrDefaultAsync(x => x.Id == id);

            if (reviewEntity == null)
            {
                return new ResponseDto<ReviewDto>
                {
                    StatusCode = 404,
                    Status = false,
                    Message = "No se encontro el registro.",
                };
            }

            _context.Reviews.Remove(reviewEntity);
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