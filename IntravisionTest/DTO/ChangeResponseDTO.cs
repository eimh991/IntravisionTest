namespace IntravisionTest.DTO
{
    public class ChangeResponseDTO
    {
        public decimal ChangeAmount { get; set; }
        public Dictionary<int, int> ChangeCoins { get; set; } = new Dictionary<int, int>();
    }
}
