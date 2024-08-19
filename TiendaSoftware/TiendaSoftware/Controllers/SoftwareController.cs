using TiendaSoftware.DTOS.Common;
using TiendaSoftware.DTOS.Softwares;
using TiendaSoftware.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TiendaSoftware.Controllers
{
    [Route("api/softwares")]
    [ApiController]
    public class SoftwaresController : ControllerBase
    {
        private readonly ISoftwaresService _softwaresService;

        public SoftwaresController(ISoftwaresService softwaresService)
        {
            this._softwaresService = softwaresService;
        }

        [HttpGet]
        public async Task<ActionResult<ResponseDto<PaginationDto<List<SoftwareDto>>>>> PaginationList(
            string searchTerm, int page = 1)
        {
            var response = await _softwaresService.GetListAsync(searchTerm, page);

            return StatusCode(response.StatusCode, new
            {
                response.Status,
                response.Message,
                response.Data
            });
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseDto<SoftwareDto>>> GetOneById(Guid id)
        {
            var response = await _softwaresService.GetByIdAsync(id);

            return StatusCode(response.StatusCode, new
            {
                response.Status,
                response.Message,
                response.Data
            });
        }

        [HttpPost]
        public async Task<ActionResult<ResponseDto<SoftwareDto>>> Create(SoftwareCreateDto dto)
        {
            var response = await _softwaresService.CreateAsync(dto);

            return StatusCode(response.StatusCode, new
            {
                response.Status,
                response.Message,
            });
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ResponseDto<SoftwareDto>>> Edit(SoftwareEditDto dto,
            Guid id)
        {
            var response = await _softwaresService.EditAsync(dto, id);

            return StatusCode(response.StatusCode, new
            {
                response.Status,
                response.Message,
            });
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ResponseDto<SoftwareDto>>> Delete(Guid id)
        {
            var response = await _softwaresService.DeleteAsync(id);

            return StatusCode(response.StatusCode, new
            {
                response.Status,
                response.Message,
            });
        }
    }
}