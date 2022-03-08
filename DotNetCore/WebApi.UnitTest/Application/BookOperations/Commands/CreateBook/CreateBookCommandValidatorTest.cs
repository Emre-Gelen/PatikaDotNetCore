using DotNetCoreWebApi.Application.BookOperations.Commands.CreateBook;
using FluentAssertions;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.UnitTests.TestSetup;
using Xunit;
using static DotNetCoreWebApi.Application.BookOperations.Commands.CreateBook.CreateBookCommand;

namespace WebApi.UnitTests.Application.BookOperations.Commands.CreateBook
{
    public class CreateBookCommandValidatorTest : IClassFixture<CommonTestFixture>
    {
        [Theory]
        [InlineData("Lord Of The Rings", 0, 0, 0)]
        [InlineData("Lord Of The Rings", 1, 0, 0)]
        [InlineData("Lord Of The Rings", 0, 1, 0)]
        [InlineData("Lord Of The Rings", 1, 1, 0)]
        [InlineData("Lord Of The Rings", 0, 0, 1)]
        [InlineData("Lord Of The Rings", 1, 0, 1)]
        [InlineData("Lord Of The Rings", 0, 1, 1)]
        [InlineData(" ", 1, 1, 1)]
        [InlineData(" ", 1, 0, 1)]
        [InlineData(" ", 0, 0, 0)]
        [InlineData("", 1, 1, 1)]
        [InlineData("", 0, 1, 1)]
        [InlineData("", 1, 0, 1)]
        [InlineData("Lord", 1, 1, 0)]
        [InlineData("Lor", 1, 1, 1)]
        public void WhenInvalidInputsAreGiven_Validator_ShouldReturnErrors(string title, int genreId, int pageCount, int authorId)
        {
            //Arrange : Preparing
            CreateBookCommand command = new CreateBookCommand(null, null);
            command.Model = new CreateBookModel()
            {
                Title = title,
                GenreId = genreId,
                PageCount = pageCount,
                AuthorId = authorId,
                PublishDate = DateTime.Now.Date.AddYears(-1)
            };

            CreateBookCommandValidator validator = new CreateBookCommandValidator();

            //Act : Running && Assert : Validation
            FluentActions
                .Invoking(() => validator.ValidateAndThrow(command))
                .Should().Throw<FluentValidation.ValidationException>();
        }
        [Fact]
        public void WhenDateTimeEqualNowIsGiven_Validator_ShouldReturnError()
        {
            //Arrange : Preparing
            CreateBookCommand command = new CreateBookCommand(null, null);
            command.Model = new CreateBookModel()
            {
                Title = "TestTitle",
                GenreId = 1,
                PageCount = 150,
                AuthorId = 1,
                PublishDate = DateTime.Now.Date
            };

            CreateBookCommandValidator validator = new CreateBookCommandValidator();

            //Act : Running && Assert : Validation
            FluentActions
               .Invoking(() => validator.ValidateAndThrow(command))
               .Should().Throw<FluentValidation.ValidationException>();
        }
        [Fact]
        public void WhenValidInputsAreGiven_Validator_ShouldNotReturnError()
        {
            //Arrange : Preparing
            CreateBookCommand command = new CreateBookCommand(null, null);
            command.Model = new CreateBookModel()
            {
                Title = "TestTitle",
                GenreId = 1,
                PageCount = 150,
                AuthorId = 1,
                PublishDate = DateTime.Now.Date.AddYears(-1)};

            CreateBookCommandValidator validator = new CreateBookCommandValidator();

            //Act : Running && Assert : Validation
            FluentActions
               .Invoking(() => validator.ValidateAndThrow(command))
               .Should().NotThrow();
        }
    }
}
