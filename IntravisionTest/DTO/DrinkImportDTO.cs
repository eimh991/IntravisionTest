namespace IntravisionTest.DTO
{
    public class DrinkImportDTO
    {
        public string DrinkName { get; set; } = string.Empty;
        public int BrandId { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
    }
}
