//using System.Text.Json;

//using FluentValidation.Results;

//using Moq;

//using SFC.Player.Application.Common.Constants;
//using SFC.Player.Application.Features.Players.Commands.Common.Validators;
//using SFC.Player.Application.Features.Players.Common.Dto;
//using SFC.Player.Application.Interfaces.Common;
//using SFC.Player.Application.Features.Players.Common;
//using SFC.Player.Domain.Entities.Data;
//using SFC.Player.Domain.Enums;
//using SFC.Player.Application.Interfaces.Persistence.Repository;

//namespace SFC.Player.Application.UnitTests.Features.Player.Commands.Common.Validators;
//public class PlayerValidatorTests
//{
//    private readonly Mock<IDateTimeService> _mockDateTimeService = new();
//    private readonly Mock<IStatTypeRepository> _statTypeRepository = new();
//    private readonly Mock<IDataRepository<FootballPosition>> _footballPositionRepository = new();
//    private readonly Mock<IDataRepository<GameStyle>> _gameStyleRepository = new();
//    private readonly Mock<IDataRepository<WorkingFoot>> _workingFootRepository = new();

//    private PlayerValidator<BasePlayerDto> Validator
//    {
//        get
//        {
//            return new(
//                _mockDateTimeService.Object,
//                _statTypeRepository.Object,
//                _footballPositionRepository.Object,
//                _workingFootRepository.Object,
//                _gameStyleRepository.Object);
//        }
//    }

//    public PlayerValidatorTests()
//    {
//        _mockDateTimeService.Setup(dt => dt.Now).Returns(DateTime.UtcNow);
//        _statTypeRepository.Setup(r => r.CountAsync()).ReturnsAsync(PlayerTestConstants.STAT_TYPES_COUNT);
//        _statTypeRepository.Setup(r => r.ListAllAsync()).ReturnsAsync(PlayerTestConstants.STAT_TYPES);
//        _footballPositionRepository.Setup(r => r.AnyAsync(It.IsAny<int>())).ReturnsAsync(true);
//        _gameStyleRepository.Setup(r => r.AnyAsync(It.IsAny<int>())).ReturnsAsync(true);
//        _workingFootRepository.Setup(r => r.AnyAsync(It.IsAny<int>())).ReturnsAsync(true);
//    }

//    #region General profile

//    [Theory]
//    [Trait("Feature", "Validators")]
//    [InlineData("", "'FirstName' must not be empty.")]
//    [InlineData(null, "'FirstName' must not be empty.")]
//    [InlineData("tucxrlbkhvdrzgzxvtxosapnwhgnhclrbiafabwytkgpikytuzvjsowcbnrzpyrrimbpwklqvijyvlfoybzlfrnmdoklxdlddbhwmgvfczjvunwitbkxohcupnhqppnxrrwzwhaidivbrsliytlftrt",
//        "The length of 'FirstName' must be 150 characters or fewer. You entered 151 characters.")]
//    public async Task Feature_Validator_FirstName_ShouldFailValidationWhenEmptyOrNullOrTooLong(string? firstName, string errorMessage)
//    {
//        // Arrange
//        BasePlayerDto player = PlayerTestConstants.GetValidPlayer();
//        player.Profile.General.FirstName = firstName!;

//        // Act
//        ValidationResult result = await Validator.ValidateAsync(player);

//        // Assert
//        Assert.False(result.IsValid);
//        Assert.Single(result.Errors);

//        ValidationFailure failure = result.Errors.First();

//        Assert.Equal(errorMessage, failure.ErrorMessage);
//        Assert.Equal("Profile.General.FirstName", failure.PropertyName);
//    }

//    [Theory]
//    [Trait("Feature", "Validators")]
//    [InlineData("", "'LastName' must not be empty.")]
//    [InlineData(null, "'LastName' must not be empty.")]
//    [InlineData("tucxrlbkhvdrzgzxvtxosapnwhgnhclrbiafabwytkgpikytuzvjsowcbnrzpyrrimbpwklqvijyvlfoybzlfrnmdoklxdlddbhwmgvfczjvunwitbkxohcupnhqppnxrrwzwhaidivbrsliytlftrt",
//        "The length of 'LastName' must be 150 characters or fewer. You entered 151 characters.")]
//    public async Task Feature_Validator_LastName_ShouldFailValidationWhenEmptyOrNullOrTooLong(string? lastName, string errorMessage)
//    {
//        // Arrange
//        BasePlayerDto player = PlayerTestConstants.GetValidPlayer();
//        player.Profile.General.LastName = lastName!;

//        // Act
//        ValidationResult result = await Validator.ValidateAsync(player);

//        // Assert
//        Assert.False(result.IsValid);
//        Assert.Single(result.Errors);

//        ValidationFailure failure = result.Errors.First();

//        Assert.Equal(errorMessage, failure.ErrorMessage);
//        Assert.Equal("Profile.General.LastName", failure.PropertyName);
//    }

//    [Theory]
//    [Trait("Feature", "Validators")]
//    [InlineData("oalczwusmmlhzsunswlbjnvjcjzbbeqlukwjp" +
//        "fzhimjxckgsklpqlvsdpcicslonvpteoupnjodfdqxprr" +
//        "qmvkyovgqiyjmraycnedpvppxzboqhrnbxzlgjkvpegqo" +
//        "nnbgkcqtvzryousunzulodkmbcnbxdebijqepbokglsvq" +
//        "guknokvlcranwkqbqsawrtcldriwqfmswqqcudmbnxisp" +
//        "ucyakemznxoytvspvxjxffdjmrercyxiinwpxabanzrkt" +
//        "vgrirsbzmqlymtgrkgizcaiafcaoeumurukrkluafmvjb" +
//        "zehzaeoeowmrqhdmbuqhjnwfvtpyaivokglpsdhmibzaa" +
//        "ttyttsduijjhgunxisxdcclhgkshkiwhmpbccoaauybfe" +
//        "jfuivtggekuezhbpvnbciunheqzuuxpprevlbhizbmmvs" +
//        "przfywtyhalgnzizjtyrbkqxtevclrvfuzzdokduayjoq" +
//        "lpvybkblmkuqhgmwwpeigzndewyibfrydqmjoxvqgbpaj" +
//        "nzcvkzyxicxzwpsjapynafgjghuttgdcdqlwvabvziihl" +
//        "ocvkqtwdtbysxjtvqzvbvuhqxmoqkxuywalhnuadgjusp" +
//        "vifuqphfrynkmlgvwiqfquvzkwbgdghmkyyrjxujpvwcy" +
//        "fobedxdjgtjcksfgrynxsirwfhkwjlcdagjuohxkcqqtp" +
//        "nhyymswaokwrrfuepcmivmqoconakwhkkvjpimavomyzm" +
//        "kfkhjmjxpilpdpcgxcswgpvpzfhhaltxfvnoknabivebe" +
//        "cojpuuafvpuqfxjzcmuvxsqxjpyktxywjakuepodojgdf" +
//        "fhihalvzfzxnxgtiluiuvsxcouvucktfziyptxajpkgyj" +
//        "uzkydiwlhavmbauebweuyeslestxyrdoogzuomifvzmoc" +
//        "yakxlmfhrqsxaeqnrxpokkaewyxpdbakqihwahiamborg" +
//        "spzohfdjxvnrolewelfufllznafqeeonduoaddiiqhdop" +
//        "tebigqggdheailtwojwqkltz",
//        "The length of 'Biography' must be 1050 characters or fewer. You entered 1051 characters.")]
//    public async Task Feature_Validator_Biography_ShouldFailValidationWhenTooLong(string biography, string errorMessage)
//    {
//        // Arrange
//        BasePlayerDto player = PlayerTestConstants.GetValidPlayer();
//        player.Profile.General.Biography = biography;

//        // Act
//        ValidationResult result = await Validator.ValidateAsync(player);

//        // Assert
//        Assert.False(result.IsValid);
//        Assert.Single(result.Errors);

//        ValidationFailure failure = result.Errors.First();

//        Assert.Equal(errorMessage, failure.ErrorMessage);
//        Assert.Equal("Profile.General.Biography", failure.PropertyName);
//    }

//    [Fact]
//    [Trait("Feature", "Validators")]
//    public async Task Feature_Validator_Birthday_ShouldPassValidationWhenNull()
//    {
//        // Arrange
//        BasePlayerDto player = PlayerTestConstants.GetValidPlayer();
//        player.Profile.General.Birthday = null;

//        // Act
//        ValidationResult result = await Validator.ValidateAsync(player);

//        // Assert
//        Assert.True(result.IsValid);
//        Assert.Empty(result.Errors);
//    }

//    public static readonly object[][] BirthdayData =
//    [
//        [new DateTime(1900, 1, 1), "'Birthday' must be more than 99 years ago."],
//        [DateTime.UtcNow.AddYears(10), "'Birthday' must be less than today's date."]
//    ];

//    [Theory]
//    [Trait("Feature", "Validators")]
//    [MemberData(nameof(BirthdayData))]
//    public async Task Feature_Validator_Birthday_ShouldFailValidationWhenNotFitLimits(DateTime birthday, string errorMessage)
//    {
//        // Arrange
//        BasePlayerDto player = PlayerTestConstants.GetValidPlayer();
//        player.Profile.General.Birthday = birthday;

//        // Act
//        ValidationResult result = await Validator.ValidateAsync(player);

//        // Assert
//        Assert.False(result.IsValid);
//        Assert.Single(result.Errors);

//        ValidationFailure failure = result.Errors.First();

//        Assert.Equal(errorMessage, failure.ErrorMessage);
//        Assert.Equal("Profile.General.Birthday", failure.PropertyName);
//    }

//    [Fact]
//    [Trait("Feature", "Validators")]
//    public async Task Feature_Validator_Photo_ShouldPassValidationWhenNull()
//    {
//        // Arrange
//        BasePlayerDto player = PlayerTestConstants.GetValidPlayer();
//        player.Profile.General.Photo = null;

//        // Act
//        ValidationResult result = await Validator.ValidateAsync(player);

//        // Assert
//        Assert.True(result.IsValid);
//        Assert.Empty(result.Errors);
//    }

//    [Theory]
//    [Trait("Feature", "Validators")]
//    [InlineData(0, PhotoExtension.Jpeg, "'Photo' must be between 1 and 5242880. You entered 0.",
//        nameof(PlayerPhotoDto.Size))]
//    [InlineData(ValidationConstants.PHOTO_MAX_SIZE_IN_BYTES + 1, PhotoExtension.Tiff, "'Photo' must be between 1 and 5242880. You entered 5242881.",
//        nameof(PlayerPhotoDto.Size))]
//    [InlineData(1, (PhotoExtension)22, "'Photo' has a range of values which does not include '22'.",
//        nameof(PlayerPhotoDto.Extension))]
//    public async Task Feature_Validator_Photo_ShouldFailValidationWhenNotFitSizeLimitOrExtension(int size, PhotoExtension extension, string errorMessage,
//        string propName)
//    {
//        // Arrange
//        BasePlayerDto player = PlayerTestConstants.GetValidPlayer();
//        player.Profile.General.Photo = new PlayerPhotoDto
//        {
//            Size = size,
//            Extension = extension
//        };

//        // Act
//        ValidationResult result = await Validator.ValidateAsync(player);

//        // Assert
//        Assert.False(result.IsValid);
//        Assert.Single(result.Errors);

//        ValidationFailure failure = result.Errors.First();

//        Assert.Equal(errorMessage, failure.ErrorMessage);
//        Assert.Equal($"Profile.General.Photo.{propName}", failure.PropertyName);
//    }

//    public static readonly object[][] TagsValidData =
//    [
//        [null!],
//        [Array.Empty<string>()]
//    ];

//    [Theory]
//    [Trait("Feature", "Validators")]
//    [MemberData(nameof(TagsValidData))]
//    public async Task Feature_Validator_Tags_ShouldPassValidationWhenNullOrEmptyArray(IEnumerable<string> tags)
//    {
//        // Arrange
//        BasePlayerDto player = PlayerTestConstants.GetValidPlayer();
//        player.Profile.General.Tags = tags;

//        // Act
//        ValidationResult result = await Validator.ValidateAsync(player);

//        // Assert
//        Assert.True(result.IsValid);
//        Assert.Empty(result.Errors);
//    }

//    public static readonly object[][] TagsInvalidData =
//    [
//        [new string[2] { "test", "test" }, "Each value from 'Tags' must be unique."],
//        [new string[51] { "test1", "test2", "test3", "test4", "test5", "test6", "test7", "test8", "test9", "test10",
//                                        "test11", "test12", "test13", "test14", "test15", "test16", "test17", "test18", "test19", "test20",
//                                        "test21", "test22", "test23", "test24", "test25", "test26", "test27", "test28", "test29", "test30",
//                                        "test31", "test32", "test33", "test34", "test35", "test36", "test37", "test38", "test39", "test40",
//                                        "test41", "test42", "test43", "test44", "test45", "test46", "test47", "test48", "test49", "test50", "test51"},
//            string.Format(Localization.InvalidTagsSize, nameof(PlayerGeneralProfileDto.Tags), ValidationConstants.TAGS_MAX_LENGTH) ],
//        [new string[2] { "test", "" }, "Each value from 'Tags' must not be empty.", "[1]"],
//        [new string[2] { "test", "123456789012345678901" }, "Each value from 'Tags' must be 20 characters or fewer. You entered 21 characters.", "[1]"]
//    ];

//    [Theory]
//    [Trait("Feature", "Validators")]
//    [MemberData(nameof(TagsInvalidData))]
//    public async Task Feature_Validator_Tags_ShouldFailValidationWhenHaveDuplicatesOrMoreThenMaxSizeOrHasEmptyTagOrHasTooLongTag(IEnumerable<string> tags, string errorMessage,
//        string? propName = null)
//    {
//        // Arrange
//        BasePlayerDto player = PlayerTestConstants.GetValidPlayer();
//        player.Profile.General.Tags = tags;

//        // Act
//        ValidationResult result = await Validator.ValidateAsync(player);

//        // Assert
//        Assert.False(result.IsValid);
//        Assert.Single(result.Errors);

//        ValidationFailure failure = result.Errors.First();

//        Assert.Equal(errorMessage, failure.ErrorMessage);
//        Assert.Equal($"Profile.General.Tags{propName}", failure.PropertyName);
//    }

//    public static readonly object[][] AvailabilityDaysValidData =
//    [
//        [null!],
//        [Array.Empty<DayOfWeek>()]
//    ];

//    [Theory]
//    [Trait("Feature", "Validators")]
//    [MemberData(nameof(AvailabilityDaysValidData))]
//    public async Task Feature_Validator_Availability_ShouldPassValidationWhenAvailabilityDaysIsNullOrEmptyArray(IEnumerable<DayOfWeek> days)
//    {
//        // Arrange
//        BasePlayerDto player = PlayerTestConstants.GetValidPlayer();
//        player.Profile.General.Availability = new PlayerAvailabilityDto
//        {
//            Days = days
//        };

//        // Act
//        ValidationResult result = await Validator.ValidateAsync(player);

//        // Assert
//        Assert.True(result.IsValid);
//        Assert.Empty(result.Errors);
//    }

//    public static readonly object[][] AvailabilityDaysInvalidData =
//    [
//        [ new DayOfWeek[8] { DayOfWeek.Monday, DayOfWeek.Monday, DayOfWeek.Monday, DayOfWeek.Monday, DayOfWeek.Monday,
//            DayOfWeek.Monday, DayOfWeek.Monday, DayOfWeek.Monday }, "The length of 'Days' must be less or equal to 7." ],
//        [new DayOfWeek[2] { DayOfWeek.Monday, (DayOfWeek)22 }, "Each value from 'Days' must be in Days of Week range.", "[1]"]
//    ];

//    [Theory]
//    [Trait("Feature", "Validators")]
//    [MemberData(nameof(AvailabilityDaysInvalidData))]
//    public async Task Feature_Validator_Availability_ShouldFailValidationWhenMoreThenMaxSizeOrHasInvalidDay(IEnumerable<DayOfWeek> days, string errorMessage,
//        string? propName = null)
//    {
//        // Arrange
//        BasePlayerDto player = PlayerTestConstants.GetValidPlayer();
//        player.Profile.General.Availability = new PlayerAvailabilityDto
//        {
//            Days = days
//        };

//        // Act
//        ValidationResult result = await Validator.ValidateAsync(player);

//        // Assert
//        Assert.False(result.IsValid);
//        Assert.Single(result.Errors);

//        ValidationFailure failure = result.Errors.First();

//        Assert.Equal(errorMessage, failure.ErrorMessage);
//        Assert.Equal($"Profile.General.Availability.Days{propName}", failure.PropertyName);
//    }

//    public static readonly object[][] AvailabilityFromToValidData =
//    [
//        [TimeSpan.FromHours(1), null!],
//        [null!, TimeSpan.FromHours(1)],
//        [null!, null!]
//    ];

//    [Theory]
//    [Trait("Feature", "Validators")]
//    [MemberData(nameof(AvailabilityFromToValidData))]
//    public async Task Feature_Validator_Availability_ShouldPassValidationWhenFromIsNullOrToIsNullOrBothIsNull(TimeSpan? from, TimeSpan? to)
//    {
//        // Arrange
//        BasePlayerDto player = PlayerTestConstants.GetValidPlayer();
//        player.Profile.General.Availability = new PlayerAvailabilityDto
//        {
//            From = from,
//            To = to
//        };

//        // Act
//        ValidationResult result = await Validator.ValidateAsync(player);

//        // Assert
//        Assert.True(result.IsValid);
//        Assert.Empty(result.Errors);
//    }

//    [Fact]
//    [Trait("Feature", "Validators")]
//    public async Task Feature_Validator_Availability_ShouldFailValidationWhenFromIsMoreThanToAndViceVersa()
//    {
//        // Arrange
//        BasePlayerDto player = PlayerTestConstants.GetValidPlayer();
//        player.Profile.General.Availability = new PlayerAvailabilityDto
//        {
//            From = TimeSpan.FromHours(2),
//            To = TimeSpan.FromHours(1)
//        };

//        // Act
//        ValidationResult result = await Validator.ValidateAsync(player);

//        // Assert
//        Assert.False(result.IsValid);
//        Assert.Equal(2, result.Errors.Count);

//        ValidationFailure toFailure = result.Errors.First();
//        ValidationFailure fromFailure = result.Errors[1];

//        Assert.Equal("'From' value must be less than To value.", fromFailure.ErrorMessage);
//        Assert.Equal($"Profile.General.Availability.From", fromFailure.PropertyName);
//        Assert.Equal("'To' value must be greater than From value.", toFailure.ErrorMessage);
//        Assert.Equal($"Profile.General.Availability.To", toFailure.PropertyName);
//    }

//    #endregion General profile

//    #region Football profile

//    [Fact]
//    [Trait("Feature", "Validators")]
//    public async Task Feature_Validator_Height_ShouldPassValidationWhenNull()
//    {
//        // Arrange
//        BasePlayerDto player = PlayerTestConstants.GetValidPlayer();
//        player.Profile.Football.Height = null;

//        // Act
//        ValidationResult result = await Validator.ValidateAsync(player);

//        // Assert
//        Assert.True(result.IsValid);
//        Assert.Empty(result.Errors);
//    }

//    [Theory]
//    [Trait("Feature", "Validators")]
//    [InlineData(0)]
//    [InlineData(301)]
//    public async Task Feature_Validator_Height_ShouldFailValidationWhenNotFitLimits(int height)
//    {
//        // Arrange
//        BasePlayerDto player = PlayerTestConstants.GetValidPlayer();
//        player.Profile.Football.Height = height;

//        // Act
//        ValidationResult result = await Validator.ValidateAsync(player);

//        // Assert
//        Assert.False(result.IsValid);
//        Assert.Single(result.Errors);

//        ValidationFailure failure = result.Errors.First();

//        Assert.Equal($"'Height' must be between 1 and 300. You entered {height}.", failure.ErrorMessage);
//        Assert.Equal("Profile.Football.Height", failure.PropertyName);
//    }

//    [Fact]
//    [Trait("Feature", "Validators")]
//    public async Task Feature_Validator_Weight_ShouldPassValidationWhenNull()
//    {
//        // Arrange
//        BasePlayerDto player = PlayerTestConstants.GetValidPlayer();
//        player.Profile.Football.Weight = null;

//        // Act
//        ValidationResult result = await Validator.ValidateAsync(player);

//        // Assert
//        Assert.True(result.IsValid);
//        Assert.Empty(result.Errors);
//    }

//    [Theory]
//    [Trait("Feature", "Validators")]
//    [InlineData(0)]
//    [InlineData(301)]
//    public async Task Feature_Validator_Weight_ShouldFailValidationWhenNotFitLimits(int weight)
//    {
//        // Arrange
//        BasePlayerDto player = PlayerTestConstants.GetValidPlayer();
//        player.Profile.Football.Weight = weight;

//        // Act
//        ValidationResult result = await Validator.ValidateAsync(player);

//        // Assert
//        Assert.False(result.IsValid);
//        Assert.Single(result.Errors);

//        ValidationFailure failure = result.Errors.First();

//        Assert.Equal($"'Weight' must be between 1 and 300. You entered {weight}.", failure.ErrorMessage);
//        Assert.Equal("Profile.Football.Weight", failure.PropertyName);
//    }

//    [Fact]
//    [Trait("Feature", "Validators")]
//    public async Task Feature_Validator_Position_ShouldPassValidationWhenNull()
//    {
//        // Arrange
//        BasePlayerDto player = PlayerTestConstants.GetValidPlayer();
//        player.Profile.Football.Position = null;

//        // Act
//        ValidationResult result = await Validator.ValidateAsync(player);

//        // Assert
//        Assert.True(result.IsValid);
//        Assert.Empty(result.Errors);
//    }

//    [Fact]
//    [Trait("Feature", "Validators")]
//    public async Task Feature_Validator_Position_ShouldFailValidationWhenHasInvalidValue()
//    {
//        // Arrange
//        _footballPositionRepository.Setup(r => r.AnyAsync(It.IsAny<int>())).ReturnsAsync(false);
//        BasePlayerDto player = PlayerTestConstants.GetValidPlayer();
//        player.Profile.Football.Position = 22;

//        // Act
//        ValidationResult result = await Validator.ValidateAsync(player);

//        // Assert
//        Assert.False(result.IsValid);
//        Assert.Single(result.Errors);

//        ValidationFailure failure = result.Errors.First();

//        Assert.Equal("'Position' has a range of values which does not include '22'.", failure.ErrorMessage);
//        Assert.Equal("Profile.Football.Position", failure.PropertyName);
//    }

//    [Fact]
//    [Trait("Feature", "Validators")]
//    public async Task Feature_Validator_Position_ShouldFailValidationWhenEqualToAdditionalPosition()
//    {
//        // Arrange
//        BasePlayerDto player = PlayerTestConstants.GetValidPlayer();
//        player.Profile.Football.Position = 3;
//        player.Profile.Football.AdditionalPosition = 3;

//        // Act
//        ValidationResult result = await Validator.ValidateAsync(player);

//        // Assert
//        Assert.False(result.IsValid);
//        Assert.Equal(2, result.Errors.Count);

//        ValidationFailure failure = result.Errors.First();

//        Assert.Equal("'Position' must not to be equal to 'AdditionalPosition'.", failure.ErrorMessage);
//        Assert.Equal("Profile.Football.Position", failure.PropertyName);
//    }

//    [Fact]
//    [Trait("Feature", "Validators")]
//    public async Task Feature_Validator_AdditionalPosition_ShouldPassValidationWhenNull()
//    {
//        // Arrange
//        _footballPositionRepository.Setup(r => r.AnyAsync(It.IsAny<int>())).ReturnsAsync(false);
//        BasePlayerDto player = PlayerTestConstants.GetValidPlayer();
//        player.Profile.Football.AdditionalPosition = null;

//        // Act
//        ValidationResult result = await Validator.ValidateAsync(player);

//        // Assert
//        Assert.True(result.IsValid);
//        Assert.Empty(result.Errors);
//    }

//    [Fact]
//    [Trait("Feature", "Validators")]
//    public async Task Feature_Validator_AdditionalPosition_ShouldFailValidationWhenHasInvalidValue()
//    {
//        // Arrange
//        _footballPositionRepository.Setup(r => r.AnyAsync(It.IsAny<int>())).ReturnsAsync(false);
//        BasePlayerDto player = PlayerTestConstants.GetValidPlayer();
//        player.Profile.Football.AdditionalPosition = 22;

//        // Act
//        ValidationResult result = await Validator.ValidateAsync(player);

//        // Assert
//        Assert.False(result.IsValid);
//        Assert.Single(result.Errors);

//        ValidationFailure failure = result.Errors.First();

//        Assert.Equal("'AdditionalPosition' has a range of values which does not include '22'.", failure.ErrorMessage);
//        Assert.Equal("Profile.Football.AdditionalPosition", failure.PropertyName);
//    }

//    [Fact]
//    [Trait("Feature", "Validators")]
//    public async Task Feature_Validator_AdditionalPosition_ShouldFailValidationWhenEqualToPosition()
//    {
//        // Arrange
//        BasePlayerDto player = PlayerTestConstants.GetValidPlayer();
//        player.Profile.Football.AdditionalPosition = 3;
//        player.Profile.Football.Position = 3;

//        // Act
//        ValidationResult result = await Validator.ValidateAsync(player);

//        // Assert
//        Assert.False(result.IsValid);
//        Assert.Equal(2, result.Errors.Count);

//        ValidationFailure failure = result.Errors[1];

//        Assert.Equal("'AdditionalPosition' must not to be equal to 'Position'.", failure.ErrorMessage);
//        Assert.Equal("Profile.Football.AdditionalPosition", failure.PropertyName);
//    }

//    [Fact]
//    [Trait("Feature", "Validators")]
//    public async Task Feature_Validator_WorkingFoot_ShouldPassValidationWhenNull()
//    {
//        // Arrange
//        _workingFootRepository.Setup(r => r.AnyAsync(It.IsAny<int>())).ReturnsAsync(false);
//        BasePlayerDto player = PlayerTestConstants.GetValidPlayer();
//        player.Profile.Football.WorkingFoot = null;

//        // Act
//        ValidationResult result = await Validator.ValidateAsync(player);

//        // Assert
//        Assert.True(result.IsValid);
//        Assert.Empty(result.Errors);
//    }

//    [Fact]
//    [Trait("Feature", "Validators")]
//    public async Task Feature_Validator_WorkingFoot_ShouldFailValidationWhenHasInvalidValue()
//    {
//        // Arrange
//        _workingFootRepository.Setup(r => r.AnyAsync(It.IsAny<int>())).ReturnsAsync(false);
//        BasePlayerDto player = PlayerTestConstants.GetValidPlayer();
//        player.Profile.Football.WorkingFoot = 22;

//        // Act
//        ValidationResult result = await Validator.ValidateAsync(player);

//        // Assert
//        Assert.False(result.IsValid);
//        Assert.Single(result.Errors);

//        ValidationFailure failure = result.Errors.First();

//        Assert.Equal("'WorkingFoot' has a range of values which does not include '22'.", failure.ErrorMessage);
//        Assert.Equal("Profile.Football.WorkingFoot", failure.PropertyName);
//    }

//    [Fact]
//    [Trait("Feature", "Validators")]
//    public async Task Feature_Validator_Number_ShouldPassValidationWhenNull()
//    {
//        // Arrange
//        BasePlayerDto player = PlayerTestConstants.GetValidPlayer();
//        player.Profile.Football.Number = null;

//        // Act
//        ValidationResult result = await Validator.ValidateAsync(player);

//        // Assert
//        Assert.True(result.IsValid);
//        Assert.Empty(result.Errors);
//    }

//    [Theory]
//    [Trait("Feature", "Validators")]
//    [InlineData(-1)]
//    [InlineData(100)]
//    public async Task Feature_Validator_Number_ShouldFailValidationWhenNotFitLimits(int number)
//    {
//        // Arrange
//        BasePlayerDto player = PlayerTestConstants.GetValidPlayer();
//        player.Profile.Football.Number = number;

//        // Act
//        ValidationResult result = await Validator.ValidateAsync(player);

//        // Assert
//        Assert.False(result.IsValid);
//        Assert.Single(result.Errors);

//        ValidationFailure failure = result.Errors.First();

//        Assert.Equal($"'Number' must be between 0 and 99. You entered {number}.", failure.ErrorMessage);
//        Assert.Equal("Profile.Football.Number", failure.PropertyName);
//    }

//    [Fact]
//    [Trait("Feature", "Validators")]
//    public async Task Feature_Validator_GameStyle_ShouldPassValidationWhenNull()
//    {
//        // Arrange
//        _gameStyleRepository.Setup(r => r.AnyAsync(It.IsAny<int>())).ReturnsAsync(false);
//        BasePlayerDto player = PlayerTestConstants.GetValidPlayer();
//        player.Profile.Football.GameStyle = null;

//        // Act
//        ValidationResult result = await Validator.ValidateAsync(player);

//        // Assert
//        Assert.True(result.IsValid);
//        Assert.Empty(result.Errors);
//    }

//    [Fact]
//    [Trait("Feature", "Validators")]
//    public async Task Feature_Validator_GameStyle_ShouldFailValidationWhenHasInvalidValue()
//    {
//        // Arrange
//        _gameStyleRepository.Setup(r => r.AnyAsync(It.IsAny<int>())).ReturnsAsync(false);
//        BasePlayerDto player = PlayerTestConstants.GetValidPlayer();
//        player.Profile.Football.GameStyle = 22;

//        // Act
//        ValidationResult result = await Validator.ValidateAsync(player);

//        // Assert
//        Assert.False(result.IsValid);
//        Assert.Single(result.Errors);

//        ValidationFailure failure = result.Errors.First();

//        Assert.Equal("'GameStyle' has a range of values which does not include '22'.", failure.ErrorMessage);
//        Assert.Equal("Profile.Football.GameStyle", failure.PropertyName);
//    }

//    [Fact]
//    [Trait("Feature", "Validators")]
//    public async Task Feature_Validator_Skill_ShouldPassValidationWhenNull()
//    {
//        // Arrange
//        BasePlayerDto player = PlayerTestConstants.GetValidPlayer();
//        player.Profile.Football.Skill = null;

//        // Act
//        ValidationResult result = await Validator.ValidateAsync(player);

//        // Assert
//        Assert.True(result.IsValid);
//        Assert.Empty(result.Errors);
//    }

//    [Theory]
//    [Trait("Feature", "Validators")]
//    [InlineData(-1)]
//    [InlineData(6)]
//    public async Task Feature_Validator_Skill_ShouldFailValidationWhenNotFitLimits(int skill)
//    {
//        // Arrange
//        BasePlayerDto player = PlayerTestConstants.GetValidPlayer();
//        player.Profile.Football.Skill = skill;

//        // Act
//        ValidationResult result = await Validator.ValidateAsync(player);

//        // Assert
//        Assert.False(result.IsValid);
//        Assert.Single(result.Errors);

//        ValidationFailure failure = result.Errors.First();

//        Assert.Equal($"'Skill' must be between 0 and 5. You entered {skill}.", failure.ErrorMessage);
//        Assert.Equal("Profile.Football.Skill", failure.PropertyName);
//    }

//    [Fact]
//    [Trait("Feature", "Validators")]
//    public async Task Feature_Validator_WeakFoot_ShouldPassValidationWhenNull()
//    {
//        // Arrange
//        BasePlayerDto player = PlayerTestConstants.GetValidPlayer();
//        player.Profile.Football.WeakFoot = null;

//        // Act
//        ValidationResult result = await Validator.ValidateAsync(player);

//        // Assert
//        Assert.True(result.IsValid);
//        Assert.Empty(result.Errors);
//    }

//    [Theory]
//    [Trait("Feature", "Validators")]
//    [InlineData(-1)]
//    [InlineData(6)]
//    public async Task Feature_Validator_WeakFoot_ShouldFailValidationWhenNotFitLimits(int skill)
//    {
//        // Arrange
//        BasePlayerDto player = PlayerTestConstants.GetValidPlayer();
//        player.Profile.Football.WeakFoot = skill;

//        // Act
//        ValidationResult result = await Validator.ValidateAsync(player);

//        // Assert
//        Assert.False(result.IsValid);
//        Assert.Single(result.Errors);

//        ValidationFailure failure = result.Errors.First();

//        Assert.Equal($"'WeakFoot' must be between 0 and 5. You entered {skill}.", failure.ErrorMessage);
//        Assert.Equal("Profile.Football.WeakFoot", failure.PropertyName);
//    }

//    [Fact]
//    [Trait("Feature", "Validators")]
//    public async Task Feature_Validator_PhysicalCondition_ShouldPassValidationWhenNull()
//    {
//        // Arrange
//        BasePlayerDto player = PlayerTestConstants.GetValidPlayer();
//        player.Profile.Football.PhysicalCondition = null;

//        // Act
//        ValidationResult result = await Validator.ValidateAsync(player);

//        // Assert
//        Assert.True(result.IsValid);
//        Assert.Empty(result.Errors);
//    }

//    [Theory]
//    [Trait("Feature", "Validators")]
//    [InlineData(-1)]
//    [InlineData(6)]
//    public async Task Feature_Validator_PhysicalCondition_ShouldFailValidationWhenNotFitLimits(int skill)
//    {
//        // Arrange
//        BasePlayerDto player = PlayerTestConstants.GetValidPlayer();
//        player.Profile.Football.PhysicalCondition = skill;

//        // Act
//        ValidationResult result = await Validator.ValidateAsync(player);

//        // Assert
//        Assert.False(result.IsValid);
//        Assert.Single(result.Errors);

//        ValidationFailure failure = result.Errors.First();

//        Assert.Equal($"'PhysicalCondition' must be between 0 and 5. You entered {skill}.", failure.ErrorMessage);
//        Assert.Equal("Profile.Football.PhysicalCondition", failure.PropertyName);
//    }

//    #endregion Football profile

//    #region Stats

//    [Fact]
//    [Trait("Feature", "Validators")]
//    public async Task Feature_Validator_Points_Available_ShouldFailValidationWhenLessThanZero()
//    {
//        // Arrange
//        BasePlayerDto player = PlayerTestConstants.GetValidPlayer();
//        player.Stats.Points = new PlayerStatPointsDto
//        {
//            Available = -1
//        };

//        // Act
//        ValidationResult result = await Validator.ValidateAsync(player);

//        // Assert
//        Assert.False(result.IsValid);
//        Assert.Single(result.Errors);

//        ValidationFailure failure = result.Errors.First();

//        Assert.Equal("'Available' must be greater than or equal to '0'.", failure.ErrorMessage);
//        Assert.Equal($"Stats.Points.Available", failure.PropertyName);
//    }

//    [Fact]
//    [Trait("Feature", "Validators")]
//    public async Task Feature_Validator_Points_Used_ShouldFailValidationWhenLessThanZero()
//    {
//        // Arrange
//        BasePlayerDto player = PlayerTestConstants.GetValidPlayer();
//        player.Stats.Points = new PlayerStatPointsDto
//        {
//            Used = -1
//        };

//        // Act
//        ValidationResult result = await Validator.ValidateAsync(player);

//        // Assert
//        Assert.False(result.IsValid);
//        Assert.Single(result.Errors);

//        ValidationFailure failure = result.Errors.First();

//        Assert.Equal("'Used' must be greater than or equal to '0'.", failure.ErrorMessage);
//        Assert.Equal($"Stats.Points.Used", failure.PropertyName);
//    }    

//    [Theory]
//    [Trait("Feature", "Validators")]
//    [InlineData(102)]
//    [InlineData(101)]
//    public async Task Feature_Validator_Stat_Values_ShouldFailValidationWhenValueIsNotFitLimits(byte value)
//    {
//        // Arrange
//        BasePlayerDto player = JsonSerializer.Deserialize<BasePlayerDto>(JsonSerializer.Serialize(PlayerTestConstants.GetValidPlayer()))!;
//        player.Stats.Values.ToList()[0].Value = value;

//        // Act
//        ValidationResult result = await Validator.ValidateAsync(player);

//        // Assert
//        Assert.False(result.IsValid);
//        Assert.Single(result.Errors);

//        ValidationFailure failure = result.Errors.First();

//        Assert.Equal("Each value from 'Stats' must have Value between 0 and 100.", failure.ErrorMessage);
//        Assert.Equal($"Stats.Values[0]", failure.PropertyName);
//    }

//    #endregion Stats
//}
