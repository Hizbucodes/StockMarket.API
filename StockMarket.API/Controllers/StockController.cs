using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StockMarket.API.Data;
using StockMarket.API.Dtos.Stock;
using StockMarket.API.Interfaces;
using StockMarket.API.Mappers;

namespace StockMarket.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StockController : ControllerBase
    {
       
        private readonly IStockRepository stockRepository;

        public StockController(IStockRepository stockRepository)
        {
            
            this.stockRepository = stockRepository;
        }


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var stocks = await stockRepository.GetAllAsync();

            var stockDto = stocks.Select(s => s.ToStockDto());

            return Ok(stockDto);
        }

        [HttpGet("{id:int}")]
        public async  Task<IActionResult> GetById([FromRoute] int id)
        {
            var stock = await stockRepository.GetByIdAsync(id);

            if (stock is null)
            {
                return NotFound();
            }

             return Ok(stock.ToStockDto());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateStockRequestDto stockDto)
        {
            // Convert to Stock Model from StockDTO
            var stockModel = stockDto.ToStockFromCreateDto();

            // use the domain model to create the stock
            await stockRepository.CreateAsync(stockModel);

            return CreatedAtAction(nameof(GetById), new { id = stockModel.Id }, stockModel.ToStockDto());
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateStockRequestDto updateDto)
        {
            // Convert update stock dto to stock model
            var stockModel = updateDto.ToStockFromUpdateDto();

           stockModel = await stockRepository.UpdateAsync(id, stockModel);

            if (stockModel is null)
            {
                return NotFound();
            }

            return Ok(stockModel.ToStockDto());
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var stockModel = await stockRepository.DeleteAsync(id);

            if (stockModel is null)
            {
                return NotFound();
            }

            return Ok(stockModel.ToStockDto());
        }
          
    }
}
