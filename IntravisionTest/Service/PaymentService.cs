using IntravisionTest.Data;
using IntravisionTest.DTO;
using IntravisionTest.Interface;
using IntravisionTest.Model;
using Microsoft.EntityFrameworkCore;

namespace IntravisionTest.Service
{
    public class PaymentService : IPaymentServicecs
    {
        private readonly AppDbContext _context;

        public PaymentService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<ChangeResponseDTO> PayOrderAsync(PaymentRequestDTO request, CancellationToken cancellationToken)
        {
            var order = await _context.Orders.FirstOrDefaultAsync(o => o.OrderId == request.OrderId, cancellationToken);
            if (order == null)
                throw new InvalidOperationException("Заказ не найден");

            var paidAmount = request.Coins.Sum(c => c.Value * c.Quantity);

            if (paidAmount < order.TotalPrice)
                throw new InvalidOperationException("Недостаточно средств");

            // 1. Пополняем банк монет
            foreach (var coin in request.Coins)
            {
                var dbCoin = await _context.Coins.FirstOrDefaultAsync(c => c.Value == coin.Value, cancellationToken);
                if (dbCoin != null)
                    dbCoin.Quantity += coin.Quantity;
                else
                    await _context.Coins.AddAsync(new Coin { Value = coin.Value, Quantity = coin.Quantity }, cancellationToken);
            }

            // 2. Выдаем сдачу
            var change = paidAmount - order.TotalPrice;
            var changeCoins = await CalculateChangeAsync(change, cancellationToken);

            // 3. Сохраняем изменения
            await _context.SaveChangesAsync(cancellationToken);

            return new ChangeResponseDTO
            {
                ChangeAmount = change,
                ChangeCoins = changeCoins
            };
        }

        private async Task<Dictionary<int, int>> CalculateChangeAsync(decimal change, CancellationToken cancellationToken)
        {
            var availableCoins = await _context.Coins
                .OrderByDescending(c => c.Value)
                .ToListAsync(cancellationToken);

            var result = new Dictionary<int, int>();

            foreach (var coin in availableCoins)
            {
                int needed = (int)(change / coin.Value);
                int used = Math.Min(needed, coin.Quantity);

                if (used > 0)
                {
                    result[coin.Value] = used;
                    coin.Quantity -= used;
                    change -= used * coin.Value;
                }
            }

            if (change > 0.01m)
                throw new InvalidOperationException("Невозможно выдать сдачу доступными монетами");

            return result;
        }
    }
    
}
