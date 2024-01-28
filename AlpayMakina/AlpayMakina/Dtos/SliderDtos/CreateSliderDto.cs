namespace AlpayMakina.Dtos.SliderDtos
{
    public class CreateSliderDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public IFormFile ProductImageFile { get; set; }
        public string PriceUrl { get; set; }
        public IFormFile PriceImageFile { get; set; }
        public string Active { get; set; }
    }
}
