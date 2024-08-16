using System;
using System.Linq;
using System.Collections.Generic;
using TiendaSoftware.DTOS.Common;
using TiendaSoftware.DTOS.Reviews;

namespace TiendaSoftware.Services.Interfaces;

public interface IReviewService
{

    Task<ResponseDto<ReviewDto>> GetByIdAsync(Guid id);
    Task<ResponseDto<ReviewDto>> CreateAsync(ReviewCreateDto dto);
    Task<ResponseDto<ReviewDto>> EditAsync(ReviewEditDto dto, Guid id);
    Task<ResponseDto<ReviewDto>> DeleteAsync(Guid id);
    Task<ResponseDto<PaginationDto<List<ReviewDto>>>> GetReviewsListAsync(string searchTerm = "", int page = 1);
}
