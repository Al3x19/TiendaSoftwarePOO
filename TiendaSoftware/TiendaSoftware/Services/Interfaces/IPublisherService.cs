﻿using TiendaSoftware.DTOS.Publishers;
using TiendaSoftware.DTOS.Common;

namespace TiendaSoftware.Services.Interfaces
{
    public interface IPublisherService
    {
        Task<ResponseDto<List<PublisherDto>>> GetPublishersListAsync();
        Task<ResponseDto<PublisherDto>> GetPublisherByIdAsync(Guid id);
        Task<ResponseDto<PublisherDto>> CreateAsync(PublisherCreateDto dto);
        Task<ResponseDto<PublisherDto>> EditAsync(PublisherEditDto dto, Guid id);
        Task<ResponseDto<PublisherDto>> DeleteAsync(Guid id);
    }
}
