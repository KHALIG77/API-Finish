using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Shop.Services.DTOs.ProductDTOs
{
    
    public class ProductPostDTO
    {
        
        public int BrandId { get; set; }
        public string Name { get; set; }
        public decimal SalePrice { get; set; }
        public decimal CostPrice { get; set; }
        public IFormFile ImageFile {get; set;}
        public decimal DiscountPercent { get; set; }
       
       
    }
    public class ProductPostDTOValidation:AbstractValidator<ProductPostDTO>

    {
        public ProductPostDTOValidation()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(20).MinimumLength(5);
            RuleFor(x => x.SalePrice).GreaterThanOrEqualTo(x => x.CostPrice).NotEmpty();
            RuleFor(x => x.CostPrice).GreaterThanOrEqualTo(0).NotEmpty();
            RuleFor(x => x.ImageFile).NotEmpty();
            RuleFor(x => x).Custom((x, context) =>
            {
                if (x.DiscountPercent > 0)
                {
                    var price = x.SalePrice * (100 - x.DiscountPercent) / 100;
                    if(x.CostPrice> price)
                    {
                        context.AddFailure(nameof(x.DiscountPercent), "DiscountPercent incorrect");
                    }

                }
                if(x.ImageFile!= null)
                {
                    if (x.ImageFile.Length > 2097152)
                        context.AddFailure(nameof(x.ImageFile), "ImageFile must be less or equal than 2MB");

                    if (x.ImageFile.ContentType != "image/jpeg" && x.ImageFile.ContentType != "image/png")
                        context.AddFailure(nameof(x.ImageFile), "ImageFile must be image/jpeg or image/png");
                }
               
            });

        }

    }
}
