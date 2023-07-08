using FluentValidation;

namespace Shop.Api.DTOs.UsersDTOs
{
    public class UserLoginDTO
    {
        public string Email {get; set;}
        public string Password { get; set;} 
    }
    public class UserLoginDTOValidation:AbstractValidator<UserLoginDTO>
    {
        public UserLoginDTOValidation()
        {
            RuleFor(x => x.Password).NotEmpty().MinimumLength(8);
            RuleFor(x => x.Email).Matches(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$").WithMessage("Email is incorrect format");
        }
    }
}
