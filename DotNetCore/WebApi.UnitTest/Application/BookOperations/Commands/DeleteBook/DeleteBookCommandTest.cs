using DotNetCoreWebApi.Application.BookOperations.Commands.DeleteBook;
using DotNetCoreWebApi.DBOperations;
using FluentAssertions;
using System;
using System.Linq;
using WebApi.UnitTests.TestSetup;
using Xunit;

namespace WebApi.UnitTests.Application.BookOperations.Commands.DeleteBook
{
    public class DeleteBookCommandTest : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _dbContext;

        public DeleteBookCommandTest(CommonTestFixture testFixture)
        {
            _dbContext = testFixture.Context;
        }

        [Fact]
        public void WhenNotExistIdIsGiven_InvalidOperationException_ShouldReturnError()
        {
            //Arrange : Preparing
            int Id = 1000000;
            DeleteBookCommand command = new DeleteBookCommand(_dbContext);
            command.BookId = Id;

            //Act : Running && Assert : Validation
            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Book was not found.");
        }

        [Fact]
        public void WhenValidInputsAreGiven_InvalidOperationException_ShouldNotReturnError()
        {
            //Arrange : Preparing
            DeleteBookCommand command = new DeleteBookCommand(_dbContext);
            int bookId = 1;

            command.BookId = bookId;

            //Act : Running
            FluentActions
                .Invoking(() => command.Handle()).Invoke();

            //Assert : Validation
            var book = _dbContext.Books.SingleOrDefault(s => s.Id == bookId);
            book.Should().BeNull();
        }
    }
}