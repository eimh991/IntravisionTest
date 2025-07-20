using IntravisionTest.Model;

namespace IntravisionTest.Interface
{
    public interface IOrderRepository
    {
        Task<List<CartItem>> GetCartItemsWithProductsAsync(CancellationToken cancellationToken);
        Task AddOrderAsync(Order order, CancellationToken cancellationToken);
        void RemoveCartItems(List<CartItem> items, CancellationToken cancellationToken);
        Task<List<Order>> GetAllOrdersWithItemsAsync(CancellationToken cancellationToken);
        Task SaveChangesAsync(CancellationToken cancellationToken);

    }
}
