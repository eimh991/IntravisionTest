using IntravisionTest.DTO;

namespace IntravisionTest.Interfaces
{
    public interface IDrinkService
    {
         Task<IEnumerable<DrinkResponceDTO>> GetFilteredDrinksAsync(int? brandId, decimal? minPrice, decimal? maxPrice, CancellationToken cancellationToken);
    }
}
