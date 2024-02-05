namespace AlpayMakina.Dtos.ProductDetailDtos
{
	public class ResultProductDetailDto
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public decimal Price { get; set; }
		public string Currency { get; set; }
		public string ImageUrl { get; set; }
		public string CategoryName { get; set; }
		public int CategoryId { get; set; }
		public string SubCategoryName { get; set; }
		public int SubCategoryId { get; set; }
        public string Description { get; set; }

    }
}
