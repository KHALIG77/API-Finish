using System.ComponentModel.DataAnnotations;

namespace Shop.UI.Models
{
	public class ProductCreateRequest
	{
		
		public string Name {get;set;}
		public decimal SalePrice { get;set;}	
		public decimal CostPrice {get;set;}
		public decimal DiscountPercent {get;set;}
		public int BrandId {get;set;}
		[Required]
		public IFormFile ImageFile {get;set;}

	}
}
