using FluentValidation;

namespace Shop.Services.DTOs.ProductDTOs
{
    public class ProductGetAllItemDTO
    {
        public int Id { get; set; }
        public int BrandId { get; set; }
        public string Name { get; set; }
        public decimal SalePrice { get; set; }
        public string BrandName { get; set; }
        public string ImageUrl {get; set; }
        public bool HasDiscount {get; set; }
    }
}
