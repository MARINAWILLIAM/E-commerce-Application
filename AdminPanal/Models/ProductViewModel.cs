using DomainLayer.Entity;
using System.ComponentModel.DataAnnotations;

namespace AdminPanal.Models
{
    public class ProductViewModel
    {
		public int Id { get; set; }

		[Required(ErrorMessage = "Name is Required")]
		public string Name { get; set; }

		[Required(ErrorMessage = "Description is Required")]
		public string Description { get; set; }

		public IFormFile Image { get; set; }

		public string? PictureUrl { get; set; }

		[Required(ErrorMessage = "Price is Required")]
		[Range(1, 100000)]
		public decimal Price { get; set; }

		[Required(ErrorMessage = "ProductTypeId is Required")]
		public int ProductTypeId { get; set; }
		public ProductTypeTable? producttype { get; set; }

		[Required(ErrorMessage = "ProductBrandId is Required")]

		public int productBrandId { get; set; }
		public ProductBrandTable? productBrand { get; set; }

	}
}
