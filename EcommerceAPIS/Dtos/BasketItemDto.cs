using System.ComponentModel.DataAnnotations;

namespace EcommerceAPIS.Dtos
{
    public class BasketItemDto
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string productName { get; set; }
        [Required]
        public string PictureUrl { get; set; }
        [Required]
        [Range(0.5, double.MaxValue, ErrorMessage = "Price Must Be one Item At Least 0.5")]//fraction
        public decimal Price { get; set; }
        [Required]
        [Range(1, int.MaxValue,ErrorMessage ="Quantity Must Be one Item At Least one")]
        public int Quantity { get; set; }
        [Required]
        public string Brand { get; set; }
        [Required]
        public string Type { get; set; }
     
    }
}