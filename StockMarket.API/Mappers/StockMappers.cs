using StockMarket.API.Dtos.Stock;
using StockMarket.API.Models;

namespace StockMarket.API.Mappers
{
    public static class StockMappers
    {
        public static StockDto ToStockDto(this Stock stockModel)
        {
            return new StockDto
            {
                Id = stockModel.Id,
                Symbol = stockModel.Symbol,
                CompanyName = stockModel.CompanyName,
                Purchase = stockModel.Purchase,
                LastDiv = stockModel.LastDiv,
                Industry = stockModel.Industry,
                MarketCap = stockModel.MarketCap,
                Comments = stockModel.Comments.Select(c => c.ToCommentDto()).ToList()
            };
        }

        public static Stock ToStockFromCreateDto(this CreateStockRequestDto stockDto)
        {
            return new Stock
            {
                Symbol = stockDto.Symbol,
                CompanyName = stockDto.CompanyName,
                Purchase = stockDto.Purchase,
                LastDiv = stockDto.LastDiv,
                Industry = stockDto.Industry,
                MarketCap = stockDto.MarketCap,
            };
        }

        public static Stock ToStockFromUpdateDto(this UpdateStockRequestDto stockUpdateDto)
        {
            return new Stock
            {
                Symbol = stockUpdateDto.Symbol,
                CompanyName = stockUpdateDto.CompanyName,
                Purchase = stockUpdateDto.Purchase,
                LastDiv = stockUpdateDto.LastDiv,
                Industry = stockUpdateDto.Industry,
                MarketCap = stockUpdateDto.MarketCap,
            };
        }
    }
}
