using FluentValidation;
using FluentValidation.AspNetCore;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace WebApi.Configuration.Validation
{
    public class CustomValidationInterceptor : IValidatorInterceptor
    {
        public ValidationContext BeforeMvcValidation(ControllerContext controllerContext, ValidationContext validationContext)
        {
            Log.Information("Request from user with {0} IP address.",controllerContext.HttpContext.Connection.RemoteIpAddress.MapToIPv4());
            return validationContext;
        }

        public ValidationResult AfterMvcValidation(ControllerContext controllerContext, ValidationContext validationContext,
            ValidationResult result)
        {
            if (result.Errors.Count > 0)
            {
                Log.Information("Validation error occured in {0} during model binding.",validationContext.InstanceToValidate.GetType());
            }
            return result;
        }
    }
}
