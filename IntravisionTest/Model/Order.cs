namespace IntravisionTest.Model
{
    public class Order
    {
        public int OrderId { get; set; }
        public decimal TotalPrice{ get; set; }
        public DateTime CreatedAt { get; init; } = DateTime.UtcNow;
        public List<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
}
