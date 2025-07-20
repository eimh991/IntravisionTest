using IntravisionTest.Data;
using IntravisionTest.Interfaces;
using IntravisionTest.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace IntravisionTest.Repositories
{
    public class DrinkRepository : IDrinkRepository
    {
        private readonly AppDbContext _dbContext;

        public DrinkRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task AddAsync(Drink drink, CancellationToken cancellationToken)
        {
            await _dbContext.Drinks.AddAsync(drink,cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAsync(Drink drink, CancellationToken cancellationToken)
        {
          _dbContext.Drinks.Remove(drink);
          await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<Drink?> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            var drink = await _dbContext.Drinks.FirstOrDefaultAsync(d => d.DrinkId == id, cancellationToken);
            if(drink != null)
            {
                return drink;
            }
            throw new NullReferenceException();
        }

        public async Task<IEnumerable<Drink>> GetFilteredAsync(int? brandId, decimal? minPrice, decimal? maxPrice, CancellationToken cancellationToken)
        {
            var query = _dbContext.Drinks.AsQueryable();

            if (brandId.HasValue)
            {
                query = query.Where(d => d.BrandId == brandId);
            }

            if (minPrice.HasValue)
            {
                query = query.Where(d => d.Price >= minPrice);
            }

            if (maxPrice.HasValue)
            {
                query = query.Where(d => d.Price <= maxPrice);
            }

            query = query.Include(d => d.Brand);

            return await query.ToListAsync(cancellationToken);
        }

        public async Task UpdateAsync(Drink drink, CancellationToken cancellationToken)
        {
            await _dbContext.Drinks
                .Where(d => d.DrinkId == drink.DrinkId)
                .ExecuteUpdateAsync(s => s
                .SetProperty(d => d.Price, drink.Price)
                .SetProperty(d=>d.Stock, drink.Stock)
                .SetProperty(d=>d.DrinkName, drink.DrinkName)
                .SetProperty(d=>d.BrandId, drink.BrandId)
                , cancellationToken);
        }

        public async Task AddRangeAsync(List<Drink> drinks, CancellationToken cancellationToken)
        {
            await _dbContext.Drinks.AddRangeAsync(drinks, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
