using DotNetCoreWebApi.Application.GenreOperations.Commands.CreateGenre;
using FluentAssertions;
using FluentValidation;
using WebApi.UnitTests.TestSetup;
using Xunit;
using static DotNetCoreWebApi.Application.GenreOperations.Commands.CreateGenre.CreateGenreCommand;

namespace WebApi.UnitTests.Application.GenreOperations.Commands.CreateGenre
{
    public class CreateGenreCommandValidatorTest : IClassFixture<CommonTestFixture>
    {
        [Theory]
        [InlineData("Cody")]
        [InlineData("Fant")]
        [InlineData("Com")]
        public void WhenInvalidInputsAreGiven_Validator_ShouldReturnError(string name)
        {
            //Arrange : Preparing
            CreateGenreCommand command = new CreateGenreCommand(null, null);
            CreateGenreModel model = new CreateGenreModel()
            {
                Name = name
            };
            command.Model = model;

            CreateGenreCommandValidator validator = new CreateGenreCommandValidator();

            //Act : Running && Assert : Validation
            FluentActions
                .Invoking(() => validator.ValidateAndThrow(command))
                .Should().Throw<FluentValidation.ValidationException>();
        }

        [Fact]
        public void WhenValidInputsAreGiven_Validator_ShouldNotReturnError()
        {
            //Arrange : Preparing
            CreateGenreCommand command = new CreateGenreCommand(null, null);
            command.Model = new CreateGenreModel()
            {
                Name = "Comedy"
            };

            CreateGenreCommandValidator validator = new CreateGenreCommandValidator();

            FluentActions
                .Invoking(() => validator.ValidateAndThrow(command))
                .Should().NotThrow();
        }
    }
}