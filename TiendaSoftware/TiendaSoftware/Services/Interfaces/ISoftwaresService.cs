using TiendaSoftware.DTOS.Common;
using TiendaSoftware.DTOS.Softwares;

namespace TiendaSoftware.Services.Interfaces
{
    public interface ISoftwaresService
    {
        Task<ResponseDto<SoftwareDto>> CreateAsync(SoftwareCreateDto dto);
        Task<ResponseDto<SoftwareDto>> DeleteAsync(Guid id);
        Task<ResponseDto<SoftwareDto>> EditAsync(SoftwareEditDto dto, Guid id);
        Task<ResponseDto<SoftwareDto>> GetByIdAsync(Guid id);
        Task<ResponseDto<PaginationDto<List<SoftwareDto>>>> GetListAsync(string searchTerm = "", int page = 1);
    }
}
