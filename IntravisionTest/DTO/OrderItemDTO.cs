namespace IntravisionTest.DTO
{
    public class OrderItemDTO
    {
        public string DrinkName { get; set; } = string.Empty;
        public string BrandName { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
