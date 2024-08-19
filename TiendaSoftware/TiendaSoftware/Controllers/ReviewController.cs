using TiendaSoftware.DTOS.Common;
using TiendaSoftware.DTOS.Reviews;
using TiendaSoftware.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace TiendaSoftware.Controllers
{
    [Route("api/reviews")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewService _reviewsService;

        public ReviewController(IReviewService reviewsService)
        {
            this._reviewsService = reviewsService;
        }

        [HttpGet]
        public async Task<ActionResult<ResponseDto<PaginationDto<List<ReviewDto>>>>> PaginationList(
            string searchTerm, int page = 1)
        {
            var response = await _reviewsService.GetReviewsListAsync(searchTerm, page);

            return StatusCode(response.StatusCode, new
            {
                response.Status,
                response.Message,
                response.Data
            });
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseDto<ReviewDto>>> GetOneById(Guid id)
        {
            var response = await _reviewsService.GetByIdAsync(id);

            return StatusCode(response.StatusCode, new
            {
                response.Status,
                response.Message,
                response.Data
            });
        }

        [HttpPost]
        public async Task<ActionResult<ResponseDto<ReviewDto>>> Create(ReviewCreateDto dto)
        {
            var response = await _reviewsService.CreateAsync(dto);

            return StatusCode(response.StatusCode, new
            {
                response.Status,
                response.Message,
            });
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ResponseDto<ReviewDto>>> Edit(ReviewEditDto dto,
            Guid id)
        {
            var response = await _reviewsService.EditAsync(dto, id);

            return StatusCode(response.StatusCode, new
            {
                response.Status,
                response.Message,
            });
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ResponseDto<ReviewDto>>> Delete(Guid id)
        {
            var response = await _reviewsService.DeleteAsync(id);

            return StatusCode(response.StatusCode, new
            {
                response.Status,
                response.Message,
            });
        }
    }
}