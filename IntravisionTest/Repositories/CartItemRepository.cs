using IntravisionTest.Data;
using IntravisionTest.DTO;
using IntravisionTest.Interface;
using IntravisionTest.Model;
using Microsoft.EntityFrameworkCore;

namespace IntravisionTest.Repositories
{
    public class CartItemRepository : ICartItemRepository
    {
        private readonly AppDbContext _context;

        public CartItemRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<CartItem>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _context.CartItems
                .Include(ci => ci.Drink)
                .ToListAsync(cancellationToken);
        }
        public async Task<CartItem?> GetByProductIdAsync(int productId, CancellationToken cancellationToken)
        {
            return await _context.CartItems.FirstOrDefaultAsync(ci => ci.DrinkId == productId, cancellationToken);
        }

        public async Task<CartItem?> GetByIdAsync(int cartItemId, CancellationToken cancellationToken)
        {
            return await _context.CartItems.FirstOrDefaultAsync(ci => ci.CartItemId == cartItemId, cancellationToken);
        }

        public async Task AddAsync(CartItem cartItem, CancellationToken cancellationToken)
        {
            await _context.CartItems.AddAsync(cartItem, cancellationToken);
        }

        public void Update(CartItem cartItem)
        {
            _context.CartItems.Update(cartItem);
        }


        public void Remove(CartItem cartItem)
        {
            _context.CartItems.Remove(cartItem);
        }

        public void RemoveRange(IEnumerable<CartItem> cartItems)
        {
            _context.CartItems.RemoveRange(cartItems);
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken)
        {
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}

