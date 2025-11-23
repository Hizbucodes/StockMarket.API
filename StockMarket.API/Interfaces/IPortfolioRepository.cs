using StockMarket.API.Models;

namespace StockMarket.API.Interfaces
{
    public interface IPortfolioRepository
    {
        Task<List<Stock>> GetUserPortfolio(AppUser user);
        Task<Portfolio> CreateAsync(Portfolio portfolio);

        Task<Portfolio> DeleteAsync(AppUser appUser,string symbol);
    }
}
