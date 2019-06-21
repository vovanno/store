using FluentValidation;
using WebApi.VIewDto;

namespace WebApi.Configuration.Validation
{
    public class PublisherValidator : AbstractValidator<PublisherViewDto>
    {
        public PublisherValidator()
        {
            RuleFor(p => p.PublisherId).InclusiveBetween(0, int.MaxValue);
            RuleFor(p => p.Name).NotEmpty().MaximumLength(30);
        }
    }
}
