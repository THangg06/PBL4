namespace WebMVC.Models
{
    public class Product
    {
        public int ProductID { get; set; } // Primary Key
        public string? ProductName { get; set; }
        public string? CatName { get; set; }
        public int? CatID { get; set; } // Foreign Key to Categories
        public decimal? Price { get; set; }
        public string? Thumb { get; set; }
         public Category? Category { get; set; }
    }
}
