using TiendaSoftware.DataBase.Entities;
using TiendaSoftware.DTOS.Softwares;
using TiendaSoftware.DTOS.Common;
using TiendaSoftware.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace TiendaSoftware.Controllers
{
    [ApiController]
    [Route("api/software")]
    public class SoftwaresController : ControllerBase
    {
        private readonly ISoftwaresService _softwaresService;

        public List<PublisherEntity> _publishers { get; set; }

        public SoftwaresController(ISoftwaresService softwaresService)
        {
            this._softwaresService = softwaresService;
        }
        [HttpGet]
        public async Task<ActionResult<ResponseDto<List<SoftwareDto>>>> GetAll()
        {
            var response = await _softwaresService.GetListAsync();

            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseDto<SoftwareDto>>> Get(Guid id)
        {
            var response = await _softwaresService.GetByIdAsync(id);

            return StatusCode(response.StatusCode, response);
        }
        [HttpPost]
        public async Task<ActionResult<ResponseDto<SoftwareDto>>> Create(SoftwareCreateDto dto)
        {
            var response = await _softwaresService.CreateAsync(dto);

            return StatusCode(response.StatusCode, response);

        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ResponseDto<List<SoftwareDto>>>> Edit(SoftwareEditDto dto, Guid id)
        {
            var response = await _softwaresService.EditAsync(dto, id);

            return StatusCode(response.StatusCode, response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ResponseDto<SoftwareDto>>> Delete(Guid id)
        {
            //var category = await _softwaresService.GetPublisherByIdAsync(id);
            var response = await _softwaresService.DeleteAsync(id);

            return StatusCode(response.StatusCode, response);
        }
    }
}
