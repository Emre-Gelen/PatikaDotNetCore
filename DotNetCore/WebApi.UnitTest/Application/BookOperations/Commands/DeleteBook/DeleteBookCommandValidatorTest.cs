using DotNetCoreWebApi.Application.BookOperations.Commands.DeleteBook;
using FluentAssertions;
using FluentValidation;
using WebApi.UnitTests.TestSetup;
using Xunit;

namespace WebApi.UnitTests.Application.BookOperations.Commands.DeleteBook
{
    public class DeleteBookCommandValidatorTest : IClassFixture<CommonTestFixture>
    {
        [Fact]
        public void WhenInvalidInputAreGiven_Validator_ShouldReturnError()
        {
            //Arrange : Preparing
            DeleteBookCommand command = new DeleteBookCommand(null);
            command.BookId = 0;

            DeleteBookCommandValidator validator = new DeleteBookCommandValidator();

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
        [InlineData(5)]
        [InlineData(6)]
        [InlineData(7)]
        public void WhenValidInputAreGiven_Validator_ShouldNotReturnError(int bookId)
        {
            //Arrange : Preparing
            DeleteBookCommand command = new DeleteBookCommand(null);
            command.BookId = bookId;

            DeleteBookCommandValidator validator = new DeleteBookCommandValidator();

            //Act : Running && Assert : Validation
            FluentActions
                .Invoking(() => validator.ValidateAndThrow(command))
                .Should().NotThrow();
        }
    }
}