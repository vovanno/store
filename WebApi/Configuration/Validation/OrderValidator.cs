using FluentValidation;
using WebApi.VIewDto;

namespace WebApi.Configuration.Validation
{
    public class OrderValidator : AbstractValidator<OrderViewDto>
    {
        public OrderValidator()
        {
            RuleFor(p => p.OrderId).InclusiveBetween(0, int.MaxValue);
            RuleFor(p => p.OrderDate).NotEmpty();
            RuleFor(p => p.Status).NotEmpty().MaximumLength(50);
        }
    }
}
