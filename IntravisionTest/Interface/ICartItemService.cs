using IntravisionTest.DTO;
using IntravisionTest.Model;

namespace IntravisionTest.Interface
{
    public interface ICartItemService
    {
        Task<List<CartItemDTO>> GetCartAsync(CancellationToken cancellationToken);
        Task AddOrUpdateItemAsync(int drinkId, int quantity, CancellationToken cancellationToken);
        Task RemoveItemAsync(int cartItemId, CancellationToken cancellationToken);
        Task ClearCartAsync(CancellationToken cancellationToken);
    }
}
