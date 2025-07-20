using IntravisionTest.DTO;
using IntravisionTest.Interface;
using IntravisionTest.Model;

namespace IntravisionTest.Service
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;

        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }
        public async Task<int> CreateOrderFromCartAsync(CancellationToken cancellationToken)
        {
            var cartItems = await _orderRepository.GetCartItemsWithProductsAsync(cancellationToken);

            if (!cartItems.Any())
                throw new InvalidOperationException("Корзина пуста");

            var total = cartItems.Sum(ci => ci.Quantity * ci.Drink.Price);

            var order = new Order
            {
                CreatedAt = DateTime.UtcNow,
                TotalPrice = total,
                OrderItems = new List<OrderItem>()
            };

            foreach (var cartItem in cartItems)
            {
                if (cartItem.Drink.Stock < cartItem.Quantity)
                    throw new InvalidOperationException($"Недостаточно на складе: {cartItem.Drink.DrinkName}");

                cartItem.Drink.Stock -= cartItem.Quantity;

                order.OrderItems.Add(new OrderItem
                {
                    BrandName = cartItem.Drink.Brand.BrandName,
                    DrinkName = cartItem.Drink.DrinkName,
                    Quantity = cartItem.Quantity,
                    UnitPrice = cartItem.Drink.Price
                });
            }

            await _orderRepository.AddOrderAsync(order, cancellationToken);
            _orderRepository.RemoveCartItems(cartItems,cancellationToken);
            await _orderRepository.SaveChangesAsync(cancellationToken);

            return order.OrderId;
        }

        public async Task<List<OrderResponseDTO>> GetAllOrdersAsync(CancellationToken cancellationToken)
        {
            var orders = await _orderRepository.GetAllOrdersWithItemsAsync(cancellationToken);

            return orders.Select(o => new OrderResponseDTO
            {
                OrderId = o.OrderId,
                CreatedAt = o.CreatedAt,
                TotalPrice = o.TotalPrice,
                Items = o.OrderItems.Select(oi => new OrderItemDTO
                {
                    BrandName = oi.BrandName,
                    DrinkName = oi.DrinkName,
                    Quantity = oi.Quantity,
                    UnitPrice = oi.UnitPrice
                }).ToList()
            }).ToList();
        }
    }
}
