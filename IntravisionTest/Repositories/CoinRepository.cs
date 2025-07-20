using IntravisionTest.Data;
using IntravisionTest.Interface;
using IntravisionTest.Model;
using Microsoft.EntityFrameworkCore;

namespace IntravisionTest.Repositories
{
    public class CoinRepository : ICoinRepository
    {
        private readonly AppDbContext _context;

        public CoinRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(Coin coin, CancellationToken cancellationToken)
        {
            await _context.Coins.AddAsync(coin, cancellationToken);
        }

        public async Task<List<Coin>> GetAllDescendingAsync(CancellationToken cancellationToken)
        {
            return await _context.Coins
                .OrderByDescending(c => c.Value)
                .ToListAsync(cancellationToken);
        }

        public async Task<Coin?> GetByValueAsync(int value, CancellationToken cancellationToken)
        {
            return await _context.Coins
                .FirstOrDefaultAsync(c => c.Value == value, cancellationToken);
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken)
        {
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
