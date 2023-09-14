using SFC.Players.Application.Common.Mappings.Converters;
using SFC.Players.Domain.Entities;
using SFC.Players.Domain.Enums;

namespace SFC.Players.Application.UnitTests.Common.Mappings.Converters;
public class Base64StringTypeConverterTests
{
    [Fact]
    [Trait("Mapping", "Base64StringTypeConverter")]
    public void Mapping_Base64StringTypeConverter_ShouldConvert()
    {
        // Arrange
        PlayerPhoto photo = new()
        {
            Extension = PhotoExtension.Jpg,
            Source = new byte[10]
        };
        Base64StringTypeConverter converter = new();

        // Act
        string? result = converter.Convert(photo, null, null!);

        // Assert
        Assert.Equal("data:image/jpg;base64,AAAAAAAAAAAAAA==", result);
    }

    [Fact]
    [Trait("Mapping", "Base64StringTypeConverter")]
    public void Mapping_Base64StringTypeConverter_ShouldReturnNullIfPhotoNotExist()
    {
        // Arrange
        PlayerPhoto? photo = null;
        Base64StringTypeConverter converter = new();

        // Act
        string? result = converter.Convert(photo, null, null!);

        // Assert
        Assert.Null(result);
    }
}
