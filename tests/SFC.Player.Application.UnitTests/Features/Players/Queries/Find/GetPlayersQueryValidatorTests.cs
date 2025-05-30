﻿//using System.ComponentModel.DataAnnotations;

//using FluentValidation.Results;

//using SFC.Player.Application.Common.Constants;
//using SFC.Player.Application.Features.Common.Dto;
//using SFC.Player.Application.Features.Players.Common.Dto;
//using SFC.Player.Application.Features.Players.Queries.Find;
//using SFC.Player.Application.Features.Players.Queries.Find.Dto.Filters;

//using ValidationResult = FluentValidation.Results.ValidationResult;

//namespace SFC.Player.Application.UnitTests.Features.Player.Queries.Find;
//public class GetPlayersQueryValidatorTests
//{
//    #region General

//    [Fact]
//    [Trait("Feature", "Validators")]
//    public async Task Feature_Validator_GetPlayers_Name_ShouldFailValidationWhenTooLong()
//    {
//        // Arrange
//        GetPlayersQuery query = new()
//        {
//            Filter = new GetPlayersFilterDto
//            {
//                Profile = new GetPlayersProfileFilterDto
//                {
//                    General = new GetPlayersGeneralProfileFilterDto
//                    {
//                        Name = "tucxrlbkhvdrzgzxvtxosapnwhgnhclrbiafabwytkgpikytuzvjsowcbnrzpyrrimbpwklqvijyvlfoybzlfrnmdoklxdlddbhwmgvfczjvunwitbkxohcupnhqppnxrrwzwhaidivbrsliytlftrt"
//                    }
//                }
//            }
//        };

//        // Act
//        ValidationResult result = await new GetPlayersQueryValidator().ValidateAsync(query);

//        // Assert
//        Assert.False(result.IsValid);
//        Assert.Single(result.Errors);

//        ValidationFailure failure = result.Errors.First();

//        Assert.Equal("The length of 'Name' must be 150 characters or fewer. You entered 151 characters.", failure.ErrorMessage);
//        Assert.Equal("Filter.Profile.General.Name", failure.PropertyName);
//    }

//    [Fact]
//    [Trait("Feature", "Validators")]
//    public async Task Feature_Validator_GetPlayers_City_ShouldFailValidationWhenTooLong()
//    {
//        // Arrange
//        GetPlayersQuery query = new()
//        {
//            Filter = new GetPlayersFilterDto
//            {
//                Profile = new GetPlayersProfileFilterDto
//                {
//                    General = new GetPlayersGeneralProfileFilterDto
//                    {
//                        City = "tucxrlbkhvdrzgzxvtxosapnwhgnhclrbiafabwytkgpikytuzvjsowcbnrzpyrrimbpwklqvijyvlfoybzlfrnmdoklxdlddbhwmgvfczjvunwitbkxohcupnhqppnxrrwzwhaidivbrsliytlftrt"
//                    }
//                }
//            }
//        };

//        // Act
//        ValidationResult result = await new GetPlayersQueryValidator().ValidateAsync(query);

//        // Assert
//        Assert.False(result.IsValid);
//        Assert.Single(result.Errors);

//        ValidationFailure failure = result.Errors.First();

//        Assert.Equal("The length of 'City' must be 100 characters or fewer. You entered 151 characters.", failure.ErrorMessage);
//        Assert.Equal("Filter.Profile.General.City", failure.PropertyName);
//    }

//    [Fact]
//    [Trait("Feature", "Validators")]
//    public async Task Feature_Validator_GetPlayers_Tags_ShouldPassValidationWhenNullOrEmptyArray()
//    {
//        // Arrange
//        GetPlayersQuery query = new()
//        {
//            Filter = new GetPlayersFilterDto
//            {
//                Profile = new GetPlayersProfileFilterDto
//                {
//                    General = new GetPlayersGeneralProfileFilterDto
//                    {
//                        Tags = Array.Empty<string>()
//                    }
//                }
//            }
//        };

//        // Act
//        ValidationResult result = await new GetPlayersQueryValidator().ValidateAsync(query);

//        // Assert
//        Assert.True(result.IsValid);
//        Assert.Empty(result.Errors);
//    }

//    public static readonly object[][] TagsInvalidData =
//    {
//        new object[] { new string[2] { "test", "test" }, "Each value from 'Tags' must be unique." },
//        new object[] { new string[51] { "test1", "test2", "test3", "test4", "test5", "test6", "test7", "test8", "test9", "test10",
//                                        "test11", "test12", "test13", "test14", "test15", "test16", "test17", "test18", "test19", "test20",
//                                        "test21", "test22", "test23", "test24", "test25", "test26", "test27", "test28", "test29", "test30",
//                                        "test31", "test32", "test33", "test34", "test35", "test36", "test37", "test38", "test39", "test40",
//                                        "test41", "test42", "test43", "test44", "test45", "test46", "test47", "test48", "test49", "test50", "test51"},
//            string.Format(Localization.InvalidTagsSize, nameof(PlayerGeneralProfileDto.Tags), ValidationConstants.TAGS_MAX_LENGTH) },
//        new object[] { new string[2] { "test", "" }, "Each value from 'Tags' must not be empty.", "[1]" },
//        new object[] { new string[2] { "test", "123456789012345678901" }, "Each value from 'Tags' must be 20 characters or fewer. You entered 21 characters.", "[1]" }
//    };

//    [Theory]
//    [Trait("Feature", "Validators")]
//    [MemberData(nameof(TagsInvalidData))]
//    public async Task Feature_Validator_GetPlayers_Tags_ShouldFailValidationWhenMoreThenMaxSizeOrHaveDuplicatesOrHasEmptyTagOrHasTooLongTag(IEnumerable<string> tags, string errorMessage,
//        string? propName = null)
//    {
//        // Arrange
//        GetPlayersQuery query = new()
//        {
//            Filter = new GetPlayersFilterDto
//            {
//                Profile = new GetPlayersProfileFilterDto
//                {
//                    General = new GetPlayersGeneralProfileFilterDto
//                    {
//                        Tags = tags
//                    }
//                }
//            }
//        };

//        // Act
//        ValidationResult result = await new GetPlayersQueryValidator().ValidateAsync(query);

//        // Assert
//        Assert.False(result.IsValid);
//        Assert.Single(result.Errors);

//        ValidationFailure failure = result.Errors.First();

//        Assert.Equal(errorMessage, failure.ErrorMessage);
//        Assert.Equal($"Filter.Profile.General.Tags{propName}", failure.PropertyName);
//    }

//    public static readonly object[][] AvailabilityDaysValidData =
//    {
//        new object[] { null! },
//        new object[] { Array.Empty<DayOfWeek>() }
//    };

//    [Theory]
//    [Trait("Feature", "Validators")]
//    [MemberData(nameof(AvailabilityDaysValidData))]
//    public async Task Feature_Validator_GetPlayers_Availability_ShouldPassValidationWhenAvailabilityDaysIsNullOrEmptyArray(IEnumerable<DayOfWeek> days)
//    {
//        // Arrange
//        GetPlayersQuery query = new()
//        {
//            Filter = new GetPlayersFilterDto
//            {
//                Profile = new GetPlayersProfileFilterDto
//                {
//                    General = new GetPlayersGeneralProfileFilterDto
//                    {
//                        Availability = new GetPlayersAvailabilityLimitDto { Days = days }
//                    }
//                }
//            }
//        };

//        // Act
//        ValidationResult result = await new GetPlayersQueryValidator().ValidateAsync(query);

//        // Assert
//        Assert.True(result.IsValid);
//        Assert.Empty(result.Errors);
//    }

//    public static readonly object[][] AvailabilityDaysInvalidData =
//    {
//        new object[] { new DayOfWeek[8] { DayOfWeek.Monday, DayOfWeek.Monday, DayOfWeek.Monday, DayOfWeek.Monday, DayOfWeek.Monday,
//            DayOfWeek.Monday, DayOfWeek.Monday, DayOfWeek.Monday }, "The length of 'Days' must be less or equal to 7." },
//        new object[] { new DayOfWeek[2] { DayOfWeek.Monday, (DayOfWeek)22 }, "Each value from 'Days' must be in Days of Week range.", "[1]" }
//    };

//    [Theory]
//    [Trait("Feature", "Validators")]
//    [MemberData(nameof(AvailabilityDaysInvalidData))]
//    public async Task Feature_Validator_GetPlayers_Availability_ShouldFailValidationWhenMoreThenMaxSizeOrHasInvalidDay(IEnumerable<DayOfWeek> days, string errorMessage,
//        string? propName = null)
//    {
//        // Arrange
//        GetPlayersQuery query = new()
//        {
//            Filter = new GetPlayersFilterDto
//            {
//                Profile = new GetPlayersProfileFilterDto
//                {
//                    General = new GetPlayersGeneralProfileFilterDto
//                    {
//                        Availability = new GetPlayersAvailabilityLimitDto { Days = days }
//                    }
//                }
//            }
//        };

//        // Act
//        ValidationResult result = await new GetPlayersQueryValidator().ValidateAsync(query);

//        // Assert
//        Assert.False(result.IsValid);
//        Assert.Single(result.Errors);

//        ValidationFailure failure = result.Errors.First();

//        Assert.Equal(errorMessage, failure.ErrorMessage);
//        Assert.Equal($"Filter.Profile.General.Availability.Days{propName}", failure.PropertyName);
//    }

//    public static readonly object[][] AvailabilityFromToValidData =
//    {
//        new object[] { TimeSpan.FromHours(1), null! },
//        new object[] { null!, TimeSpan.FromHours(1) },
//        new object[] { null!, null! }
//    };

//    [Theory]
//    [Trait("Feature", "Validators")]
//    [MemberData(nameof(AvailabilityFromToValidData))]
//    public async Task Feature_Validator_GetPlayers_Availability_ShouldPassValidationWhenFromIsNullOrToIsNullOrBothIsNull(TimeSpan? from, TimeSpan? to)
//    {
//        // Arrange
//        GetPlayersQuery query = new()
//        {
//            Filter = new GetPlayersFilterDto
//            {
//                Profile = new GetPlayersProfileFilterDto
//                {
//                    General = new GetPlayersGeneralProfileFilterDto
//                    {
//                        Availability = new GetPlayersAvailabilityLimitDto { From = from, To = to }
//                    }
//                }
//            }
//        };

//        // Act
//        ValidationResult result = await new GetPlayersQueryValidator().ValidateAsync(query);

//        // Assert
//        Assert.True(result.IsValid);
//        Assert.Empty(result.Errors);
//    }

//    [Fact]
//    [Trait("Feature", "Validators")]
//    public async Task Feature_Validator_GetPlayers_Availability_ShouldFailValidationWhenFromIsMoreThanToAndViceVersa()
//    {
//        // Arrange
//        GetPlayersQuery query = new()
//        {
//            Filter = new GetPlayersFilterDto
//            {
//                Profile = new GetPlayersProfileFilterDto
//                {
//                    General = new GetPlayersGeneralProfileFilterDto
//                    {
//                        Availability = new GetPlayersAvailabilityLimitDto { From = TimeSpan.FromHours(2), To = TimeSpan.FromHours(1) }
//                    }
//                }
//            }
//        };

//        // Act
//        ValidationResult result = await new GetPlayersQueryValidator().ValidateAsync(query);

//        // Assert
//        Assert.False(result.IsValid);
//        Assert.Equal(2, result.Errors.Count);

//        ValidationFailure toFailure = result.Errors.First();
//        ValidationFailure fromFailure = result.Errors[1];

//        Assert.Equal("'From' value must be less than To value.", fromFailure.ErrorMessage);
//        Assert.Equal($"Filter.Profile.General.Availability.From", fromFailure.PropertyName);
//        Assert.Equal("'To' value must be greater than From value.", toFailure.ErrorMessage);
//        Assert.Equal($"Filter.Profile.General.Availability.To", toFailure.PropertyName);
//    }

//    public static readonly object[][] YearsFromToValidData =
//    {
//        new object[] { (short)18, null! },
//        new object[] { null!, (short)60 },
//        new object[] { null!, null! }
//    };

//    [Theory]
//    [Trait("Feature", "Validators")]
//    [MemberData(nameof(YearsFromToValidData))]
//    public async Task Feature_Validator_GetPlayers_Years_ShouldPassValidationWhenFromIsNullOrToIsNullOrBothIsNull(short? from, short? to)
//    {
//        // Arrange
//        GetPlayersQuery query = new()
//        {
//            Filter = new GetPlayersFilterDto
//            {
//                Profile = new GetPlayersProfileFilterDto
//                {
//                    General = new GetPlayersGeneralProfileFilterDto
//                    {
//                        Years = new RangeLimitDto<short?> { From = from!, To = to! }
//                    }
//                }
//            }
//        };

//        // Act
//        ValidationResult result = await new GetPlayersQueryValidator().ValidateAsync(query);

//        // Assert
//        Assert.True(result.IsValid);
//        Assert.Empty(result.Errors);
//    }

//    [Fact]
//    [Trait("Feature", "Validators")]
//    public async Task Feature_Validator_GetPlayers_Years_ShouldFailValidationWhenFromIsMoreThanToAndViceVersa()
//    {
//        // Arrange
//        GetPlayersQuery query = new()
//        {
//            Filter = new GetPlayersFilterDto
//            {
//                Profile = new GetPlayersProfileFilterDto
//                {
//                    General = new GetPlayersGeneralProfileFilterDto
//                    {
//                        Years = new RangeLimitDto<short?> { From = 60, To = 18 }
//                    }
//                }
//            }
//        };

//        // Act
//        ValidationResult result = await new GetPlayersQueryValidator().ValidateAsync(query);

//        // Assert
//        Assert.False(result.IsValid);
//        Assert.Equal(2, result.Errors.Count);

//        ValidationFailure toFailure = result.Errors.First();
//        ValidationFailure fromFailure = result.Errors[1];

//        Assert.Equal("'From' value must be less than To value.", fromFailure.ErrorMessage);
//        Assert.Equal($"Filter.Profile.General.Years.From", fromFailure.PropertyName);
//        Assert.Equal("'To' value must be greater than From value.", toFailure.ErrorMessage);
//        Assert.Equal($"Filter.Profile.General.Years.To", toFailure.PropertyName);
//    }

//    #endregion General

//    #region Football

//    public static readonly object[][] HeightFromToValidData =
//    {
//        new object[] { (short)160, null! },
//        new object[] { null!, (short)210 },
//        new object[] { null!, null! }
//    };

//    [Theory]
//    [Trait("Feature", "Validators")]
//    [MemberData(nameof(HeightFromToValidData))]
//    public async Task Feature_Validator_GetPlayers_Height_ShouldPassValidationWhenFromIsNullOrToIsNullOrBothIsNull(short? from, short? to)
//    {
//        // Arrange
//        GetPlayersQuery query = new()
//        {
//            Filter = new GetPlayersFilterDto
//            {
//                Profile = new GetPlayersProfileFilterDto
//                {
//                    Football = new GetPlayersFootballProfileFilterDto
//                    {
//                        Height = new RangeLimitDto<short?> { From = from, To = to }
//                    }
//                }
//            }
//        };

//        // Act
//        ValidationResult result = await new GetPlayersQueryValidator().ValidateAsync(query);

//        // Assert
//        Assert.True(result.IsValid);
//        Assert.Empty(result.Errors);
//    }

//    [Fact]
//    [Trait("Feature", "Validators")]
//    public async Task Feature_Validator_GetPlayers_Height_ShouldFailValidationWhenFromIsMoreThanToAndViceVersa()
//    {
//        // Arrange
//        GetPlayersQuery query = new()
//        {
//            Filter = new GetPlayersFilterDto
//            {
//                Profile = new GetPlayersProfileFilterDto
//                {
//                    Football = new GetPlayersFootballProfileFilterDto
//                    {
//                        Height = new RangeLimitDto<short?> { From = 200, To = 150 }
//                    }
//                }
//            }
//        };

//        // Act
//        ValidationResult result = await new GetPlayersQueryValidator().ValidateAsync(query);

//        // Assert
//        Assert.False(result.IsValid);
//        Assert.Equal(2, result.Errors.Count);

//        ValidationFailure toFailure = result.Errors.First();
//        ValidationFailure fromFailure = result.Errors[1];

//        Assert.Equal("'From' value must be less than To value.", fromFailure.ErrorMessage);
//        Assert.Equal($"Filter.Profile.Football.Height.From", fromFailure.PropertyName);
//        Assert.Equal("'To' value must be greater than From value.", toFailure.ErrorMessage);
//        Assert.Equal($"Filter.Profile.Football.Height.To", toFailure.PropertyName);
//    }

//    public static readonly object[][] WeightFromToValidData =
//    {
//        new object[] { (short)160, null! },
//        new object[] { null!, (short)210 },
//        new object[] { null!, null! }
//    };

//    [Theory]
//    [Trait("Feature", "Validators")]
//    [MemberData(nameof(WeightFromToValidData))]
//    public async Task Feature_Validator_GetPlayers_Weight_ShouldPassValidationWhenFromIsNullOrToIsNullOrBothIsNull(short? from, short? to)
//    {
//        // Arrange
//        GetPlayersQuery query = new()
//        {
//            Filter = new GetPlayersFilterDto
//            {
//                Profile = new GetPlayersProfileFilterDto
//                {
//                    Football = new GetPlayersFootballProfileFilterDto
//                    {
//                        Weight = new RangeLimitDto<short?> { From = from, To = to }
//                    }
//                }
//            }
//        };

//        // Act
//        ValidationResult result = await new GetPlayersQueryValidator().ValidateAsync(query);

//        // Assert
//        Assert.True(result.IsValid);
//        Assert.Empty(result.Errors);
//    }

//    [Fact]
//    [Trait("Feature", "Validators")]
//    public async Task Feature_Validator_GetPlayers_Weight_ShouldFailValidationWhenFromIsMoreThanToAndViceVersa()
//    {
//        // Arrange
//        GetPlayersQuery query = new()
//        {
//            Filter = new GetPlayersFilterDto
//            {
//                Profile = new GetPlayersProfileFilterDto
//                {
//                    Football = new GetPlayersFootballProfileFilterDto
//                    {
//                        Weight = new RangeLimitDto<short?> { From = 200, To = 65 }
//                    }
//                }
//            }
//        };

//        // Act
//        ValidationResult result = await new GetPlayersQueryValidator().ValidateAsync(query);

//        // Assert
//        Assert.False(result.IsValid);
//        Assert.Equal(2, result.Errors.Count);

//        ValidationFailure toFailure = result.Errors.First();
//        ValidationFailure fromFailure = result.Errors[1];

//        Assert.Equal("'From' value must be less than To value.", fromFailure.ErrorMessage);
//        Assert.Equal($"Filter.Profile.Football.Weight.From", fromFailure.PropertyName);
//        Assert.Equal("'To' value must be greater than From value.", toFailure.ErrorMessage);
//        Assert.Equal($"Filter.Profile.Football.Weight.To", toFailure.PropertyName);
//    }

//    #endregion Football

//    #region Stats

//    public static readonly object[][] TotalStatsFromToValidData =
//    {
//        new object[] { (short)10, null! },
//        new object[] { null!, (short)89 },
//        new object[] { null!, null! }
//    };

//    [Theory]
//    [Trait("Feature", "Validators")]
//    [MemberData(nameof(TotalStatsFromToValidData))]
//    public async Task Feature_Validator_GetPlayers_TotalStats_ShouldPassValidationWhenFromIsNullOrToIsNullOrBothIsNull(short? from, short? to)
//    {
//        // Arrange
//        GetPlayersQuery query = new()
//        {
//            Filter = new GetPlayersFilterDto
//            {
//                Stats = new GetPlayersStatsFilterDto
//                {
//                    Total = new RangeLimitDto<short?> { From = from, To = to }
//                }
//            }
//        };

//        // Act
//        ValidationResult result = await new GetPlayersQueryValidator().ValidateAsync(query);

//        // Assert
//        Assert.True(result.IsValid);
//        Assert.Empty(result.Errors);
//    }

//    [Fact]
//    [Trait("Feature", "Validators")]
//    public async Task Feature_Validator_GetPlayers_TotalStats_ShouldFailValidationWhenFromIsMoreThanToAndViceVersa()
//    {
//        // Arrange
//        GetPlayersQuery query = new()
//        {
//            Filter = new GetPlayersFilterDto
//            {
//                Stats = new GetPlayersStatsFilterDto
//                {
//                    Total = new RangeLimitDto<short?> { From = 99, To = 23 }
//                }
//            }
//        };

//        // Act
//        ValidationResult result = await new GetPlayersQueryValidator().ValidateAsync(query);

//        // Assert
//        Assert.False(result.IsValid);
//        Assert.Equal(2, result.Errors.Count);

//        ValidationFailure toFailure = result.Errors.First();
//        ValidationFailure fromFailure = result.Errors[1];

//        Assert.Equal("'From' value must be less than To value.", fromFailure.ErrorMessage);
//        Assert.Equal($"Filter.Stats.Total.From", fromFailure.PropertyName);
//        Assert.Equal("'To' value must be greater than From value.", toFailure.ErrorMessage);
//        Assert.Equal($"Filter.Stats.Total.To", toFailure.PropertyName);
//    }

//    public static readonly object[][] PhysicalStatsFromToValidData =
//    {
//        new object[] { (short)10, null! },
//        new object[] { null!, (short)89 },
//        new object[] { null!, null! }
//    };

//    [Theory]
//    [Trait("Feature", "Validators")]
//    [MemberData(nameof(TotalStatsFromToValidData))]
//    public async Task Feature_Validator_GetPlayers_PhysicalStats_ShouldPassValidationWhenFromIsNullOrToIsNullOrBothIsNull(short? from, short? to)
//    {
//        // Arrange
//        GetPlayersQuery query = new()
//        {
//            Filter = new GetPlayersFilterDto
//            {
//                Stats = new GetPlayersStatsFilterDto
//                {
//                    Physical = new GetPlayersStatsBySkillRangeLimitDto { From = from, To = to }
//                }
//            }
//        };

//        // Act
//        ValidationResult result = await new GetPlayersQueryValidator().ValidateAsync(query);

//        // Assert
//        Assert.True(result.IsValid);
//        Assert.Empty(result.Errors);
//    }

//    [Fact]
//    [Trait("Feature", "Validators")]
//    public async Task Feature_Validator_GetPlayers_PhysicalStats_ShouldFailValidationWhenFromIsMoreThanToAndViceVersa()
//    {
//        // Arrange
//        GetPlayersQuery query = new()
//        {
//            Filter = new GetPlayersFilterDto
//            {
//                Stats = new GetPlayersStatsFilterDto
//                {
//                    Physical = new GetPlayersStatsBySkillRangeLimitDto { From = 99, To = 23 }
//                }
//            }
//        };

//        // Act
//        ValidationResult result = await new GetPlayersQueryValidator().ValidateAsync(query);

//        // Assert
//        Assert.False(result.IsValid);
//        Assert.Equal(2, result.Errors.Count);

//        ValidationFailure toFailure = result.Errors.First();
//        ValidationFailure fromFailure = result.Errors[1];

//        Assert.Equal("'From' value must be less than To value.", fromFailure.ErrorMessage);
//        Assert.Equal($"Filter.Stats.Physical.From", fromFailure.PropertyName);
//        Assert.Equal("'To' value must be greater than From value.", toFailure.ErrorMessage);
//        Assert.Equal($"Filter.Stats.Physical.To", toFailure.PropertyName);
//    }

//    public static readonly object[][] MentalStatsFromToValidData =
//    {
//        new object[] { (short)10, null! },
//        new object[] { null!, (short)89 },
//        new object[] { null!, null! }
//    };

//    [Theory]
//    [Trait("Feature", "Validators")]
//    [MemberData(nameof(TotalStatsFromToValidData))]
//    public async Task Feature_Validator_GetPlayers_MentalStats_ShouldPassValidationWhenFromIsNullOrToIsNullOrBothIsNull(short? from, short? to)
//    {
//        // Arrange
//        GetPlayersQuery query = new()
//        {
//            Filter = new GetPlayersFilterDto
//            {
//                Stats = new GetPlayersStatsFilterDto
//                {
//                    Mental = new GetPlayersStatsBySkillRangeLimitDto { From = from, To = to }
//                }
//            }
//        };

//        // Act
//        ValidationResult result = await new GetPlayersQueryValidator().ValidateAsync(query);

//        // Assert
//        Assert.True(result.IsValid);
//        Assert.Empty(result.Errors);
//    }

//    [Fact]
//    [Trait("Feature", "Validators")]
//    public async Task Feature_Validator_GetPlayers_MentalStats_ShouldFailValidationWhenFromIsMoreThanToAndViceVersa()
//    {
//        // Arrange
//        GetPlayersQuery query = new()
//        {
//            Filter = new GetPlayersFilterDto
//            {
//                Stats = new GetPlayersStatsFilterDto
//                {
//                    Mental = new GetPlayersStatsBySkillRangeLimitDto { From = 99, To = 23 }
//                }
//            }
//        };

//        // Act
//        ValidationResult result = await new GetPlayersQueryValidator().ValidateAsync(query);

//        // Assert
//        Assert.False(result.IsValid);
//        Assert.Equal(2, result.Errors.Count);

//        ValidationFailure toFailure = result.Errors.First();
//        ValidationFailure fromFailure = result.Errors[1];

//        Assert.Equal("'From' value must be less than To value.", fromFailure.ErrorMessage);
//        Assert.Equal($"Filter.Stats.Mental.From", fromFailure.PropertyName);
//        Assert.Equal("'To' value must be greater than From value.", toFailure.ErrorMessage);
//        Assert.Equal($"Filter.Stats.Mental.To", toFailure.PropertyName);
//    }

//    public static readonly object[][] SkillStatsFromToValidData =
//    {
//        new object[] { (short)10, null! },
//        new object[] { null!, (short)89 },
//        new object[] { null!, null! }
//    };

//    [Theory]
//    [Trait("Feature", "Validators")]
//    [MemberData(nameof(TotalStatsFromToValidData))]
//    public async Task Feature_Validator_GetPlayers_SkillStats_ShouldPassValidationWhenFromIsNullOrToIsNullOrBothIsNull(short? from, short? to)
//    {
//        // Arrange
//        GetPlayersQuery query = new()
//        {
//            Filter = new GetPlayersFilterDto
//            {
//                Stats = new GetPlayersStatsFilterDto
//                {
//                    Skill = new GetPlayersStatsBySkillRangeLimitDto { From = from, To = to }
//                }
//            }
//        };

//        // Act
//        ValidationResult result = await new GetPlayersQueryValidator().ValidateAsync(query);

//        // Assert
//        Assert.True(result.IsValid);
//        Assert.Empty(result.Errors);
//    }

//    [Fact]
//    [Trait("Feature", "Validators")]
//    public async Task Feature_Validator_GetPlayers_SkillStats_ShouldFailValidationWhenFromIsMoreThanToAndViceVersa()
//    {
//        // Arrange
//        GetPlayersQuery query = new()
//        {
//            Filter = new GetPlayersFilterDto
//            {
//                Stats = new GetPlayersStatsFilterDto
//                {
//                    Skill = new GetPlayersStatsBySkillRangeLimitDto { From = 99, To = 23 }
//                }
//            }
//        };

//        // Act
//        ValidationResult result = await new GetPlayersQueryValidator().ValidateAsync(query);

//        // Assert
//        Assert.False(result.IsValid);
//        Assert.Equal(2, result.Errors.Count);

//        ValidationFailure toFailure = result.Errors.First();
//        ValidationFailure fromFailure = result.Errors[1];

//        Assert.Equal("'From' value must be less than To value.", fromFailure.ErrorMessage);
//        Assert.Equal($"Filter.Stats.Skill.From", fromFailure.PropertyName);
//        Assert.Equal("'To' value must be greater than From value.", toFailure.ErrorMessage);
//        Assert.Equal($"Filter.Stats.Skill.To", toFailure.PropertyName);
//    }

//    #endregion Stats
//}
