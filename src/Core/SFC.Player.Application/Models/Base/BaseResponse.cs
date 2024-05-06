using System.Text.Json.Serialization;

using SFC.Player.Application.Common.Constants;

namespace SFC.Player.Application.Models.Base;

/// <summary>
/// **Base** response model.
/// </summary>
[JsonDerivedType(typeof(BaseErrorResponse))]
public class BaseResponse
{
    public BaseResponse()
    {
        Success = true;
        Message = Messages.SuccessResult;
    }
    public BaseResponse(string message)
    {
        Success = true;
        Message = message;
    }

    [JsonConstructor]
    public BaseResponse(string message, bool success)
    {
        Success = success;
        Message = message;
    }

    /// <summary>
    /// Determined if response has **success** result.
    /// </summary>
    [JsonPropertyOrder(0)]
    public bool Success { get; }

    /// <summary>
    /// Describe response **result**.
    /// </summary>
    [JsonPropertyOrder(1)]
    public string Message { get; }
}
