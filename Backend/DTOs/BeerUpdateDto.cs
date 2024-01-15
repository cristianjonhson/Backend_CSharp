namespace Backend.DTOs
{
    public class BeerUpdateDto
    {
        public string BeerName { get; set; }

        public string BeerDescription { get; set; }

        public string BeerType { get; set; }

        public decimal Alcohol { get; set; }
        public Int64 BrandId { get; set; }
    }
}
