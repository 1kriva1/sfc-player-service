using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;

using SFC.Players.Api.Filters;
using SFC.Players.Application.Common.Constants;
using SFC.Players.Application.Common.Models;

namespace SFC.Players.Api.UnitTests.Filters;
public class ModelStateValidationFilterTests
{
    [Fact]
    [Trait("API", "Filter")]
    public async Task API_Filter_Validation_ShouldProcessRequestWithValidModelState()
    {
        // Arrange 
        ValidationFilterAttribute filter = new();

        DefaultHttpContext httpContext = new();

        ActionContext actionContext = new(httpContext, new(), new(), new());

        ActionExecutingContext actionExecutingContext = new(actionContext,
            new List<IFilterMetadata>(),
            new Dictionary<string, object?>(), null!);

        ActionExecutedContext actionExecutedContext = new(actionContext, new List<IFilterMetadata>(), null!);

        Task<ActionExecutedContext> Next() => Task.FromResult(actionExecutedContext);

        // Act
        await filter.OnActionExecutionAsync(actionExecutingContext, Next);

        // Assert
        Assert.Null(actionExecutingContext.Result);
    }

    [Fact]
    [Trait("API", "Filter")]
    public async Task API_Filter_Validation_ShouldReturnBadRequestResultWithInValidModelState()
    {
        // Arrange 
        string errorCode = "test_code";

        // Act
        ActionExecutingContext context = await ExecuteActionFilterAsync(errorCode);

        // Assert
        BadRequestObjectResult result = Assert.IsType<BadRequestObjectResult>(context.Result);
        Assert.NotNull(result.Value);
        Assert.IsType<BaseErrorResponse>(result.Value);
        BaseErrorResponse? response = result.Value as BaseErrorResponse;
        Assert.False(response?.Success);
        Assert.Equal(Messages.ValidationError, response?.Message);
        Assert.True(response!.Errors!.ContainsKey(errorCode));
        Assert.Equal("test_message", response.Errors[errorCode].First());
    }

    [Fact]
    [Trait("API", "Filter")]
    public async Task API_Filter_Validation_ShouldReturnBadRequestResultForEmptyBody()
    {
        // Act
        ActionExecutingContext context = await ExecuteActionFilterAsync(string.Empty);

        // Assert
        BadRequestObjectResult result = Assert.IsType<BadRequestObjectResult>(context.Result);
        Assert.NotNull(result.Value);
        Assert.IsType<BaseErrorResponse>(result.Value);
        BaseErrorResponse? response = result.Value as BaseErrorResponse;
        Assert.False(response?.Success);
        Assert.Equal(Messages.ValidationError, response?.Message);
        Assert.True(response!.Errors!.ContainsKey("Body"));
        Assert.Equal("Request body is required.", response.Errors["Body"].First());
    }

    private async Task<ActionExecutingContext> ExecuteActionFilterAsync(string errorCode)
    {
        // Arrange 
        ValidationFilterAttribute filter = new();

        DefaultHttpContext httpContext = new();

        string errorMessage = "test_message";
        ModelStateDictionary modelState = new();
        modelState.AddModelError(errorCode, errorMessage);

        ActionContext actionContext = new(httpContext, new(), new(), modelState);

        List<IFilterMetadata> filterMetadata = new();

        ActionExecutingContext actionExecutingContext = new(actionContext,
            filterMetadata,
            new Dictionary<string, object?>(), null!);

        ActionExecutedContext actionExecutedContext = new(actionContext, filterMetadata, null!);

        Task<ActionExecutedContext> Next() => Task.FromResult(actionExecutedContext);

        // Act
        await filter.OnActionExecutionAsync(actionExecutingContext, Next);

        return actionExecutingContext;
    }
}
