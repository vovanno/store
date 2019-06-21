using FluentValidation;
using WebApi.VIewDto;

namespace WebApi.Configuration.Validation
{
    public class GenreValidator : AbstractValidator<GenreViewDto>
    {
        public GenreValidator()
        {
            RuleFor(p => p.GenreId).NotEmpty().InclusiveBetween(0, int.MaxValue);
            RuleFor(p => p.Name).NotEmpty().MaximumLength(30);
        }
    }
}
