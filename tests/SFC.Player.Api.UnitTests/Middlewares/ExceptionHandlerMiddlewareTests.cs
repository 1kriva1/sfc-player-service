using System.Net;
using System.Text.Json;

using Microsoft.AspNetCore.Http;

using SFC.Player.Api.Middlewares;
using SFC.Player.Application.Common.Constants;
using SFC.Player.Application.Common.Exceptions;
using SFC.Player.Application.Models.Base;

namespace SFC.Player.Api.UnitTests.Middlewares;
public class ExceptionHandlerMiddlewareTests
{
    [Fact]
    [Trait("API", "Middleware")]
    public async Task API_Middleware_Exception_ShouldHaveDefaultContentType()
    {
        // Arrange
        DefaultHttpContext httpContext = new();

        static Task Next(HttpContext httpContext) => Task.FromException<SystemException>(new SystemException("Test_error"));

        ExceptionHandlerMiddleware middleware = new(Next);

        // Act
        await middleware.InvokeAsync(httpContext);

        // Assert
        Assert.Equal(CommonConstants.CONTENT_TYPE, httpContext.Response.ContentType);
    }

    [Fact]
    [Trait("API", "Middleware")]
    public async Task API_Middleware_Exception_ShouldHaveDefinedContentType()
    {
        // Arrange
        string customContentType = "application/xml";
        DefaultHttpContext httpContext = new();
        httpContext.Request.ContentType = customContentType;

        static Task Next(HttpContext httpContext) => Task.FromException<SystemException>(new SystemException("Test_error"));

        ExceptionHandlerMiddleware middleware = new(Next);

        // Act
        await middleware.InvokeAsync(httpContext);

        // Assert
        Assert.Equal(customContentType, httpContext.Response.ContentType);
    }

    [Fact]
    [Trait("API", "Middleware")]
    public async Task API_Middleware_Exception_ShouldProcessWithoutException()
    {
        // Arrange
        DefaultHttpContext httpContext = new();

        static Task Next(HttpContext httpContext) => Task.CompletedTask;

        ExceptionHandlerMiddleware middleware = new(Next);

        // Act
        await middleware.InvokeAsync(httpContext);

        // Assert
        Assert.Equal((int)HttpStatusCode.OK, httpContext.Response.StatusCode);
    }

    [Fact]
    [Trait("API", "Middleware")]
    public async Task API_Middleware_Exception_ShouldReturnInternalServerErrorIfHandlerNotProvided()
    {
        // Arrange
        DefaultHttpContext httpContext = new();
        httpContext.Response.Body = new MemoryStream();

        static Task Next(HttpContext httpContext) => Task.FromException<SystemException>(new SystemException("test_error"));

        ExceptionHandlerMiddleware middleware = new(Next);

        // Act
        await middleware.InvokeAsync(httpContext);

        // Assert
        AssertBaseResponse(HttpStatusCode.InternalServerError, httpContext.Response, "Failed result.", out string _);
    }

    [Fact]
    [Trait("API", "Middleware")]
    public async Task API_Middleware_Exception_ShouldReturnBadRequest()
    {
        // Arrange
        DefaultHttpContext httpContext = new();
        httpContext.Response.Body = new MemoryStream();

        string errorMessage = "bad_request_error", errorCode = "test_code", errorDescription = "test_message";

        Task Next(HttpContext httpContext) => Task.FromException<BadRequestException>(new BadRequestException(errorMessage, (errorCode, errorDescription)));

        ExceptionHandlerMiddleware middleware = new(Next);

        // Act
        await middleware.InvokeAsync(httpContext);

        // Assert
        AssertBaseErrorResponse(HttpStatusCode.BadRequest, httpContext.Response, errorMessage, errorCode, errorDescription);
    }

    [Fact]
    [Trait("API", "Middleware")]
    public async Task API_Middleware_Exception_ShouldReturnNotFound()
    {
        // Arrange
        DefaultHttpContext httpContext = new();
        httpContext.Response.Body = new MemoryStream();

        string errorMessage = "not_found_error";

        Task Next(HttpContext httpContext) => Task.FromException<NotFoundException>(new NotFoundException(errorMessage));

        ExceptionHandlerMiddleware middleware = new(Next);

        // Act
        await middleware.InvokeAsync(httpContext);

        // Assert
        AssertBaseResponse(HttpStatusCode.NotFound, httpContext.Response, errorMessage, out string _);
    }

    [Fact]
    [Trait("API", "Middleware")]
    public async Task API_Middleware_Exception_ShouldReturnUnauthorized()
    {
        // Arrange
        DefaultHttpContext httpContext = new();
        httpContext.Response.Body = new MemoryStream();

        string errorMessage = "authorization_error";

        Task Next(HttpContext httpContext) => Task.FromException<AuthorizationException>(new AuthorizationException(errorMessage));

        ExceptionHandlerMiddleware middleware = new(Next);

        // Act
        await middleware.InvokeAsync(httpContext);

        // Assert
        AssertBaseResponse(HttpStatusCode.Unauthorized, httpContext.Response, errorMessage, out string _);
    }

    private void AssertBaseResponse(HttpStatusCode statusCode, HttpResponse httpResponse, string errorMessage, out string responseBody)
    {
        Assert.Equal((int)statusCode, httpResponse.StatusCode);

        httpResponse.Body.Seek(0, SeekOrigin.Begin);

        responseBody = new StreamReader(httpResponse.Body).ReadToEnd();

        Assert.False(string.IsNullOrEmpty(responseBody));

        BaseResponse? response = JsonSerializer.Deserialize<BaseResponse>(responseBody);

        Assert.NotNull(response);
        Assert.Equal(errorMessage, response.Message);
        Assert.False(response.Success);
    }

    private void AssertBaseErrorResponse(HttpStatusCode statusCode, HttpResponse httpResponse, string errorMessage,
        string errorCode, string errorDescription)
    {
        AssertBaseResponse(statusCode, httpResponse, errorMessage, out string responseBody);

        BaseErrorResponse? response = JsonSerializer.Deserialize<BaseErrorResponse>(responseBody);

        Assert.True(response!.Errors!.ContainsKey(errorCode));
        Assert.Equal(errorDescription, response.Errors[errorCode].First());
    }
}
