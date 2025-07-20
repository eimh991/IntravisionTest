using IntravisionTest.Data;
using IntravisionTest.Interface;
using IntravisionTest.Model;
using Microsoft.EntityFrameworkCore;

namespace IntravisionTest.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly AppDbContext _context;

        public OrderRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task AddOrderAsync(Order order, CancellationToken cancellationToken)
        {
            await _context.Orders.AddAsync(order, cancellationToken);
        }

        public async Task<List<Order>> GetAllOrdersWithItemsAsync(CancellationToken cancellationToken)
        {
           return await _context.Orders
                .Include(o=>o.OrderItems)
                .ToListAsync(cancellationToken);
        }

        public async Task<List<CartItem>> GetCartItemsWithProductsAsync(CancellationToken cancellationToken)
        {
            return await _context.CartItems
                .Include(ci => ci.Drink)
                .ThenInclude(d => d.Brand)
                .ToListAsync(cancellationToken);
        }

        public void RemoveCartItems(List<CartItem> items, CancellationToken cancellationToken)
        {
            _context.CartItems.RemoveRange(items);
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken)
        {
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
