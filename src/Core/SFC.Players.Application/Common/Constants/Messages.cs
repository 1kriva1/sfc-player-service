using System.Text;

using Microsoft.Extensions.Localization;

namespace SFC.Players.Application.Common.Constants;

public class Messages
{
    private static IStringLocalizer<Resources> _localizer = default!;

    public Messages(IStringLocalizer<Resources> localizer)
    {
        _localizer ??= localizer;
    }

    public static void Configure(IStringLocalizer<Resources> localizer)
    {
        _localizer = localizer;
    }

    public static string SuccessResult =>
                    GetValue(_localizer?.GetString("SuccessResult"),
                        "Success result.")!;

    public static string FailedResult =>
                       GetValue(_localizer?.GetString("FailedResult"),
                           "Failed result.")!;

    public static string ValidationError =>
                    GetValue(_localizer?.GetString("ValidationError"),
                        "Validation error.")!;

    public static string RequestBodyRequired =>
                        GetValue(_localizer?.GetString("RequestBodyRequired"),
                            "Request body is required.")!;

    public static string AuthorizationError =>
                    GetValue(_localizer?.GetString("AuthorizationError"),
                        "Authorization error.")!;

    public static string PlayerNotFound =>
                       GetValue(_localizer?.GetString("PlayerNotFound"),
                           "Player not found.")!;

    public static string InvalidPhotoExtension =>
                      GetValue(_localizer?.GetString("InvalidPhotoExtension"),
                          "Invalid photo extension.")!;

    public static string InvalidBirthdayMaxValue =>
                      GetValue(_localizer?.GetString("InvalidBirthdayMaxValue"),
                          "'{{PropertyName}}' must be more than {0} years ago.")!;

    public static string InvalidBirthdayMinValue =>
                      GetValue(_localizer?.GetString("InvalidBirthdayMinValue"),
                          "'{PropertyName}' must be less than today's date.")!;

    public static string TagsUniqueness =>
                      GetValue(_localizer?.GetString("TagsUniqueness"),
                          "Each value from '{PropertyName}' must be unique.")!;

    public static string InvalidTagsSize =>
                      GetValue(_localizer?.GetString("InvalidTagsSize"),
                          "The length of '{0}' must be less or equal to {1}.")!;

    public static string TagEmpty =>
                      GetValue(_localizer?.GetString("TagEmpty"),
                          "Each value from '{PropertyName}' must not be empty.")!;

    public static string TagMaxLength =>
                      GetValue(_localizer?.GetString("TagMaxLength"),
                          "Each value from '{PropertyName}' must be {MaxLength} characters or fewer. You entered {TotalLength} characters.")!;

    public static string AvailableDaysSize =>
                      GetValue(_localizer?.GetString("AvailableDaysSize"),
                          "The length of '{0}' must be less or equal to {1}.")!;

    public static string InvalidAvailableDay =>
                      GetValue(_localizer?.GetString("InvalidAvailableDay"),
                          "Each value from '{PropertyName}' must be in Days of Week range.")!;

    public static string MustBeGreaterThan =>
                     GetValue(_localizer?.GetString("MustBeGreaterThan"),
                         "'{0}' value must be greater than {1} value.")!;

    public static string MustBeLessThan =>
                     GetValue(_localizer?.GetString("MustBeLessThan"),
                         "'{0}' value must be less than {1} value.")!;

    public static string MustBeNotEqual =>
                     GetValue(_localizer?.GetString("MustBeNotEqual"),
                         "'{{PropertyName}}' must not to be equal to '{0}'.")!;

    public static string LengthMustBeEqual =>
                     GetValue(_localizer?.GetString("LengthMustBeEqual"),
                         "The length of '{0}' must be equal to {1}.")!;

    public static string MustBeInCategoryRange =>
                     GetValue(_localizer?.GetString("MustBeInCategoryRange"),
                         "Each value from '{0}' must have {1} in Stat Category range.")!;

    public static string MustBeInStatTypeRange =>
                     GetValue(_localizer?.GetString("MustBeInStatTypeRange"),
                         "Each value from '{0}' must have {1} in Stat Type range.")!;

    public static string EachStatTypeMustBeInSpecificCategory =>
                     GetValue(_localizer?.GetString("EachStatTypeMustBeInSpecificCategory"),
                         "Each value from '{0}' must have {1} for specific {2}.")!;

    public static string StatValueMustBeInRange =>
                     GetValue(_localizer?.GetString("StatValueMustBeInRange"),
                         "Each value from '{0}' must have {1} between {2} and {3}.")!;

    public static string PlayerAlreadyCreatedForThisUser =>
                     GetValue(_localizer?.GetString("PlayerAlreadyCreatedForThisUser"),
                         "Player already created for this user.")!;

    public static string PlayerNotRelatedToThisUser =>
                     GetValue(_localizer?.GetString("PlayerNotRelatedToThisUser"),
                         "Player not related for this user.")!;

    private static string GetValue(LocalizedString? @string, string defaultValue)
    {
        return @string == null
            ? defaultValue
            : @string.ResourceNotFound
            ? defaultValue
            : @string.Value;
    }
}
