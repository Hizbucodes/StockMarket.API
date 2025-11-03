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

        public async Task<Comment> CreateAsync(Comment commentModal)
        {
            await _context.AddAsync(commentModal);
            await _context.SaveChangesAsync();
            return commentModal;
        }

        public async Task<List<Comment>> GetAllAsync()
        {
            return await _context.Comments.ToListAsync();
        }

        public async Task<Comment?> GetByIdAsync(int id)
        {
            return await _context.Comments.FindAsync(id);
        }
    }
}
