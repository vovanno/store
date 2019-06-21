using FluentValidation;
using WebApi.VIewDto;

namespace WebApi.Configuration.Validation
{
    public class UserValidator : AbstractValidator<UserViewDto>
    {
        public UserValidator()
        {
            RuleFor(p => p.Email).EmailAddress();
            RuleFor(p => p.UserName).NotEmpty().MaximumLength(50).WithMessage("Name should be up to 50 symbols.");
            //RuleFor(p => p.Password).MinimumLength(6).Matches("[A-Z]*\W|_") .WithMessage("Description should be up to 500 symbols.");
        }
    }
}
