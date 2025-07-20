using IntravisionTest.Interface;
using IntravisionTest.Interfaces;
using IntravisionTest.Model;
using System.Globalization;

namespace IntravisionTest.Service
{
    public class ImportService : IImportService
    {
        private readonly IDrinkRepository _drinkRepository;

        public ImportService(IDrinkRepository drinkRepository)
        {
            _drinkRepository = drinkRepository;
        }

        public async Task<int> ImportDrinksAsync(IFormFile file, CancellationToken cancellationToken)
        {
            var drinks = new List<Drink>();

            using var reader = new StreamReader(file.OpenReadStream());
            string? line;
            bool isHeader = true;

            while ((line = await reader.ReadLineAsync()) != null)
            {
                if (isHeader)
                {
                    isHeader = false;
                    continue;
                }

                var parts = line.Split(',');

                if (parts.Length != 4) continue;

                if (!int.TryParse(parts[1], out int brandId)) continue;
                if (!decimal.TryParse(parts[2], NumberStyles.Any, CultureInfo.InvariantCulture, out decimal price)) continue;
                if (!int.TryParse(parts[3], out int stock)) continue;

                drinks.Add(new Drink
                {
                    DrinkName = parts[0],
                    BrandId = brandId,
                    Price = price,
                    Stock = stock
                });
            }

            await _drinkRepository.AddRangeAsync(drinks, cancellationToken);
            return drinks.Count;
        }
    }
}
