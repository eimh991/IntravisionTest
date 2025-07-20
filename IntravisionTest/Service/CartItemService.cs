using IntravisionTest.Data;
using IntravisionTest.DTO;
using IntravisionTest.Interface;
using IntravisionTest.Model;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace IntravisionTest.Service
{
    public class CartItemService : ICartItemService
    {
        private readonly ICartItemRepository _cartRepository;
        private readonly AppDbContext _context;

        public CartItemService(ICartItemRepository cartRepository, AppDbContext context)
        {
            _cartRepository = cartRepository;
            _context = context;

        }

        public async Task<List<CartItemDTO>> GetCartAsync(CancellationToken cancellationToken)
        {
            var cartItems =  await _cartRepository.GetAllAsync(cancellationToken);

           return cartItems.Select(c => new CartItemDTO
            {
                CartItemDTOId = c.CartItemId,
                DrinkId = c.DrinkId,
                DrinkName = c.Drink?.DrinkName,
                Quantity = c.Quantity,
                Price = c.Drink.Price,
                MaxQuantity = c.Drink.Stock,
            }).ToList();
            
        }

        public async Task AddOrUpdateItemAsync(int drinkId, int quantity, CancellationToken cancellationToken)
        {
            var drink = await _context.Drinks.FirstOrDefaultAsync(d =>d.DrinkId == drinkId, cancellationToken);
            if (drink == null || quantity < 1 || quantity > drink.Stock)
                throw new InvalidOperationException("Некорректное количество или товар не найден");

            var existing = await _cartRepository.GetByProductIdAsync(drinkId, cancellationToken);

            if (existing != null)
            {
                existing.Quantity = quantity;
                _cartRepository.Update(existing);
            }
            else
            {
                var newItem = new CartItem { DrinkId = drinkId, Quantity = quantity };
                await _cartRepository.AddAsync(newItem, cancellationToken);
            }

            await _cartRepository.SaveChangesAsync(cancellationToken);
        }

        public async Task RemoveItemAsync(int cartItemId, CancellationToken cancellationToken)
        {
            var item = await _cartRepository.GetByIdAsync(cartItemId, cancellationToken);
            if (item != null)
            {
                _cartRepository.Remove(item);
                await _cartRepository.SaveChangesAsync(cancellationToken);
            }
        }

        public async Task ClearCartAsync(CancellationToken cancellationToken)
        {
            var items = await _cartRepository.GetAllAsync(cancellationToken);
            _cartRepository.RemoveRange(items);
            await _cartRepository.SaveChangesAsync(cancellationToken);
        }
    }
}
