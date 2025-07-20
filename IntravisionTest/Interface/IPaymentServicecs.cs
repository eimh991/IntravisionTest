using IntravisionTest.DTO;

namespace IntravisionTest.Interface
{
    public interface IPaymentServicecs
    {
        Task<ChangeResponseDTO> PayOrderAsync(PaymentRequestDTO request, CancellationToken cancellationToken);
    }
}
