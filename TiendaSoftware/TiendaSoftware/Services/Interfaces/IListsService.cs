using System;
using System.Linq;
using System.Collections.Generic;
using TiendaSoftware.DTOS.Common;
using TiendaSoftware.DTOS.Lists;

namespace TiendaSoftware.Services.Interfaces;

public interface IListsService
{

    Task<ResponseDto<ListDto>> GetByIdAsync(Guid id);
    Task<ResponseDto<ListDto>> CreateAsync(ListCreateDto dto);
    Task<ResponseDto<ListDto>> EditAsync(ListEditDto dto, Guid id);
    Task<ResponseDto<ListDto>> DeleteAsync(Guid id);
    Task<ResponseDto<PaginationDto<List<ListDto>>>> GetListAsync(string searchTerm = "", int page = 1);
}
