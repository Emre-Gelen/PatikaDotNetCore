using DotNetCoreWebApi.Application.BookOperations.Commands.UpdateBook;
using FluentAssertions;
using FluentValidation;
using WebApi.UnitTests.TestSetup;
using Xunit;
using static DotNetCoreWebApi.Application.BookOperations.Commands.UpdateBook.UpdateBookCommand;

namespace WebApi.UnitTests.Application.BookOperations.Commands.UpdateBook
{
    public class UpdateBookCommandValidatorTest : IClassFixture<CommonTestFixture>
    {
        [Theory]
        [InlineData(0, "Lord Of The Rings", 0, 0)]
        [InlineData(0, "Lord Of The Rings", 1, 0)]
        [InlineData(0, "Lord Of The Rings", 0, 1)]
        [InlineData(1, "Lord", 0, 1)]
        [InlineData(1, "Lord", 1, 0)]
        [InlineData(1, "Lor", 1, 1)]
        [InlineData(1, "Lor", 1, 0)]
        public void WhenInvalidInputsAreGiven_Validator_ShouldReturnErrors(int bookId, string title, int genreId, int authorId)
        {
            //Arrange : Preparing
            UpdateBookCommand command = new UpdateBookCommand(null);
            command.BookId = bookId;
            command.Model = new UpdateBookModel()
            {
                Title = title,
                AuthorId = authorId,
                GenreId = genreId
            };

            //Act : Running && Assert : Validation
            UpdateBookCommandValidator validator = new UpdateBookCommandValidator();

            FluentActions
                .Invoking(() => validator.ValidateAndThrow(command))
                .Should().Throw<FluentValidation.ValidationException>();
        }

        [Theory]
        [InlineData(1, "Lord Of The Rings", 1, 2)]
        [InlineData(2, "Lord Of The Rings", 2, 2)]
        [InlineData(3, "Lord Of The Rings", 3, 1)]
        [InlineData(1, "Lord Of The Rings", 4, 1)]
        public void WhenValidInputsAreGiven_Validator_ShouldNotReturnError(int bookId, string title, int genreId, int authorId)
        {
            //Arrange : Preparing
            UpdateBookCommand command = new UpdateBookCommand(null);
            command.BookId = bookId;
            command.Model = new UpdateBookModel()
            {
                Title = title,
                AuthorId = authorId,
                GenreId = genreId
            };

            UpdateBookCommandValidator validator = new UpdateBookCommandValidator();

            //Act : Running && Assert : Validation
            FluentActions
                .Invoking(() => validator.ValidateAndThrow(command))
                .Should().NotThrow();
        }
    }
}