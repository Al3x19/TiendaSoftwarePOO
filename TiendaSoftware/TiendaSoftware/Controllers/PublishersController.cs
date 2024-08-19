using TiendaSoftware.DTOS.Common;
using TiendaSoftware.DTOS.Publishers;
using TiendaSoftware.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TiendaSoftware.Controllers
{
    [Route("api/publishers")]
    [ApiController]
    public class PublishersController : ControllerBase
    {
        private readonly IPublisherService _publishersService;

        public PublishersController(IPublisherService publishersService)
        {
            this._publishersService = publishersService;
        }

        [HttpGet]
        public async Task<ActionResult<ResponseDto<PaginationDto<List<PublisherDto>>>>> PaginationList(
            string searchTerm, int page = 1)
        {
            var response = await _publishersService.GetPublishersListAsync(searchTerm, page);

            return StatusCode(response.StatusCode, new
            {
                response.Status,
                response.Message,
                response.Data
            });
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseDto<PublisherDto>>> GetOneById(Guid id)
        {
            var response = await _publishersService.GetPublisherByIdAsync(id);

            return StatusCode(response.StatusCode, new
            {
                response.Status,
                response.Message,
                response.Data
            });
        }

        [HttpPost]
        public async Task<ActionResult<ResponseDto<PublisherDto>>> Create(PublisherCreateDto dto)
        {
            var response = await _publishersService.CreateAsync(dto);

            return StatusCode(response.StatusCode, new
            {
                response.Status,
                response.Message,
            });
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ResponseDto<PublisherDto>>> Edit(PublisherEditDto dto,
            Guid id)
        {
            var response = await _publishersService.EditAsync(dto, id);

            return StatusCode(response.StatusCode, new
            {
                response.Status,
                response.Message,
            });
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ResponseDto<PublisherDto>>> Delete(Guid id)
        {
            var response = await _publishersService.DeleteAsync(id);

            return StatusCode(response.StatusCode, new
            {
                response.Status,
                response.Message,
            });
        }
    }
}