using DotNetCoreWebApi.Application.GenreOperations.Commands.DeleteGenre;
using FluentAssertions;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.UnitTests.TestSetup;
using Xunit;

namespace WebApi.UnitTests.Application.GenreOperations.Commands.DeleteGenre
{
    public class DeleteGenreCommandValidatorTest : IClassFixture<CommonTestFixture>
    {
        [Fact]
        public void WhenInvalidIdIsGiven_Validator_ShouldReturnError()
        {
            //Arrange : Preparing
            DeleteGenreCommand command = new DeleteGenreCommand(null);
            command.GenreId = 0;

            DeleteGenreCommandValidator validator = new DeleteGenreCommandValidator();

            //Act : Running && Assert : Validation
            FluentActions
                .Invoking(() => validator.ValidateAndThrow(command))
                .Should().Throw<FluentValidation.ValidationException>();
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        public void WhenValidIdIsGiven_Validator_ShouldNotReturnError(int genreId)
        {
            //Arrange : Preparing
            DeleteGenreCommand command = new DeleteGenreCommand(null);
            command.GenreId = genreId;

            DeleteGenreCommandValidator validator = new DeleteGenreCommandValidator();

            //Act : Running && Assert : Validation
            FluentActions
                .Invoking(() => validator.ValidateAndThrow(command))
                .Should().NotThrow();
        }
    }
}
