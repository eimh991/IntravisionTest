using IntravisionTest.DTO;

namespace IntravisionTest.Interface
{
    public interface ICartPaymentService
    {
        Task<ChangeResponseDTO> PayAsync(CartPaymentRequestDTO dto, CancellationToken cancellationToken);
    }
}

