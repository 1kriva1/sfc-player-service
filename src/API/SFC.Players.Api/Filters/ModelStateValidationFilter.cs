﻿using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using SFC.Players.Application.Common.Models;
using SFC.Players.Application.Common.Constants;

namespace SFC.Players.Api.Filters;

public class ValidationFilterAttribute : ActionFilterAttribute
{
    public override Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
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
                            Messages.RequestBodyRequired
                        }
                    }
                };

                result = new BaseErrorResponse(Messages.ValidationError, emptyBodyError);
            }
            else
            {
                result = new(Messages.ValidationError, context.ModelState.ToDictionary(
                    state => state.Key,
                    state => state.Value?.Errors.Select(e => e.ErrorMessage) ?? Array.Empty<string>())
               );
            }

            context.Result = new BadRequestObjectResult(result);
        }

        return base.OnActionExecutionAsync(context, next);
    }
}
