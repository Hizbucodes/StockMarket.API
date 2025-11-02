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

        public async Task<List<Stock>> GetAllAsync()
        {
            return await _context.Stocks.Include(c => c.Comments).ToListAsync();
        }

        public async Task<Stock?> GetByIdAsync(int id)
        {
            return await _context.Stocks.Include(c => c.Comments).FirstOrDefaultAsync(i => i.Id == id);
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
