using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using StockMarket.API.Dtos.Comment;
using StockMarket.API.Interfaces;
using StockMarket.API.Mappers;
using StockMarket.API.Repository;

namespace StockMarket.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IStockRepository stockRepository;

        public CommentController(ICommentRepository commentRepository, IStockRepository stockRepository)
        {
            this._commentRepository = commentRepository;
            this.stockRepository = stockRepository;
        }


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
           
          var comments =  await _commentRepository.GetAllAsync();

            var commentDto =  comments.Select(s => s.ToCommentDto());
            return Ok(commentDto);
        }


        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var comment = await _commentRepository.GetByIdAsync(id);

            if (comment is null)
            {
                return NotFound();
            }

            return Ok(comment.ToCommentDto());
        }

        [HttpPost("{stockId}")]
        public async Task<IActionResult> Create([FromRoute] int stockId, [FromBody] CreateCommentDto commentDto)
        {
            var stockIdExists = await stockRepository.StockExists(stockId);

            if (!stockIdExists)
            {
                return BadRequest("Stock does not exist");
            }

            var commentModal = commentDto.ToCommentFromCreateDto(stockId);

            await _commentRepository.CreateAsync(commentModal);

            return CreatedAtAction(nameof(GetById), new { id = commentModal }, commentModal.ToCommentDto());
        }
    }
}
