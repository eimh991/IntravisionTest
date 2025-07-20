namespace IntravisionTest.Model
{
    public class OrderItem
    {
        public int OrderItemId { get; set; }
        public string DrinkName { get; set; } = string.Empty;
        public string BrandName { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }

        public int OrderId { get; set; }
        public Order? Order { get; set; }
    }
}
