﻿namespace AlpayMakina.Dtos.SliderDtos
{
    public class UpdateSliderDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public string PriceUrl { get; set; }
        public string Active { get; set; }
    }
}