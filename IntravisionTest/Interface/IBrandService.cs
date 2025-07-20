using IntravisionTest.DTO;
using IntravisionTest.Model;

namespace IntravisionTest.Interface
{
    public interface IBrandService
    {
        Task<IEnumerable<BrandDTO>> GetAllBrandsAsync(CancellationToken cancellationToken);
    }
}
