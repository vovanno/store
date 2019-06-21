using FluentValidation;
using WebApi.VIewDto;

namespace WebApi.Configuration.Validation
{
    public class CommentValidator : AbstractValidator<CommentViewDto>
    {
        public CommentValidator()
        {
            RuleFor(p => p.CommentId).InclusiveBetween(0, int.MaxValue);
            RuleFor(p => p.Name).NotEmpty().MaximumLength(30);
            RuleFor(p => p.Body).NotEmpty().MaximumLength(300);
            RuleFor(p => p.ParentId).InclusiveBetween(0, int.MaxValue);
            RuleFor(p => p.GameId).NotEmpty().InclusiveBetween(1, int.MaxValue);
        }
    }
}
