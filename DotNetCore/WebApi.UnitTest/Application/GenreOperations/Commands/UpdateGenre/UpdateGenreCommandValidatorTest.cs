using DotNetCoreWebApi.Application.GenreOperations.Commands.UpdateGenre;
using FluentAssertions;
using FluentValidation;
using WebApi.UnitTests.TestSetup;
using Xunit;

namespace WebApi.UnitTests.Application.GenreOperations.Commands.UpdateGenre
{
    public class UpdateGenreCommandValidatorTest : IClassFixture<CommonTestFixture>
    {
        [Theory]
        [InlineData(0,"Comedy",true)]
        [InlineData(0,"Comedy",false)]
        [InlineData(1,"Com",true)]
        public void WhenInvalidInputsAreGiven_Validator_ShouldReturnErrors(int genreId, string name, bool isActive)
        {
            //Arrange
            UpdateGenreCommand command = new UpdateGenreCommand(null);
            command.GenreId = genreId;
            command.Model = new UpdateGenreModel()
            {
                Name = name,
                IsActive = isActive
            };

            UpdateGenreCommandValidator validator = new UpdateGenreCommandValidator();

            //Act & Assert
            FluentActions
                .Invoking(() => validator.ValidateAndThrow(command))
                .Should().Throw<FluentValidation.ValidationException>();
        }
        [Theory]
        [InlineData(1, "Comedy", true)]
        [InlineData(2, "Comedy", false)]
        [InlineData(4, "Come", false)]
        public void WhenValidInputsAreGiven_Validator_ShouldNotReturnError(int genreId, string name, bool isActive)
        {
            //Arrange
            UpdateGenreCommand command = new UpdateGenreCommand(null);
            command.GenreId = genreId;
            command.Model = new UpdateGenreModel()
            {
                Name = name,
                IsActive = isActive
            };

            UpdateGenreCommandValidator validator = new UpdateGenreCommandValidator();

            //Act & Assert
            FluentActions
                .Invoking(() => validator.ValidateAndThrow(command))
                .Should().NotThrow();
        }
    }
}