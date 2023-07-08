namespace Shop.Services.DTOs.ProductDTOs
{
    public class ProductGetDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal SalePrice { get; set; }
        public decimal DiscountPercent {get; set; }
        public decimal CostPrice {get; set; }
        public string ImageUrl {get; set; }
        public BrandInProductGetDTO Brand {get; set;}
    }
    public class BrandInProductGetDTO
    {
        public int BrandId { get; set; }
        public string Name { get; set; }
    }
}
