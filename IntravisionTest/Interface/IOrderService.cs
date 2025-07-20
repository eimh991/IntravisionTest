using IntravisionTest.DTO;

namespace IntravisionTest.Interface
{
    public interface IOrderService
    {
        Task<int> CreateOrderFromCartAsync(CancellationToken cancellationToken);
        Task<List<OrderResponseDTO>> GetAllOrdersAsync(CancellationToken cancellationToken);
    }
}
