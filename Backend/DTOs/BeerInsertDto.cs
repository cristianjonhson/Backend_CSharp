﻿namespace Backend.DTOs
{
    public class BeerInsertDto
    {
        public string BeerName { get; set; }

        public string BeerDescription { get; set; }

        public string BeerType { get; set; }

        public decimal Alcohol { get; set; }
        public int BrandId { get; set; }
    }
}
