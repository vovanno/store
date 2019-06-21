using CrossCuttingFunctionality.FilterModels;
using FluentValidation;
using WebApi.VIewDto;

namespace WebApi.Configuration.Validation
{
    public class FilterModelValidation : AbstractValidator<FilterModel>
    {
        public FilterModelValidation()
        {
            //RuleFor(p=>p.ByPriceAscending).
        }
    }
}
