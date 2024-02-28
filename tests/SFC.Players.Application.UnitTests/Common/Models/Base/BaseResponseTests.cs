﻿using System.Text.Json;

using Newtonsoft.Json.Linq;

using SFC.Players.Application.Common.Constants;
using SFC.Players.Application.Models.Base;

namespace SFC.Players.Application.UnitTests.Common.Models.Base;
public class BaseResponseTests
{
    [Fact]
    [Trait("Model", "BaseResponse")]
    public void Model_BaseResponse_ShouldHaveDefaultValues()
    {
        // Arrange
        BaseResponse response = new();

        // Assert
        Assert.Equal(Messages.SuccessResult, response.Message);
        Assert.True(response.Success);
    }

    [Fact]
    [Trait("Model", "BaseResponse")]
    public void Model_BaseResponse_ShouldHaveDefinedMessage()
    {
        // Arrange
        string message = "Test message";
        BaseResponse response = new(message);

        // Assert
        Assert.Equal(message, response.Message);
        Assert.True(response.Success);
    }

    [Fact]
    [Trait("Model", "BaseResponse")]
    public void Model_BaseResponse_ShouldHaveDefinedMessageAndSuccessValues()
    {
        // Arrange
        string message = "Test message";
        BaseResponse response = new(message, false);

        // Assert
        Assert.Equal(message, response.Message);
        Assert.False(response.Success);
    }

    [Fact]
    [Trait("Model", "BaseResponse")]
    public void Model_BaseResponse_ShouldHaveCorrectPropertiesOrder()
    {
        // Arrange
        BaseResponse response = new();

        // Act
        string serializedBaseResponse = JsonSerializer.Serialize(response);
        JObject jsonObj = JObject.Parse(serializedBaseResponse);
        JProperty[] properties = jsonObj.Properties().ToArray();

        // Assert

        Assert.Equal(2, properties.Count());
        Assert.Equal(nameof(BaseResponse.Success), properties[0].Name);
        Assert.Equal(nameof(BaseResponse.Message), properties[1].Name);
    }
}
