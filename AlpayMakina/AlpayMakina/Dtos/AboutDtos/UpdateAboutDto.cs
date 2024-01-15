namespace AlpayMakina.Dtos.AboutDtos
{
	public class UpdateAboutDto
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public TimeSpan HTime { get; set; }
		public DateTime HDate { get; set; }
		public string ImageUrl { get; set; }
		public string Description { get; set; }
        public IFormFile ImageFile { get; set; }
    }
}
