using IntravisionTest.DTO;
using IntravisionTest.Interfaces;
using IntravisionTest.Model;
using Microsoft.AspNetCore.Mvc;

namespace IntravisionTest.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DrinkController : ControllerBase
    {
        private readonly IDrinkService _drinkService;

        public DrinkController(IDrinkService drinkService)
        {
            _drinkService = drinkService;
        }

        [HttpGet("catalog")]
        public async Task<ActionResult<IEnumerable<DrinkResponceDTO>>> GetCatalog(
            [FromQuery] int brandId,
            CancellationToken cancellationToken,
            [FromQuery] decimal minPrice = 0,
            [FromQuery] decimal maxPrice = 110)
        {
            var drinks = await _drinkService.GetFilteredDrinksAsync(brandId, minPrice, maxPrice,cancellationToken);
            return Ok(drinks);
        }
    }
}
