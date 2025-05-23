using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using SFC.Player.Application.Common.Constants;
using SFC.Player.Api.Infrastructure.Models.Base;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Localization = SFC.Player.Application.Common.Constants.Localization;

namespace SFC.Player.Api.Infrastructure.Filters;

public sealed class ValidationFilterAttribute : ActionFilterAttribute
{
    public override Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        ArgumentNullException.ThrowIfNull(context);

        if (!context.ModelState.IsValid)
        {
            BaseErrorResponse result;

            if (context.ModelState.Any(e => string.IsNullOrEmpty(e.Key)))
            {
                Dictionary<string, IEnumerable<string>> emptyBodyError = new()
                {
                    {
                        "Body",
                        new List<string> {
                            Localization.RequestBodyRequired
                        }
                    }
                };

                result = new BaseErrorResponse(Localization.ValidationError, emptyBodyError);
            }
            else
            {
                result = new(Localization.ValidationError, context.ModelState
                    .Where(state => state.Value?.ValidationState == ModelValidationState.Invalid)
                    .ToDictionary(
                        state => state.Key,
                        state => state.Value?.Errors.Select(e => e.ErrorMessage) ?? [])
                );
            }

            context.Result = new BadRequestObjectResult(result);
        }

        return base.OnActionExecutionAsync(context, next);
    }
}
