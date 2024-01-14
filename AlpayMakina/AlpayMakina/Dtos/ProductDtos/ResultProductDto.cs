namespace AlpayMakina.Dtos.ProductDtos
{
    public class ResultProductDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public string Currency { get; set; }
        public string ImageUrl { get; set; }
        public string Category { get; set; }
        public int CategoryId { get; set; }
        public string SubCategory { get; set; }
        public int SubCategoryId { get; set; }
    }
}
