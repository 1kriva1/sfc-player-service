using FluentValidation;

namespace SFC.Player.Application.Common.Extensions;
public static class ValidationExtensions
{
    public static IRuleBuilderOptions<C, string> RequiredProperty<C>(this IRuleBuilderInitial<C, string> builder, 
        int? maximumLength = null, string? propertyName = null)
    {
        IRuleBuilderOptions<C, string> options = builder.NotEmpty();

        if (maximumLength.HasValue)
        {
            options.MaximumLength(maximumLength.Value);
        }

        if (!string.IsNullOrEmpty(propertyName))
        {
            options.WithName(propertyName);
        }

        return options;
    }
}
