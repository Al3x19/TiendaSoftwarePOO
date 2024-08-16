using TiendaSoftware.DataBase.Entities;
using TiendaSoftware.DTOS.Reviews;
using TiendaSoftware.DTOS.Common;
using TiendaSoftware.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace TiendaSoftware.Controllers
{
    [ApiController]
    [Route("api/reviews")]
    public class ReviewsController : ControllerBase
    {
        private readonly IReviewService _reviewsService;

        public List<ReviewEntity> _reviews { get; set; }

        public ReviewsController(IReviewService reviewsService)
        {
            this._reviewsService = reviewsService;
        }
        [HttpGet]
        public async Task<ActionResult<ResponseDto<List<ReviewDto>>>> GetAll()
        {
            var response = await _reviewsService.GetReviewsListAsync();

            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseDto<ReviewDto>>> Get(Guid id)
        {
            var response = await _reviewsService.GetByIdAsync(id);

            return StatusCode(response.StatusCode, response);
        }
        [HttpPost]
        public async Task<ActionResult<ResponseDto<ReviewDto>>> Create(ReviewCreateDto dto)
        {
            var response = await _reviewsService.CreateAsync(dto);

            return StatusCode(response.StatusCode, response);

        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ResponseDto<List<ReviewDto>>>> Edit(ReviewEditDto dto, Guid id)
        {
            var response = await _reviewsService.EditAsync(dto, id);

            return StatusCode(response.StatusCode, response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ResponseDto<ReviewDto>>> Delete(Guid id)
        {
            var category = await _reviewsService.GetByIdAsync(id);
            var response = await _reviewsService.DeleteAsync(id);

            return StatusCode(response.StatusCode, response);
        }
    }
}
