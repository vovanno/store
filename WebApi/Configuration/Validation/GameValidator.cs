using FluentValidation;
using WebApi.VIewDto;

namespace WebApi.Configuration.Validation
{
    public class GameValidator : AbstractValidator<GameViewDto>
    {
        public GameValidator()
        {
            RuleFor(p=>p.GameId).InclusiveBetween(0, int.MaxValue);
            RuleFor(p => p.Name).NotEmpty().MaximumLength(50).WithMessage("Name should be up to 50 symbols.");
            RuleFor(p => p.Description).MaximumLength(500).WithMessage("Description should be up to 500 symbols.");
            RuleFor(p => p.PublisherId).InclusiveBetween(0, int.MaxValue);
            RuleFor(p => p.AmountOfViews).InclusiveBetween(0, int.MaxValue);
        }
    }
}
