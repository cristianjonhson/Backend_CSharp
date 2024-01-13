using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Backend.Models
{
    public class Beer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BeerId { get; set; }

        public string BeerName { get; set;}
        public string BeerDescription { get; set;}
        
        [ForeignKey("BrandId")]
        public virtual Brand Brand { get; set; }
    
    }
}
