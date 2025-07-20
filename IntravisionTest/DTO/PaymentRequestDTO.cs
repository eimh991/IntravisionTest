namespace IntravisionTest.DTO
{
    public class PaymentRequestDTO
    {
        public int OrderId { get; set; }
        public List<CoinInputDTO> Coins { get; set; } = new List<CoinInputDTO>();
    }
}
