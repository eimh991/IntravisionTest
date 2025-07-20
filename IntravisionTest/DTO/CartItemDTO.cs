using IntravisionTest.Model;

namespace IntravisionTest.DTO
{
    public class CartItemDTO
    {
        public int CartItemDTOId { get; set; }

        public int DrinkId { get; set; }
        public string DrinkName { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public int MaxQuantity { get; set; }
    }
}
