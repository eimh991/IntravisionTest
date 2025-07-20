using IntravisionTest.DTO;
using IntravisionTest.Interface;
using IntravisionTest.Model;
using IntravisionTest.Repositories;

namespace IntravisionTest.Service
{
    public class CartPaymentService : ICartPaymentService
    {
        private readonly IOrderRepository _repository;

        public CartPaymentService(IOrderRepository repository)
        {
            _repository = repository;
        }

        public async Task<ChangeResponseDTO> PayAsync(CartPaymentRequestDTO dto, CancellationToken cancellationToken)
        {
            var cartItems = await _repository.GetCartItemsWithProductsAsync(cancellationToken);

            if (!cartItems.Any())
                throw new InvalidOperationException("Корзина пуста");

            var total = cartItems.Sum(ci => ci.Drink.Price * ci.Quantity);
            var paid = dto.Coins.Sum(c => c.Value * c.Quantity);

            if (paid < total)
                throw new InvalidOperationException($"Недостаточно средств. Нужно {total}, внесено {paid}");

            var order = new Order
            {
                TotalPrice = total,
                OrderItems = cartItems.Select(ci => new OrderItem
                {
                    OrderItemId = ci.CartItemId,
                    Quantity = ci.Quantity,
                    UnitPrice = ci.Drink.Price,
                    DrinkName = ci.Drink.DrinkName,
                    BrandName = ci.Drink.Brand?.BrandName ?? ""
                }).ToList()
            };

            await _repository.AddOrderAsync(order, cancellationToken);

            foreach (var ci in cartItems)
            {
                ci.Drink.Stock -= ci.Quantity;
            }

            _repository.RemoveCartItems(cartItems, cancellationToken);
            await _repository.SaveChangesAsync(cancellationToken);

            return new ChangeResponseDTO
            {
                ChangeAmount = paid - total
            };
        }
    }
}
