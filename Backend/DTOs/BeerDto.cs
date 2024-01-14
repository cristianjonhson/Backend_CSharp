using Backend.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.DTOs
{
    public class BeerDto
    {
        public int BeerId { get; set; }
        public string BeerName { get; set; }

        public string BeerDescription { get; set; }

        public string BeerType { get; set; }

        public decimal Alcohol { get; set; }
        public int BrandId { get; set; }
    }
}
