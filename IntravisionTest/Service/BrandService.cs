using IntravisionTest.DTO;
using IntravisionTest.Interface;

namespace IntravisionTest.Service
{
    public class BrandService : IBrandService
    {
        private readonly IBrandRepository _brandRepository;
        
        public BrandService (IBrandRepository brandRepository)
        {
            _brandRepository = brandRepository;
        }

        public async Task<IEnumerable<BrandDTO>> GetAllBrandsAsync(CancellationToken cancellationToken)
        {
            var bands = await _brandRepository.GetAllBrandsAsync(cancellationToken);

            return bands.Select(b => new BrandDTO
            {
                Id = b.BrandId,
                Name = b.BrandName,
            });
        }
    }
}
