using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using StockMarket.API.Data;
using StockMarket.API.Interfaces;
using StockMarket.API.Mappers;
using StockMarket.API.Models;

namespace StockMarket.API.Repository
{
    public class StockRepository : IStockRepository
    {
        private readonly ApplicationDBContext _context;

        public StockRepository(ApplicationDBContext _context) 
        {
            this._context = _context;
        }

        public async Task<Stock> CreateAsync(Stock stockModel)
        {
            await _context.Stocks.AddAsync(stockModel);
            await _context.SaveChangesAsync();

            return stockModel;
        }

        public async Task<Stock?> DeleteAsync(int id)
        {
            var existingStockModel = await _context.Stocks.FirstOrDefaultAsync(x => x.Id == id);

            if(existingStockModel is null)
            {
                return null;
            }

            _context.Stocks.Remove(existingStockModel);
            await _context.SaveChangesAsync();

            return existingStockModel;
        }

        public async Task<List<Stock>> GetAllAsync(string? filterOn = null, string? filterQuery = null, string? sortBy = null, bool isAscending = true, int pageNumber = 1, int pageSize = 5)
        {
            var stocks = _context.Stocks.Include(c => c.Comments).AsQueryable();

            // Filtering
            if (string.IsNullOrWhiteSpace(filterOn) == false && string.IsNullOrWhiteSpace(filterQuery) == false)
            {
                if (filterOn.Equals("CompanyName", StringComparison.OrdinalIgnoreCase))
                {
                    stocks = stocks.Where(s => s.CompanyName.Contains(filterQuery));
                }

                if(filterOn.Equals("Symbol", StringComparison.OrdinalIgnoreCase))
                {
                    stocks = stocks.Where(s => s.Symbol.Contains(filterQuery));
                }
            }

            // Sorting
            if(string.IsNullOrWhiteSpace(sortBy) == false)
            {
                if(sortBy.Equals("CompanyName", StringComparison.OrdinalIgnoreCase))
                {
                    stocks = isAscending ? stocks.OrderBy(s => s.CompanyName) : stocks.OrderByDescending(s => s.CompanyName);
                }else if(sortBy.Equals("Symbol", StringComparison.OrdinalIgnoreCase))
                {
                    stocks = isAscending ? stocks.OrderBy(s => s.Symbol) : stocks.OrderByDescending(s => s.Symbol);
                }
            }

            // Pagination
            var skipResults = (pageNumber - 1) * pageSize;


            return await stocks.Skip(skipResults).Take(pageSize).ToListAsync();
        }

        public async Task<Stock?> GetByIdAsync(int id)
        {
            return await _context.Stocks.Include(c => c.Comments).FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<Stock?> GetBySymbolAsync(string symbol)
        {
            return await _context.Stocks.FirstOrDefaultAsync(s => s.Symbol == symbol);
        }

        public async Task<bool> StockExists(int id)
        {
            return await _context.Stocks.AnyAsync(s => s.Id == id);
        }

        public async Task<Stock?> UpdateAsync(int id, Stock stockModel)
        {
            var existingStockModel = await _context.Stocks.FirstOrDefaultAsync(x => x.Id == id);

            if (existingStockModel is null)
            {
                return null;
            }

 
            existingStockModel.LastDiv = stockModel.LastDiv;
            existingStockModel.Industry = stockModel.Industry;
            existingStockModel.Symbol = stockModel.Symbol;
            existingStockModel.Purchase = stockModel.Purchase;
            existingStockModel.MarketCap = stockModel.MarketCap;
            existingStockModel.CompanyName = stockModel.CompanyName;


            await _context.SaveChangesAsync();

            return existingStockModel;
        }
    }
}
