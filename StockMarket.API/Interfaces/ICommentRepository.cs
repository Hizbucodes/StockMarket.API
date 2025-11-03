using StockMarket.API.Models;

namespace StockMarket.API.Interfaces
{
    public interface ICommentRepository
    {
        Task<List<Comment>> GetAllAsync();
        Task<Comment?> GetByIdAsync(int id);
        Task<Comment> CreateAsync(Comment commentModal);
        Task<bool> CommentExists(int id);
        Task<Comment?> UpdateAsync(int id, Comment commentModal);
        Task<bool> DeleteAsync(int id);

    }
}
