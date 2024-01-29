using System.ComponentModel.DataAnnotations;

namespace EcommerceAPIS.Dtos
{
    public class registerDto
    {
        [Required]
        public string DisplayName { get; set; }
        [Required]
        [EmailAddress]
        public string email { get; set; }
        [Phone] 
        
        public string? phoneNumber { get; set; }
        [Required]
       
        public string Password { get; set; }
    }
}
