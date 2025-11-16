using StockMarket.API.Models;

namespace StockMarket.API.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(AppUser user);
    }
}
