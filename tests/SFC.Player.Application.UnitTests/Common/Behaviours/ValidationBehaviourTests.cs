//using FluentValidation;

//using SFC.Player.Application.Common.Behaviours;
//using SFC.Player.Application.Common.Constants;
//using SFC.Player.Application.Common.Exceptions;
//using SFC.Player.Application.Features.Players.Queries.Get;

//namespace SFC.Player.Application.UnitTests.Common.Behaviours;
//public class ValidationBehaviourTests
//{
//    private const string VALIDATION_MESSAGE = "Test validation message.";

//    private readonly Guid MOCK_USER_ID = Guid.Parse("db69fc8c-cd50-4c99-96b3-9ddb6c49d08b");

//    public class TestValidator : AbstractValidator<GetPlayerByUserQuery>
//    {
//        public TestValidator(Guid assertUserId, string message = VALIDATION_MESSAGE)
//        {
//            //RuleFor(query => query.UserId).Must(userId => userId == assertUserId)
//            //                      .WithMessage(message);
//        }
//    }

//    [Fact]
//    [Trait("Behaviour", "Validation")]
//    public async Task Behaviour_Validation_ShouldReturnResponse()
//    {
//        // Arrange
//        IEnumerable<IValidator<GetPlayerByUserQuery>> validators = new List<IValidator<GetPlayerByUserQuery>> { new TestValidator(MOCK_USER_ID) };
//        GetPlayerByUserQuery request = new();
//        ValidationBehaviour<GetPlayerByUserQuery, GetPlayerByUserViewModel> requestValidationBehaviour = new(validators);

//        // Act
//        GetPlayerByUserViewModel response = await requestValidationBehaviour.Handle(request, () => Task.FromResult(new GetPlayerByUserViewModel()), new CancellationToken());

//        // Assert
//        Assert.NotNull(response);
//    }

//    [Fact]
//    [Trait("Behaviour", "Validation")]
//    public async Task Behaviour_Validation_ShouldNotValidateIfNoValidators()
//    {
//        // Arrange
//        IEnumerable<IValidator<GetPlayerByUserQuery>> validators = new List<IValidator<GetPlayerByUserQuery>>();
//        GetPlayerByUserQuery request = new();
//        ValidationBehaviour<GetPlayerByUserQuery, GetPlayerByUserViewModel> requestValidationBehaviour = new(validators);

//        // Act
//        GetPlayerByUserViewModel response = await requestValidationBehaviour.Handle(request, () => Task.FromResult(new GetPlayerByUserViewModel()), new CancellationToken());

//        // Assert
//        Assert.NotNull(response);
//    }

//    [Fact]
//    [Trait("Behaviour", "Validation")]
//    public async Task Behaviour_Validation_ShouldReturnBadRequestException()
//    {
//        // Arrange
//        IEnumerable<IValidator<GetPlayerByUserQuery>> validators = new List<IValidator<GetPlayerByUserQuery>> { new TestValidator(Guid.NewGuid()) };
//        GetPlayerByUserQuery request = new();
//        ValidationBehaviour<GetPlayerByUserQuery, GetPlayerByUserViewModel> requestValidationBehaviour = new(validators);

//        // Act
//        BadRequestException assertException = await Assert.ThrowsAsync<BadRequestException>(async () =>
//           await requestValidationBehaviour.Handle(request, () => Task.FromResult(new GetPlayerByUserViewModel()), new CancellationToken()));

//        // Assert
//        Assert.NotNull(assertException);
//        Assert.Equal(Localization.ValidationError, assertException.Message);
//        Assert.NotNull(assertException.Errors);
//        //Assert.True(assertException.Errors.ContainsKey(nameof(GetPlayerByUserQuery.UserId)));
//        //Assert.Single(assertException.Errors[nameof(GetPlayerByUserQuery.UserId)]);
//        //Assert.Equal(VALIDATION_MESSAGE, assertException.Errors[nameof(GetPlayerByUserQuery.UserId)].FirstOrDefault());
//    }
//}
