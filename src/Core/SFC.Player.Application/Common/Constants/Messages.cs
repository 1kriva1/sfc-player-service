using System.Text;

using Microsoft.Extensions.Localization;

namespace SFC.Player.Application.Common.Constants;

public class Messages
{
    private static IStringLocalizer<Resources> s_localizer = default!;

    public Messages(IStringLocalizer<Resources> localizer)
    {
        s_localizer ??= localizer;
    }

    public static void Configure(IStringLocalizer<Resources> localizer)
    {
        s_localizer = localizer;
    }

    public static string SuccessResult =>
                    GetValue(s_localizer?.GetString("SuccessResult"),
                        "Success result.")!;

    public static string FailedResult =>
                       GetValue(s_localizer?.GetString("FailedResult"),
                           "Failed result.")!;

    public static string ValidationError =>
                    GetValue(s_localizer?.GetString("ValidationError"),
                        "Validation error.")!;

    public static string RequestBodyRequired =>
                        GetValue(s_localizer?.GetString("RequestBodyRequired"),
                            "Request body is required.")!;

    public static string AuthorizationError =>
                    GetValue(s_localizer?.GetString("AuthorizationError"),
                        "Authorization error.")!;

    public static string PlayerNotFound =>
                       GetValue(s_localizer?.GetString("PlayerNotFound"),
                           "Player not found.")!;

    public static string InvalidPhotoExtension =>
                      GetValue(s_localizer?.GetString("InvalidPhotoExtension"),
                          "Invalid photo extension.")!;

    public static string InvalidBirthdayMaxValue =>
                      GetValue(s_localizer?.GetString("InvalidBirthdayMaxValue"),
                          "'{{PropertyName}}' must be more than {0} years ago.")!;

    public static string InvalidBirthdayMinValue =>
                      GetValue(s_localizer?.GetString("InvalidBirthdayMinValue"),
                          "'{PropertyName}' must be less than today's date.")!;

    public static string TagsUniqueness =>
                      GetValue(s_localizer?.GetString("TagsUniqueness"),
                          "Each value from '{PropertyName}' must be unique.")!;

    public static string InvalidTagsSize =>
                      GetValue(s_localizer?.GetString("InvalidTagsSize"),
                          "The length of '{0}' must be less or equal to {1}.")!;

    public static string TagEmpty =>
                      GetValue(s_localizer?.GetString("TagEmpty"),
                          "Each value from '{PropertyName}' must not be empty.")!;

    public static string TagMaxLength =>
                      GetValue(s_localizer?.GetString("TagMaxLength"),
                          "Each value from '{PropertyName}' must be {MaxLength} characters or fewer. You entered {TotalLength} characters.")!;

    public static string AvailableDaysSize =>
                      GetValue(s_localizer?.GetString("AvailableDaysSize"),
                          "The length of '{0}' must be less or equal to {1}.")!;

    public static string InvalidAvailableDay =>
                      GetValue(s_localizer?.GetString("InvalidAvailableDay"),
                          "Each value from '{PropertyName}' must be in Days of Week range.")!;

    public static string MustBeGreaterThan =>
                     GetValue(s_localizer?.GetString("MustBeGreaterThan"),
                         "'{0}' value must be greater than {1} value.")!;

    public static string MustBeLessThan =>
                     GetValue(s_localizer?.GetString("MustBeLessThan"),
                         "'{0}' value must be less than {1} value.")!;

    public static string MustBeNotEqual =>
                     GetValue(s_localizer?.GetString("MustBeNotEqual"),
                         "'{{PropertyName}}' must not to be equal to '{0}'.")!;

    public static string MustBeInStatTypeRange =>
                     GetValue(s_localizer?.GetString("MustBeInStatTypeRange"),
                         "Each value from '{0}' must have {1} in Stat Type range.")!;

    public static string StatValueMustBeInRange =>
                     GetValue(s_localizer?.GetString("StatValueMustBeInRange"),
                         "Each value from '{0}' must have {1} between {2} and {3}.")!;

    public static string PlayerAlreadyCreatedForThisUser =>
                     GetValue(s_localizer?.GetString("PlayerAlreadyCreatedForThisUser"),
                         "Player already created for this user.")!;

    public static string PlayerNotRelatedToThisUser =>
                     GetValue(s_localizer?.GetString("PlayerNotRelatedToThisUser"),
                         "Player not related for this user.")!;

    public static string DataValidator =>
                     GetValue(s_localizer?.GetString("DataValidator"),
                         "'{PropertyName}' has a range of values which does not include '{PropertyValue}'.")!;

    public static string StatLength =>
                     GetValue(s_localizer?.GetString("StatLength"),
                         "Stat count is invalid.")!;

    private static string GetValue(LocalizedString? @string, string defaultValue)
    {
        return @string == null
            ? defaultValue
            : @string.ResourceNotFound
            ? defaultValue
            : @string.Value;
    }
}
