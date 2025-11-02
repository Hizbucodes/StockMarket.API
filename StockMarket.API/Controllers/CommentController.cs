using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
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

        public CommentController(ICommentRepository commentRepository)
        {
            this._commentRepository = commentRepository;
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
    }
}
