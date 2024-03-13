using System.Text.Json.Serialization;

using SFC.Player.Application.Common.Constants;

namespace SFC.Player.Application.Models.Base;

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

    [JsonPropertyOrder(0)]
    public bool Success { get; }

    [JsonPropertyOrder(1)]
    public string Message { get; }
}
