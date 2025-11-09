using StockMarket.API.Models;

namespace StockMarket.API.Interfaces
{
    public interface IStockRepository
    {
        Task<List<Stock>> GetAllAsync(string? filterOn = null, string? filterQuery = null, string? sortBy = null, bool isAscending = true);
        Task<Stock?> GetByIdAsync(int id);
        Task<Stock> CreateAsync(Stock stockModel);
        Task<Stock?> UpdateAsync(int id, Stock stockModel);
        Task<Stock?> DeleteAsync(int id);

        Task<bool> StockExists(int id);
    }
}
