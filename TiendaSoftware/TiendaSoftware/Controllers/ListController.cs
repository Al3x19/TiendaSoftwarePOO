using TiendaSoftware.DataBase.Entities;
using TiendaSoftware.DTOS.Lists;
using TiendaSoftware.DTOS.Common;
using TiendaSoftware.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace TiendaSoftware.Controllers
{
    [ApiController]
    [Route("api/lists")]
    public class ListsController : ControllerBase
    {
        private readonly IListsService _listsService;

        public List<ListEntity> _lists { get; set; }

        public ListsController(IListsService listsService)
        {
            this._listsService = listsService;
        }
        [HttpGet]
        public async Task<ActionResult<ResponseDto<List<ListDto>>>> GetAll()
        {
            var response = await _listsService.GetListAsync();

            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseDto<ListDto>>> Get(Guid id)
        {
            var response = await _listsService.GetByIdAsync(id);

            return StatusCode(response.StatusCode, response);
        }
        [HttpPost]
        public async Task<ActionResult<ResponseDto<ListDto>>> Create(ListCreateDto dto)
        {
            var response = await _listsService.CreateAsync(dto);

            return StatusCode(response.StatusCode, response);

        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ResponseDto<List<ListDto>>>> Edit(ListEditDto dto, Guid id)
        {
            var response = await _listsService.EditAsync(dto, id);

            return StatusCode(response.StatusCode, response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ResponseDto<ListDto>>> Delete(Guid id)
        {
            var category = await _listsService.GetByIdAsync(id);
            var response = await _listsService.DeleteAsync(id);

            return StatusCode(response.StatusCode, response);
        }
    }
}
