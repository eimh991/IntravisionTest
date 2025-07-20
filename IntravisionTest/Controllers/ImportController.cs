using IntravisionTest.Interface;
using Microsoft.AspNetCore.Mvc;

namespace IntravisionTest.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ImportController : ControllerBase
    {
        private readonly IImportService _importService;

        public ImportController(IImportService importService)
        {
            _importService = importService;
        }

        [HttpPost("drinks")]
        public async Task<IActionResult> ImportDrinks([FromForm] IFormFile file, CancellationToken cancellationToken)
        {
            if (file == null || file.Length == 0)
                return BadRequest("Файл не выбран");

            var count = await _importService.ImportDrinksAsync(file, cancellationToken);
            return Ok($"{count} напитков импортировано");
        }
    }
}
