using IntravisionTest.Model;

namespace IntravisionTest.Interfaces
{
    public interface IDrinkRepository
    {
        Task<IEnumerable<Drink>> GetFilteredAsync(int? brandId, decimal? minPrice, decimal? maxPrice,CancellationToken cancellationToken);
        Task<Drink?> GetByIdAsync(int id, CancellationToken cancellationToken);
        Task AddAsync(Drink drink, CancellationToken cancellationToken);
        Task UpdateAsync(Drink drink, CancellationToken cancellationToken);
        Task DeleteAsync(Drink drink, CancellationToken cancellationToken);

        Task AddRangeAsync(List<Drink> drinks, CancellationToken cancellationToken);
    }
}
