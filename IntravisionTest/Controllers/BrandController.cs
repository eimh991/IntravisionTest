using IntravisionTest.DTO;
using IntravisionTest.Interface;
using Microsoft.AspNetCore.Mvc;

namespace IntravisionTest.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BrandController : ControllerBase
    {
        private readonly IBrandService _brandService;

        public BrandController(IBrandService brandService)
        {
            _brandService = brandService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BrandDTO>>> GetAllBrands(CancellationToken cancellationToken)
        {
            var brands = await _brandService.GetAllBrandsAsync(cancellationToken);
            return Ok(brands);
        }
    }
}
