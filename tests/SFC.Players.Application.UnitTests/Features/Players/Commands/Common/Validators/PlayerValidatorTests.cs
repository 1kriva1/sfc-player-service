using System.Text.Json;

using FluentValidation.Results;

using Moq;

using SFC.Players.Application.Common.Constants;
using SFC.Players.Application.Features.Players.Commands.Common.Validators;
using SFC.Players.Application.Interfaces.Common;
using SFC.Players.Application.Models.Players.Common;
using SFC.Players.Domain.Enums;

namespace SFC.Players.Application.UnitTests.Features.Players.Commands.Common.Validators;
public class PlayerValidatorTests
{
    private readonly Mock<IDateTimeService> _mockDateTimeService = new();
    private readonly BasePlayerDto VALID_PLAYER = new()
    {
        Profile = new PlayerProfileDto
        {
            General = new PlayerGeneralProfileDto
            {
                FirstName = "First Name",
                LastName = "Last Name",
                City = "City Value"
            },
            Football = new PlayerFootballProfileDto()
        },
        Stats = new PlayerStatsDto
        {
            Points = new PlayerStatPointsDto(),
            Values = PlayerTestConstants.VALID_STATS
        }
    };

    public PlayerValidatorTests()
    {
        _mockDateTimeService.Setup(dt => dt.Now).Returns(DateTime.UtcNow);
    }

    #region General profile

    [Theory]
    [Trait("Feature", "Validators")]
    [InlineData("", "'FirstName' must not be empty.")]
    [InlineData(null, "'FirstName' must not be empty.")]
    [InlineData("tucxrlbkhvdrzgzxvtxosapnwhgnhclrbiafabwytkgpikytuzvjsowcbnrzpyrrimbpwklqvijyvlfoybzlfrnmdoklxdlddbhwmgvfczjvunwitbkxohcupnhqppnxrrwzwhaidivbrsliytlftrt",
        "The length of 'FirstName' must be 150 characters or fewer. You entered 151 characters.")]
    public async Task Feature_Validator_FirstName_ShouldFailValidationWhenEmptyOrNullOrTooLong(string firstName, string errorMessage)
    {
        // Arrange
        BasePlayerDto player = VALID_PLAYER;
        player.Profile.General.FirstName = firstName;

        PlayerValidator<BasePlayerDto> validator = new(_mockDateTimeService.Object);

        // Act
        ValidationResult result = await validator.ValidateAsync(player);

        // Assert
        Assert.False(result.IsValid);
        Assert.Single(result.Errors);

        ValidationFailure failure = result.Errors.First();

        Assert.Equal(errorMessage, failure.ErrorMessage);
        Assert.Equal("Profile.General.FirstName", failure.PropertyName);
    }

    [Theory]
    [Trait("Feature", "Validators")]
    [InlineData("", "'LastName' must not be empty.")]
    [InlineData(null, "'LastName' must not be empty.")]
    [InlineData("tucxrlbkhvdrzgzxvtxosapnwhgnhclrbiafabwytkgpikytuzvjsowcbnrzpyrrimbpwklqvijyvlfoybzlfrnmdoklxdlddbhwmgvfczjvunwitbkxohcupnhqppnxrrwzwhaidivbrsliytlftrt",
        "The length of 'LastName' must be 150 characters or fewer. You entered 151 characters.")]
    public async Task Feature_Validator_LastName_ShouldFailValidationWhenEmptyOrNullOrTooLong(string lastName, string errorMessage)
    {
        // Arrange
        BasePlayerDto player = VALID_PLAYER;
        player.Profile.General.LastName = lastName;

        PlayerValidator<BasePlayerDto> validator = new(_mockDateTimeService.Object);

        // Act
        ValidationResult result = await validator.ValidateAsync(player);

        // Assert
        Assert.False(result.IsValid);
        Assert.Single(result.Errors);

        ValidationFailure failure = result.Errors.First();

        Assert.Equal(errorMessage, failure.ErrorMessage);
        Assert.Equal("Profile.General.LastName", failure.PropertyName);
    }

    [Theory]
    [Trait("Feature", "Validators")]
    [InlineData("oalczwusmmlhzsunswlbjnvjcjzbbeqlukwjp" +
        "fzhimjxckgsklpqlvsdpcicslonvpteoupnjodfdqxprr" +
        "qmvkyovgqiyjmraycnedpvppxzboqhrnbxzlgjkvpegqo" +
        "nnbgkcqtvzryousunzulodkmbcnbxdebijqepbokglsvq" +
        "guknokvlcranwkqbqsawrtcldriwqfmswqqcudmbnxisp" +
        "ucyakemznxoytvspvxjxffdjmrercyxiinwpxabanzrkt" +
        "vgrirsbzmqlymtgrkgizcaiafcaoeumurukrkluafmvjb" +
        "zehzaeoeowmrqhdmbuqhjnwfvtpyaivokglpsdhmibzaa" +
        "ttyttsduijjhgunxisxdcclhgkshkiwhmpbccoaauybfe" +
        "jfuivtggekuezhbpvnbciunheqzuuxpprevlbhizbmmvs" +
        "przfywtyhalgnzizjtyrbkqxtevclrvfuzzdokduayjoq" +
        "lpvybkblmkuqhgmwwpeigzndewyibfrydqmjoxvqgbpaj" +
        "nzcvkzyxicxzwpsjapynafgjghuttgdcdqlwvabvziihl" +
        "ocvkqtwdtbysxjtvqzvbvuhqxmoqkxuywalhnuadgjusp" +
        "vifuqphfrynkmlgvwiqfquvzkwbgdghmkyyrjxujpvwcy" +
        "fobedxdjgtjcksfgrynxsirwfhkwjlcdagjuohxkcqqtp" +
        "nhyymswaokwrrfuepcmivmqoconakwhkkvjpimavomyzm" +
        "kfkhjmjxpilpdpcgxcswgpvpzfhhaltxfvnoknabivebe" +
        "cojpuuafvpuqfxjzcmuvxsqxjpyktxywjakuepodojgdf" +
        "fhihalvzfzxnxgtiluiuvsxcouvucktfziyptxajpkgyj" +
        "uzkydiwlhavmbauebweuyeslestxyrdoogzuomifvzmoc" +
        "yakxlmfhrqsxaeqnrxpokkaewyxpdbakqihwahiamborg" +
        "spzohfdjxvnrolewelfufllznafqeeonduoaddiiqhdop" +
        "tebigqggdheailtwojwqkltz",
        "The length of 'Biography' must be 1050 characters or fewer. You entered 1051 characters.")]
    public async Task Feature_Validator_Biography_ShouldFailValidationWhenTooLong(string biography, string errorMessage)
    {
        // Arrange
        BasePlayerDto player = VALID_PLAYER;
        player.Profile.General.Biography = biography;

        PlayerValidator<BasePlayerDto> validator = new(_mockDateTimeService.Object);

        // Act
        ValidationResult result = await validator.ValidateAsync(player);

        // Assert
        Assert.False(result.IsValid);
        Assert.Single(result.Errors);

        ValidationFailure failure = result.Errors.First();

        Assert.Equal(errorMessage, failure.ErrorMessage);
        Assert.Equal("Profile.General.Biography", failure.PropertyName);
    }

    [Fact]
    [Trait("Feature", "Validators")]
    public async Task Feature_Validator_Birthday_ShouldPassValidationWhenNull()
    {
        // Arrange
        BasePlayerDto player = VALID_PLAYER;
        player.Profile.General.Birthday = null;

        PlayerValidator<BasePlayerDto> validator = new(_mockDateTimeService.Object);

        // Act
        ValidationResult result = await validator.ValidateAsync(player);

        // Assert
        Assert.True(result.IsValid);
        Assert.Empty(result.Errors);
    }

    public static readonly object[][] BirthdayData =
    {
        new object[] {new DateTime(1900, 1, 1), "'Birthday' must be more than 99 years ago." },
        new object[] {DateTime.UtcNow.AddYears(10), "'Birthday' must be less than today's date." }
    };

    [Theory]
    [Trait("Feature", "Validators")]
    [MemberData(nameof(BirthdayData))]
    public async Task Feature_Validator_Birthday_ShouldFailValidationWhenNotFitLimits(DateTime birthday, string errorMessage)
    {
        // Arrange
        BasePlayerDto player = VALID_PLAYER;
        player.Profile.General.Birthday = birthday;

        PlayerValidator<BasePlayerDto> validator = new(_mockDateTimeService.Object);

        // Act
        ValidationResult result = await validator.ValidateAsync(player);

        // Assert
        Assert.False(result.IsValid);
        Assert.Single(result.Errors);

        ValidationFailure failure = result.Errors.First();

        Assert.Equal(errorMessage, failure.ErrorMessage);
        Assert.Equal("Profile.General.Birthday", failure.PropertyName);
    }

    [Fact]
    [Trait("Feature", "Validators")]
    public async Task Feature_Validator_Photo_ShouldPassValidationWhenNull()
    {
        // Arrange
        BasePlayerDto player = VALID_PLAYER;
        player.Profile.General.Photo = null;

        PlayerValidator<BasePlayerDto> validator = new(_mockDateTimeService.Object);

        // Act
        ValidationResult result = await validator.ValidateAsync(player);

        // Assert
        Assert.True(result.IsValid);
        Assert.Empty(result.Errors);
    }

    [Theory]
    [Trait("Feature", "Validators")]
    [InlineData(0, PhotoExtension.Jpeg, "'Photo' must be between 1 and 5242880. You entered 0.",
        nameof(PlayerPhotoDto.Size))]
    [InlineData(ValidationConstants.PHOTO_MAX_SIZE_IN_BYTES + 1, PhotoExtension.Tiff, "'Photo' must be between 1 and 5242880. You entered 5242881.",
        nameof(PlayerPhotoDto.Size))]
    [InlineData(1, (PhotoExtension)22, "'Photo' has a range of values which does not include '22'.",
        nameof(PlayerPhotoDto.Extension))]
    public async Task Feature_Validator_Photo_ShouldFailValidationWhenNotFitSizeLimitOrExtension(int size, PhotoExtension extension, string errorMessage,
        string propName)
    {
        // Arrange
        BasePlayerDto player = VALID_PLAYER;
        player.Profile.General.Photo = new PlayerPhotoDto
        {
            Size = size,
            Extension = extension
        };

        PlayerValidator<BasePlayerDto> validator = new(_mockDateTimeService.Object);

        // Act
        ValidationResult result = await validator.ValidateAsync(player);

        // Assert
        Assert.False(result.IsValid);
        Assert.Single(result.Errors);

        ValidationFailure failure = result.Errors.First();

        Assert.Equal(errorMessage, failure.ErrorMessage);
        Assert.Equal($"Profile.General.Photo.{propName}", failure.PropertyName);
    }

    public static readonly object[][] TagsValidData =
    {
        new object[] { null! },
        new object[] { Array.Empty<string>() }
    };

    [Theory]
    [Trait("Feature", "Validators")]
    [MemberData(nameof(TagsValidData))]
    public async Task Feature_Validator_Tags_ShouldPassValidationWhenNullOrEmptyArray(IEnumerable<string> tags)
    {
        // Arrange
        BasePlayerDto player = VALID_PLAYER;
        player.Profile.General.Tags = tags;

        PlayerValidator<BasePlayerDto> validator = new(_mockDateTimeService.Object);

        // Act
        ValidationResult result = await validator.ValidateAsync(player);

        // Assert
        Assert.True(result.IsValid);
        Assert.Empty(result.Errors);
    }

    public static readonly object[][] TagsInvalidData =
    {
        new object[] { new string[2] { "test", "test" }, "Each value from 'Tags' must be unique." },
        new object[] { new string[51] { "test1", "test2", "test3", "test4", "test5", "test6", "test7", "test8", "test9", "test10",
                                        "test11", "test12", "test13", "test14", "test15", "test16", "test17", "test18", "test19", "test20",
                                        "test21", "test22", "test23", "test24", "test25", "test26", "test27", "test28", "test29", "test30",
                                        "test31", "test32", "test33", "test34", "test35", "test36", "test37", "test38", "test39", "test40",
                                        "test41", "test42", "test43", "test44", "test45", "test46", "test47", "test48", "test49", "test50", "test51"},
            string.Format(Messages.InvalidTagsSize, nameof(PlayerGeneralProfileDto.Tags), ValidationConstants.TAGS_MAX_LENGTH) },
        new object[] { new string[2] { "test", "" }, "Each value from 'Tags' must not be empty.", "[1]" },
        new object[] { new string[2] { "test", "123456789012345678901" }, "Each value from 'Tags' must be 20 characters or fewer. You entered 21 characters.", "[1]" }
    };

    [Theory]
    [Trait("Feature", "Validators")]
    [MemberData(nameof(TagsInvalidData))]
    public async Task Feature_Validator_Tags_ShouldFailValidationWhenHaveDuplicatesOrMoreThenMaxSizeOrHasEmptyTagOrHasTooLongTag(IEnumerable<string> tags, string errorMessage,
        string? propName = null)
    {
        // Arrange
        BasePlayerDto player = VALID_PLAYER;
        player.Profile.General.Tags = tags;

        PlayerValidator<BasePlayerDto> validator = new(_mockDateTimeService.Object);

        // Act
        ValidationResult result = await validator.ValidateAsync(player);

        // Assert
        Assert.False(result.IsValid);
        Assert.Single(result.Errors);

        ValidationFailure failure = result.Errors.First();

        Assert.Equal(errorMessage, failure.ErrorMessage);
        Assert.Equal($"Profile.General.Tags{propName}", failure.PropertyName);
    }

    public static readonly object[][] AvailabilityDaysValidData =
    {
        new object[] { null! },
        new object[] { Array.Empty<DayOfWeek>() }
    };

    [Theory]
    [Trait("Feature", "Validators")]
    [MemberData(nameof(AvailabilityDaysValidData))]
    public async Task Feature_Validator_Availability_ShouldPassValidationWhenAvailabilityDaysIsNullOrEmptyArray(IEnumerable<DayOfWeek> days)
    {
        // Arrange
        BasePlayerDto player = VALID_PLAYER;
        player.Profile.General.Availability = new PlayerAvailabilityDto
        {
            Days = days
        };

        PlayerValidator<BasePlayerDto> validator = new(_mockDateTimeService.Object);

        // Act
        ValidationResult result = await validator.ValidateAsync(player);

        // Assert
        Assert.True(result.IsValid);
        Assert.Empty(result.Errors);
    }

    public static readonly object[][] AvailabilityDaysInvalidData =
    {
        new object[] { new DayOfWeek[8] { DayOfWeek.Monday, DayOfWeek.Monday, DayOfWeek.Monday, DayOfWeek.Monday, DayOfWeek.Monday,
            DayOfWeek.Monday, DayOfWeek.Monday, DayOfWeek.Monday }, "The length of 'Days' must be less or equal to 7." },
        new object[] { new DayOfWeek[2] { DayOfWeek.Monday, (DayOfWeek)22 }, "Each value from 'Days' must be in Days of Week range.", "[1]" }
    };

    [Theory]
    [Trait("Feature", "Validators")]
    [MemberData(nameof(AvailabilityDaysInvalidData))]
    public async Task Feature_Validator_Availability_ShouldFailValidationWhenMoreThenMaxSizeOrHasInvalidDay(IEnumerable<DayOfWeek> days, string errorMessage,
        string? propName = null)
    {
        // Arrange
        BasePlayerDto player = VALID_PLAYER;
        player.Profile.General.Availability = new PlayerAvailabilityDto
        {
            Days = days
        };

        PlayerValidator<BasePlayerDto> validator = new(_mockDateTimeService.Object);

        // Act
        ValidationResult result = await validator.ValidateAsync(player);

        // Assert
        Assert.False(result.IsValid);
        Assert.Single(result.Errors);

        ValidationFailure failure = result.Errors.First();

        Assert.Equal(errorMessage, failure.ErrorMessage);
        Assert.Equal($"Profile.General.Availability.Days{propName}", failure.PropertyName);
    }

    public static readonly object[][] AvailabilityFromToValidData =
    {
        new object[] { TimeSpan.FromHours(1), null! },
        new object[] { null!, TimeSpan.FromHours(1) },
        new object[] { null!, null! }
    };

    [Theory]
    [Trait("Feature", "Validators")]
    [MemberData(nameof(AvailabilityFromToValidData))]
    public async Task Feature_Validator_Availability_ShouldPassValidationWhenFromIsNullOrToIsNullOrBothIsNull(TimeSpan? from, TimeSpan? to)
    {
        // Arrange
        BasePlayerDto player = VALID_PLAYER;
        player.Profile.General.Availability = new PlayerAvailabilityDto
        {
            From = from,
            To = to
        };

        PlayerValidator<BasePlayerDto> validator = new(_mockDateTimeService.Object);

        // Act
        ValidationResult result = await validator.ValidateAsync(player);

        // Assert
        Assert.True(result.IsValid);
        Assert.Empty(result.Errors);
    }

    [Fact]
    [Trait("Feature", "Validators")]
    public async Task Feature_Validator_Availability_ShouldFailValidationWhenFromIsMoreThanToAndViceVersa()
    {
        // Arrange
        BasePlayerDto player = VALID_PLAYER;
        player.Profile.General.Availability = new PlayerAvailabilityDto
        {
            From = TimeSpan.FromHours(2),
            To = TimeSpan.FromHours(1)
        };

        PlayerValidator<BasePlayerDto> validator = new(_mockDateTimeService.Object);

        // Act
        ValidationResult result = await validator.ValidateAsync(player);

        // Assert
        Assert.False(result.IsValid);
        Assert.Equal(2, result.Errors.Count);

        ValidationFailure toFailure = result.Errors.First();
        ValidationFailure fromFailure = result.Errors[1];

        Assert.Equal("'From' value must be less than To value.", fromFailure.ErrorMessage);
        Assert.Equal($"Profile.General.Availability.From", fromFailure.PropertyName);
        Assert.Equal("'To' value must be greater than From value.", toFailure.ErrorMessage);
        Assert.Equal($"Profile.General.Availability.To", toFailure.PropertyName);
    }

    #endregion General profile

    #region Football profile

    [Fact]
    [Trait("Feature", "Validators")]
    public async Task Feature_Validator_Height_ShouldPassValidationWhenNull()
    {
        // Arrange
        BasePlayerDto player = VALID_PLAYER;
        player.Profile.Football.Height = null;

        PlayerValidator<BasePlayerDto> validator = new(_mockDateTimeService.Object);

        // Act
        ValidationResult result = await validator.ValidateAsync(player);

        // Assert
        Assert.True(result.IsValid);
        Assert.Empty(result.Errors);
    }

    [Theory]
    [Trait("Feature", "Validators")]
    [InlineData(0)]
    [InlineData(301)]
    public async Task Feature_Validator_Height_ShouldFailValidationWhenNotFitLimits(int height)
    {
        // Arrange
        BasePlayerDto player = VALID_PLAYER;
        player.Profile.Football.Height = height;

        PlayerValidator<BasePlayerDto> validator = new(_mockDateTimeService.Object);

        // Act
        ValidationResult result = await validator.ValidateAsync(player);

        // Assert
        Assert.False(result.IsValid);
        Assert.Single(result.Errors);

        ValidationFailure failure = result.Errors.First();

        Assert.Equal($"'Height' must be between 1 and 300. You entered {height}.", failure.ErrorMessage);
        Assert.Equal("Profile.Football.Height", failure.PropertyName);
    }

    [Fact]
    [Trait("Feature", "Validators")]
    public async Task Feature_Validator_Weight_ShouldPassValidationWhenNull()
    {
        // Arrange
        BasePlayerDto player = VALID_PLAYER;
        player.Profile.Football.Weight = null;

        PlayerValidator<BasePlayerDto> validator = new(_mockDateTimeService.Object);

        // Act
        ValidationResult result = await validator.ValidateAsync(player);

        // Assert
        Assert.True(result.IsValid);
        Assert.Empty(result.Errors);
    }

    [Theory]
    [Trait("Feature", "Validators")]
    [InlineData(0)]
    [InlineData(301)]
    public async Task Feature_Validator_Weight_ShouldFailValidationWhenNotFitLimits(int weight)
    {
        // Arrange
        BasePlayerDto player = VALID_PLAYER;
        player.Profile.Football.Weight = weight;

        PlayerValidator<BasePlayerDto> validator = new(_mockDateTimeService.Object);

        // Act
        ValidationResult result = await validator.ValidateAsync(player);

        // Assert
        Assert.False(result.IsValid);
        Assert.Single(result.Errors);

        ValidationFailure failure = result.Errors.First();

        Assert.Equal($"'Weight' must be between 1 and 300. You entered {weight}.", failure.ErrorMessage);
        Assert.Equal("Profile.Football.Weight", failure.PropertyName);
    }

    [Fact]
    [Trait("Feature", "Validators")]
    public async Task Feature_Validator_Position_ShouldPassValidationWhenNull()
    {
        // Arrange
        BasePlayerDto player = VALID_PLAYER;
        player.Profile.Football.Position = null;

        PlayerValidator<BasePlayerDto> validator = new(_mockDateTimeService.Object);

        // Act
        ValidationResult result = await validator.ValidateAsync(player);

        // Assert
        Assert.True(result.IsValid);
        Assert.Empty(result.Errors);
    }

    [Fact]
    [Trait("Feature", "Validators")]
    public async Task Feature_Validator_Position_ShouldFailValidationWhenHasInvalidValue()
    {
        // Arrange
        BasePlayerDto player = VALID_PLAYER;
        player.Profile.Football.Position = (FootballPosition)22;

        PlayerValidator<BasePlayerDto> validator = new(_mockDateTimeService.Object);

        // Act
        ValidationResult result = await validator.ValidateAsync(player);

        // Assert
        Assert.False(result.IsValid);
        Assert.Single(result.Errors);

        ValidationFailure failure = result.Errors.First();

        Assert.Equal("'Position' has a range of values which does not include '22'.", failure.ErrorMessage);
        Assert.Equal("Profile.Football.Position", failure.PropertyName);
    }

    [Fact]
    [Trait("Feature", "Validators")]
    public async Task Feature_Validator_Position_ShouldFailValidationWhenEqualToAdditionalPosition()
    {
        // Arrange
        BasePlayerDto player = VALID_PLAYER;
        player.Profile.Football.Position = FootballPosition.Forward;
        player.Profile.Football.AdditionalPosition = FootballPosition.Forward;

        PlayerValidator<BasePlayerDto> validator = new(_mockDateTimeService.Object);

        // Act
        ValidationResult result = await validator.ValidateAsync(player);

        // Assert
        Assert.False(result.IsValid);
        Assert.Equal(2, result.Errors.Count);

        ValidationFailure failure = result.Errors.First();

        Assert.Equal("'Position' must not to be equal to 'AdditionalPosition'.", failure.ErrorMessage);
        Assert.Equal("Profile.Football.Position", failure.PropertyName);
    }

    [Fact]
    [Trait("Feature", "Validators")]
    public async Task Feature_Validator_AdditionalPosition_ShouldPassValidationWhenNull()
    {
        // Arrange
        BasePlayerDto player = VALID_PLAYER;
        player.Profile.Football.AdditionalPosition = null;

        PlayerValidator<BasePlayerDto> validator = new(_mockDateTimeService.Object);

        // Act
        ValidationResult result = await validator.ValidateAsync(player);

        // Assert
        Assert.True(result.IsValid);
        Assert.Empty(result.Errors);
    }

    [Fact]
    [Trait("Feature", "Validators")]
    public async Task Feature_Validator_AdditionalPosition_ShouldFailValidationWhenHasInvalidValue()
    {
        // Arrange
        BasePlayerDto player = VALID_PLAYER;
        player.Profile.Football.AdditionalPosition = (FootballPosition)22;

        PlayerValidator<BasePlayerDto> validator = new(_mockDateTimeService.Object);

        // Act
        ValidationResult result = await validator.ValidateAsync(player);

        // Assert
        Assert.False(result.IsValid);
        Assert.Single(result.Errors);

        ValidationFailure failure = result.Errors.First();

        Assert.Equal("'AdditionalPosition' has a range of values which does not include '22'.", failure.ErrorMessage);
        Assert.Equal("Profile.Football.AdditionalPosition", failure.PropertyName);
    }

    [Fact]
    [Trait("Feature", "Validators")]
    public async Task Feature_Validator_AdditionalPosition_ShouldFailValidationWhenEqualToPosition()
    {
        // Arrange
        BasePlayerDto player = VALID_PLAYER;
        player.Profile.Football.AdditionalPosition = FootballPosition.Forward;
        player.Profile.Football.Position = FootballPosition.Forward;

        PlayerValidator<BasePlayerDto> validator = new(_mockDateTimeService.Object);

        // Act
        ValidationResult result = await validator.ValidateAsync(player);

        // Assert
        Assert.False(result.IsValid);
        Assert.Equal(2, result.Errors.Count);

        ValidationFailure failure = result.Errors[1];

        Assert.Equal("'AdditionalPosition' must not to be equal to 'Position'.", failure.ErrorMessage);
        Assert.Equal("Profile.Football.AdditionalPosition", failure.PropertyName);
    }

    [Fact]
    [Trait("Feature", "Validators")]
    public async Task Feature_Validator_WorkingFoot_ShouldPassValidationWhenNull()
    {
        // Arrange
        BasePlayerDto player = VALID_PLAYER;
        player.Profile.Football.WorkingFoot = null;

        PlayerValidator<BasePlayerDto> validator = new(_mockDateTimeService.Object);

        // Act
        ValidationResult result = await validator.ValidateAsync(player);

        // Assert
        Assert.True(result.IsValid);
        Assert.Empty(result.Errors);
    }

    [Fact]
    [Trait("Feature", "Validators")]
    public async Task Feature_Validator_WorkingFoot_ShouldFailValidationWhenHasInvalidValue()
    {
        // Arrange
        BasePlayerDto player = VALID_PLAYER;
        player.Profile.Football.WorkingFoot = (WorkingFoot)22;

        PlayerValidator<BasePlayerDto> validator = new(_mockDateTimeService.Object);

        // Act
        ValidationResult result = await validator.ValidateAsync(player);

        // Assert
        Assert.False(result.IsValid);
        Assert.Single(result.Errors);

        ValidationFailure failure = result.Errors.First();

        Assert.Equal("'WorkingFoot' has a range of values which does not include '22'.", failure.ErrorMessage);
        Assert.Equal("Profile.Football.WorkingFoot", failure.PropertyName);
    }

    [Fact]
    [Trait("Feature", "Validators")]
    public async Task Feature_Validator_Number_ShouldPassValidationWhenNull()
    {
        // Arrange
        BasePlayerDto player = VALID_PLAYER;
        player.Profile.Football.Number = null;

        PlayerValidator<BasePlayerDto> validator = new(_mockDateTimeService.Object);

        // Act
        ValidationResult result = await validator.ValidateAsync(player);

        // Assert
        Assert.True(result.IsValid);
        Assert.Empty(result.Errors);
    }

    [Theory]
    [Trait("Feature", "Validators")]
    [InlineData(-1)]
    [InlineData(100)]
    public async Task Feature_Validator_Number_ShouldFailValidationWhenNotFitLimits(int number)
    {
        // Arrange
        BasePlayerDto player = VALID_PLAYER;
        player.Profile.Football.Number = number;

        PlayerValidator<BasePlayerDto> validator = new(_mockDateTimeService.Object);

        // Act
        ValidationResult result = await validator.ValidateAsync(player);

        // Assert
        Assert.False(result.IsValid);
        Assert.Single(result.Errors);

        ValidationFailure failure = result.Errors.First();

        Assert.Equal($"'Number' must be between 0 and 99. You entered {number}.", failure.ErrorMessage);
        Assert.Equal("Profile.Football.Number", failure.PropertyName);
    }

    [Fact]
    [Trait("Feature", "Validators")]
    public async Task Feature_Validator_GameStyle_ShouldPassValidationWhenNull()
    {
        // Arrange
        BasePlayerDto player = VALID_PLAYER;
        player.Profile.Football.GameStyle = null;

        PlayerValidator<BasePlayerDto> validator = new(_mockDateTimeService.Object);

        // Act
        ValidationResult result = await validator.ValidateAsync(player);

        // Assert
        Assert.True(result.IsValid);
        Assert.Empty(result.Errors);
    }

    [Fact]
    [Trait("Feature", "Validators")]
    public async Task Feature_Validator_GameStyle_ShouldFailValidationWhenHasInvalidValue()
    {
        // Arrange
        BasePlayerDto player = VALID_PLAYER;
        player.Profile.Football.GameStyle = (GameStyle)22;

        PlayerValidator<BasePlayerDto> validator = new(_mockDateTimeService.Object);

        // Act
        ValidationResult result = await validator.ValidateAsync(player);

        // Assert
        Assert.False(result.IsValid);
        Assert.Single(result.Errors);

        ValidationFailure failure = result.Errors.First();

        Assert.Equal("'GameStyle' has a range of values which does not include '22'.", failure.ErrorMessage);
        Assert.Equal("Profile.Football.GameStyle", failure.PropertyName);
    }

    [Fact]
    [Trait("Feature", "Validators")]
    public async Task Feature_Validator_Skill_ShouldPassValidationWhenNull()
    {
        // Arrange
        BasePlayerDto player = VALID_PLAYER;
        player.Profile.Football.Skill = null;

        PlayerValidator<BasePlayerDto> validator = new(_mockDateTimeService.Object);

        // Act
        ValidationResult result = await validator.ValidateAsync(player);

        // Assert
        Assert.True(result.IsValid);
        Assert.Empty(result.Errors);
    }

    [Theory]
    [Trait("Feature", "Validators")]
    [InlineData(-1)]
    [InlineData(6)]
    public async Task Feature_Validator_Skill_ShouldFailValidationWhenNotFitLimits(int skill)
    {
        // Arrange
        BasePlayerDto player = VALID_PLAYER;
        player.Profile.Football.Skill = skill;

        PlayerValidator<BasePlayerDto> validator = new(_mockDateTimeService.Object);

        // Act
        ValidationResult result = await validator.ValidateAsync(player);

        // Assert
        Assert.False(result.IsValid);
        Assert.Single(result.Errors);

        ValidationFailure failure = result.Errors.First();

        Assert.Equal($"'Skill' must be between 0 and 5. You entered {skill}.", failure.ErrorMessage);
        Assert.Equal("Profile.Football.Skill", failure.PropertyName);
    }

    [Fact]
    [Trait("Feature", "Validators")]
    public async Task Feature_Validator_WeakFoot_ShouldPassValidationWhenNull()
    {
        // Arrange
        BasePlayerDto player = VALID_PLAYER;
        player.Profile.Football.WeakFoot = null;

        PlayerValidator<BasePlayerDto> validator = new(_mockDateTimeService.Object);

        // Act
        ValidationResult result = await validator.ValidateAsync(player);

        // Assert
        Assert.True(result.IsValid);
        Assert.Empty(result.Errors);
    }

    [Theory]
    [Trait("Feature", "Validators")]
    [InlineData(-1)]
    [InlineData(6)]
    public async Task Feature_Validator_WeakFoot_ShouldFailValidationWhenNotFitLimits(int skill)
    {
        // Arrange
        BasePlayerDto player = VALID_PLAYER;
        player.Profile.Football.WeakFoot = skill;

        PlayerValidator<BasePlayerDto> validator = new(_mockDateTimeService.Object);

        // Act
        ValidationResult result = await validator.ValidateAsync(player);

        // Assert
        Assert.False(result.IsValid);
        Assert.Single(result.Errors);

        ValidationFailure failure = result.Errors.First();

        Assert.Equal($"'WeakFoot' must be between 0 and 5. You entered {skill}.", failure.ErrorMessage);
        Assert.Equal("Profile.Football.WeakFoot", failure.PropertyName);
    }

    [Fact]
    [Trait("Feature", "Validators")]
    public async Task Feature_Validator_PhysicalCondition_ShouldPassValidationWhenNull()
    {
        // Arrange
        BasePlayerDto player = VALID_PLAYER;
        player.Profile.Football.PhysicalCondition = null;

        PlayerValidator<BasePlayerDto> validator = new(_mockDateTimeService.Object);

        // Act
        ValidationResult result = await validator.ValidateAsync(player);

        // Assert
        Assert.True(result.IsValid);
        Assert.Empty(result.Errors);
    }

    [Theory]
    [Trait("Feature", "Validators")]
    [InlineData(-1)]
    [InlineData(6)]
    public async Task Feature_Validator_PhysicalCondition_ShouldFailValidationWhenNotFitLimits(int skill)
    {
        // Arrange
        BasePlayerDto player = VALID_PLAYER;
        player.Profile.Football.PhysicalCondition = skill;

        PlayerValidator<BasePlayerDto> validator = new(_mockDateTimeService.Object);

        // Act
        ValidationResult result = await validator.ValidateAsync(player);

        // Assert
        Assert.False(result.IsValid);
        Assert.Single(result.Errors);

        ValidationFailure failure = result.Errors.First();

        Assert.Equal($"'PhysicalCondition' must be between 0 and 5. You entered {skill}.", failure.ErrorMessage);
        Assert.Equal("Profile.Football.PhysicalCondition", failure.PropertyName);
    }

    #endregion Football profile

    #region Stats

    [Fact]
    [Trait("Feature", "Validators")]
    public async Task Feature_Validator_Points_Available_ShouldFailValidationWhenLessThanZero()
    {
        // Arrange
        BasePlayerDto player = VALID_PLAYER;
        player.Stats.Points = new PlayerStatPointsDto
        {
            Available = -1
        };

        PlayerValidator<BasePlayerDto> validator = new(_mockDateTimeService.Object);

        // Act
        ValidationResult result = await validator.ValidateAsync(player);

        // Assert
        Assert.False(result.IsValid);
        Assert.Single(result.Errors);

        ValidationFailure failure = result.Errors.First();

        Assert.Equal("'Available' must be greater than or equal to '0'.", failure.ErrorMessage);
        Assert.Equal($"Stats.Points.Available", failure.PropertyName);
    }

    [Fact]
    [Trait("Feature", "Validators")]
    public async Task Feature_Validator_Points_Used_ShouldFailValidationWhenLessThanZero()
    {
        // Arrange
        BasePlayerDto player = VALID_PLAYER;
        player.Stats.Points = new PlayerStatPointsDto
        {
            Used = -1
        };

        PlayerValidator<BasePlayerDto> validator = new(_mockDateTimeService.Object);

        // Act
        ValidationResult result = await validator.ValidateAsync(player);

        // Assert
        Assert.False(result.IsValid);
        Assert.Single(result.Errors);

        ValidationFailure failure = result.Errors.First();

        Assert.Equal("'Used' must be greater than or equal to '0'.", failure.ErrorMessage);
        Assert.Equal($"Stats.Points.Used", failure.PropertyName);
    }

    [Fact]
    [Trait("Feature", "Validators")]
    public async Task Feature_Validator_Stat_Values_ShouldFailValidationWhenCountIsNotEqualToConstant()
    {
        // Arrange
        BasePlayerDto player = VALID_PLAYER;
        player.Stats.Values = new List<PlayerStatValueDto>();
        PlayerValidator<BasePlayerDto> validator = new(_mockDateTimeService.Object);

        // Act
        ValidationResult result = await validator.ValidateAsync(player);

        // Assert
        Assert.False(result.IsValid);
        Assert.Single(result.Errors);

        ValidationFailure failure = result.Errors.First();

        Assert.Equal($"The length of 'Stats' must be equal to {ValidationConstants.STATS_COUNT}.", failure.ErrorMessage);
        Assert.Equal($"Stats.Values", failure.PropertyName);
    }



    [Fact]
    [Trait("Feature", "Validators")]
    public async Task Feature_Validator_Stat_Values_ShouldFailValidationWhenCategoryIsInvalid()
    {
        // Arrange
        BasePlayerDto player = JsonSerializer.Deserialize<BasePlayerDto>(JsonSerializer.Serialize(VALID_PLAYER))!;
        player.Stats.Values.ToList()[0].Category = (StatCategory)22;
        PlayerValidator<BasePlayerDto> validator = new(_mockDateTimeService.Object);

        // Act
        ValidationResult result = await validator.ValidateAsync(player);

        // Assert
        Assert.False(result.IsValid);
        Assert.Equal(2, result.Errors.Count);

        ValidationFailure failure = result.Errors.First();

        Assert.Equal("Each value from 'Stats' must have Category in Stat Category range.", failure.ErrorMessage);
        Assert.Equal($"Stats.Values[0]", failure.PropertyName);
    }

    [Fact]
    [Trait("Feature", "Validators")]
    public async Task Feature_Validator_Stat_Values_ShouldFailValidationWhenTypeIsInvalid()
    {
        // Arrange
        BasePlayerDto player = JsonSerializer.Deserialize<BasePlayerDto>(JsonSerializer.Serialize(VALID_PLAYER))!;
        player.Stats.Values.ToList()[0].Type = (StatType)29;
        PlayerValidator<BasePlayerDto> validator = new(_mockDateTimeService.Object);

        // Act
        ValidationResult result = await validator.ValidateAsync(player);

        // Assert
        Assert.False(result.IsValid);
        Assert.Equal(2, result.Errors.Count);

        ValidationFailure failure = result.Errors.First();

        Assert.Equal("Each value from 'Stats' must have Type in Stat Type range.", failure.ErrorMessage);
        Assert.Equal($"Stats.Values[0]", failure.PropertyName);
    }

    [Fact]
    [Trait("Feature", "Validators")]
    public async Task Feature_Validator_Stat_Values_ShouldFailValidationWhenTypeNotInSpecificCategory()
    {
        // Arrange
        BasePlayerDto player = JsonSerializer.Deserialize<BasePlayerDto>(JsonSerializer.Serialize(VALID_PLAYER))!;
        player.Stats.Values.ToList()[0].Type = (StatType)28;
        PlayerValidator<BasePlayerDto> validator = new(_mockDateTimeService.Object);

        // Act
        ValidationResult result = await validator.ValidateAsync(player);

        // Assert
        Assert.False(result.IsValid);
        Assert.Single(result.Errors);

        ValidationFailure failure = result.Errors.First();

        Assert.Equal("Each value from 'Stats' must have Type for specific Category.", failure.ErrorMessage);
        Assert.Equal($"Stats.Values[0]", failure.PropertyName);
    }

    [Theory]
    [Trait("Feature", "Validators")]
    [InlineData(102)]
    [InlineData(101)]
    public async Task Feature_Validator_Stat_Values_ShouldFailValidationWhenValueIsNotFitLimits(byte value)
    {
        // Arrange
        BasePlayerDto player = JsonSerializer.Deserialize<BasePlayerDto>(JsonSerializer.Serialize(VALID_PLAYER))!;
        player.Stats.Values.ToList()[0].Value = value;
        PlayerValidator<BasePlayerDto> validator = new(_mockDateTimeService.Object);

        // Act
        ValidationResult result = await validator.ValidateAsync(player);

        // Assert
        Assert.False(result.IsValid);
        Assert.Single(result.Errors);

        ValidationFailure failure = result.Errors.First();

        Assert.Equal("Each value from 'Stats' must have Value between 0 and 100.", failure.ErrorMessage);
        Assert.Equal($"Stats.Values[0]", failure.PropertyName);
    }

    #endregion Stats
}
