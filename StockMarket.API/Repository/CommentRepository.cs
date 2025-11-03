using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using StockMarket.API.Data;
using StockMarket.API.Interfaces;
using StockMarket.API.Models;

namespace StockMarket.API.Repository
{
    public class CommentRepository : ICommentRepository
    {
        private readonly ApplicationDBContext _context;

        public CommentRepository(ApplicationDBContext context)
        {
            this._context = context;
        }

        public async Task<bool> CommentExists(int id)
        {
            return await _context.Comments.AnyAsync(c => c.Id == id);
        }

        public async Task<Comment> CreateAsync(Comment commentModal)
        {
            await _context.AddAsync(commentModal);
            await _context.SaveChangesAsync();
            return commentModal;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var commentExist = await _context.Comments.FindAsync(id);

            if (commentExist == null)
            {
                return false;
            }

            _context.Comments.Remove(commentExist);
            await _context.SaveChangesAsync();

            return true;

        }

        public async Task<List<Comment>> GetAllAsync()
        {
            return await _context.Comments.ToListAsync();
        }

        public async Task<Comment?> GetByIdAsync(int id)
        {
            return await _context.Comments.FindAsync(id);
        }

        public async Task<Comment?> UpdateAsync(int id, Comment commentModal)
        {
            var existingComment = await _context.Comments.FindAsync(id);

            if (existingComment is null)
            {
                return null;
            }

           existingComment.Title = commentModal.Title;
            existingComment.Content = commentModal.Content;

            await _context.SaveChangesAsync();

            return existingComment;
        }
    }
}
