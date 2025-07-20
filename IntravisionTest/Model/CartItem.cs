
namespace IntravisionTest.Model
{
    public class CartItem
    {
        public int CartItemId { get; set; }

        public int DrinkId { get; set; }
        public Drink? Drink { get; set; }
        public int Quantity { get; set; }
    }
}
