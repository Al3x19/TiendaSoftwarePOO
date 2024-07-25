using TiendaSoftware.DataBase.Entities;
using TiendaSoftware.DTOS.Publishers;
using TiendaSoftware.DTOS.Common;
using TiendaSoftware.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace TiendaSoftware.Controllers
{
    [ApiController]
    [Route("api/publishers")]
    public class PublishersController : ControllerBase
    {
        private readonly IPublisherService _publishersService;

        public List<PublisherEntity> _publishers {get; set; }

        public PublishersController(IPublisherService publishersService) 
        {
            this._publishersService = publishersService;
        }
        [HttpGet]
        public async Task<ActionResult<ResponseDto<List<PublisherDto>>>> GetAll() 
        {
            var response = await _publishersService.GetPublishersListAsync();

            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseDto<PublisherDto>>> Get( Guid  id)
        {
            var response = await _publishersService.GetPublisherByIdAsync(id);

            return StatusCode(response.StatusCode, response);
        }
        [HttpPost]
        public async Task<ActionResult<ResponseDto<PublisherDto>>> Create(PublisherCreateDto dto) 
        {
            var response = await _publishersService.CreateAsync( dto);

            return StatusCode(response.StatusCode, response);

        }

        [HttpPut]
        public async Task<ActionResult<ResponseDto<List<PublisherDto>>>> Edit(PublisherEditDto dto, Guid id)
        {
            var response = await _publishersService.EditAsync(dto, id);

            return StatusCode(response.StatusCode, response);   
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ResponseDto<PublisherDto>>> Delete(Guid id) 
        {
            var category = await _publishersService.GetPublisherByIdAsync(id);
            var response = await _publishersService.DeleteAsync(id);

            return StatusCode(response.StatusCode, response);
        }
    }
}
