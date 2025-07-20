using IntravisionTest.DTO;

namespace IntravisionTest.Model
{
    public class Brand
    {
        public int BrandId { get; set; }
        public string BrandName { get; set; } = string.Empty;

        public List<Drink> Drinks { get; set; } = new List<Drink>();
    }
}
