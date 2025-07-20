using IntravisionTest.Model;

namespace IntravisionTest.Interface
{
    public interface IBrandRepository
    {
        Task <IEnumerable<Brand>> GetAllBrandsAsync(CancellationToken cancellationToken);
    }
}
