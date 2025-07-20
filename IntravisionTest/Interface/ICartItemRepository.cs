using IntravisionTest.DTO;
using IntravisionTest.Model;

namespace IntravisionTest.Interface
{
    public interface ICartItemRepository
    {
        Task<List<CartItem>> GetAllAsync(CancellationToken cancellationToken);
        Task<CartItem?> GetByProductIdAsync(int productId, CancellationToken cancellationToken);
        Task<CartItem?> GetByIdAsync(int cartItemId, CancellationToken cancellationToken);
        Task AddAsync(CartItem cartItem, CancellationToken cancellationToken);
        void Update(CartItem cartItem);
        void Remove(CartItem cartItem);
        void RemoveRange(IEnumerable<CartItem> cartItems);
        Task SaveChangesAsync(CancellationToken cancellationToken);
    }
}
