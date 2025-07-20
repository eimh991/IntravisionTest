using IntravisionTest.Model;

namespace IntravisionTest.Interface
{
    public interface ICoinRepository
    {
        Task<List<Coin>> GetAllDescendingAsync(CancellationToken cancellationToken);
        Task<Coin?> GetByValueAsync(int value, CancellationToken cancellationToken);
        Task AddAsync(Coin coin, CancellationToken cancellationToken);
        Task SaveChangesAsync(CancellationToken cancellationToken);
    }
}
