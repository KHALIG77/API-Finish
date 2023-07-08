using System.ComponentModel.DataAnnotations;
using FluentValidation;

namespace Shop.Services.DTOs.BrandDTOs
{
    public class BrandPostDTO
    {

        public string Name { get; set; }
    }
    public class BrandPostDTOValidation : AbstractValidator<BrandPostDTO>
    {
        public BrandPostDTOValidation()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Please fill cell").MaximumLength(20).WithMessage("Maxlength will be 20");
        }
    }
}
