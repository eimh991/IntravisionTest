namespace IntravisionTest.DTO
{
    public class OrderResponseDTO
    {
        public int OrderId { get; set; }
        public DateTime CreatedAt { get; set; }
        public decimal TotalPrice { get; set; }
        public List<OrderItemDTO> Items { get; set; } = new();
    }
}
