using System.Data;
using FluentValidation;

namespace Shop.Services.DTOs.BrandDTOs
{
    public class BrandPutDTO
    {
        public string Name { get; set; }

    }
    public class BrandPutDTOValidation : AbstractValidator<BrandPutDTO>
    {
        public BrandPutDTOValidation()
        {
            RuleFor(x => x.Name).MaximumLength(20).WithMessage("Maxlength will be 20").NotEmpty().WithMessage("Fill name cell");
        }
    }
}
