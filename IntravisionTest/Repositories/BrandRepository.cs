using IntravisionTest.Data;
using IntravisionTest.Interface;
using IntravisionTest.Model;
using Microsoft.EntityFrameworkCore;

namespace IntravisionTest.Repositories
{
    public class BrandRepository : IBrandRepository
    {
        private readonly AppDbContext _context;
        public BrandRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Brand>> GetAllBrandsAsync(CancellationToken cancellationToken)
        {
            return await _context.Brands.ToListAsync(cancellationToken);
        }
    }
}
