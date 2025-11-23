using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StockMarket.API.Dtos.Comment;
using StockMarket.API.Extensions;
using StockMarket.API.Interfaces;
using StockMarket.API.Mappers;
using StockMarket.API.Models;
using StockMarket.API.Repository;

namespace StockMarket.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IStockRepository stockRepository;
        private readonly UserManager<AppUser> userManager;
        public CommentController(ICommentRepository commentRepository, IStockRepository stockRepository, UserManager<AppUser> userManager)
        {
            this._commentRepository = commentRepository;
            this.stockRepository = stockRepository;
            this.userManager = userManager;
        }


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
           
          var comments =  await _commentRepository.GetAllAsync();

            var commentDto =  comments.Select(s => s.ToCommentDto());
            return Ok(commentDto);
        }


        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var comment = await _commentRepository.GetByIdAsync(id);

            if (comment is null)
            {
                return NotFound();
            }

            return Ok(comment.ToCommentDto());
        }

        [HttpPost("{stockId:int}")]
        public async Task<IActionResult> Create([FromRoute] int stockId, [FromBody] CreateCommentDto commentDto)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest();
            }


            var stockIdExists = await stockRepository.StockExists(stockId);

            if (!stockIdExists)
            {
                return BadRequest("Stock does not exist");
            }

            var username = User.GetUsername();

            var appUser = await userManager.FindByNameAsync(username);


            var commentModal = commentDto.ToCommentFromCreateDto(stockId);

            commentModal.AppUserId = appUser.Id;

            await _commentRepository.CreateAsync(commentModal);

            return CreatedAtAction(nameof(GetById), new { id = commentModal.Id }, commentModal.ToCommentDto());
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateCommentRequestDto commentDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var commentModal = commentDto.ToCommentFromUpdateDto();

            var comment = await _commentRepository.UpdateAsync(id, commentModal);

            if (comment is null)
            {
                return NotFound("Comment Not Found");
            }

            return Ok(comment.ToCommentDto());
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var commentModel =  await _commentRepository.DeleteAsync(id);

            if(!commentModel)
            {
                return NotFound("Comment Not Found");
            }

           

            return NoContent();
        }
    }
}
