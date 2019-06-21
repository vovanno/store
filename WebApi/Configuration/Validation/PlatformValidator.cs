using FluentValidation;
using WebApi.VIewDto;

namespace WebApi.Configuration.Validation
{
    public class PlatformValidator : AbstractValidator<PlatformViewDto>
    {
        public PlatformValidator()
        {
            RuleFor(p => p.PlatformTypeId).InclusiveBetween(0, int.MaxValue);
            RuleFor(p => p.Type).NotEmpty().MaximumLength(30);
        }
    }
}
