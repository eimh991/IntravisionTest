using IntravisionTest.DTO;
using IntravisionTest.Interfaces;
using IntravisionTest.Model;

namespace IntravisionTest.Service
{
    public class DrinkService : IDrinkService
    {
        private readonly IDrinkRepository _drinkRepository;

        public DrinkService(IDrinkRepository drinkRepository)
        {
            _drinkRepository = drinkRepository;
        }


        public async Task<IEnumerable<DrinkResponceDTO>> GetFilteredDrinksAsync(int? brandId, decimal? minPrice, decimal? maxPrice, CancellationToken cancellationToken)
        {
            var drink = await _drinkRepository.GetFilteredAsync(brandId, minPrice, maxPrice, cancellationToken);

            return drink.Select(dr => new DrinkResponceDTO {
                Id = dr.DrinkId,
                Name = dr.DrinkName,
                Price = dr.Price,
                Quantity = dr.Stock,
                BrandName = dr.Brand?.BrandName ?? throw new Exception ("У напитка должен быть бренд")
            }
            );
        }
    }
}
