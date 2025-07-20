namespace IntravisionTest.Model
{
    public class Drink
    {
        public int DrinkId { get; set; }
        public string DrinkName { get; set; }= string.Empty;
        public decimal Price { get; set; }
        public int Stock {  get; set; } 

        public Brand? Brand { get; set; }
        public int BrandId { get; set; }
    }
}
