using TiendaSoftware.DTOS.Publishers;
using TiendaSoftware.DTOS.Common;

namespace TiendaSoftware.Services.Interfaces
{
    public interface IPublisherService
    {
        Task<ResponseDto<PublisherDto>> GetPublisherByIdAsync(Guid id);
        Task<ResponseDto<PublisherDto>> CreateAsync(PublisherCreateDto dto);
        Task<ResponseDto<PublisherDto>> EditAsync(PublisherEditDto dto, Guid id);
        Task<ResponseDto<PublisherDto>> DeleteAsync(Guid id);
        Task<ResponseDto<PaginationDto<List<PublisherDto>>>> GetPublishersListAsync(string searchTerm = "", int page = 1);
    }
}
