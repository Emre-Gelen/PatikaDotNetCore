using AutoMapper;
using DotNetCoreWebApi.Application.BookOperations.Commands.UpdateBook;
using DotNetCoreWebApi.DBOperations;
using FluentAssertions;
using System;
using System.Linq;
using WebApi.UnitTests.TestSetup;
using Xunit;
using static DotNetCoreWebApi.Application.BookOperations.Commands.UpdateBook.UpdateBookCommand;

namespace WebApi.UnitTests.Application.BookOperations.Commands.UpdateBook
{
    public class UpdateBookCommandTest : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _dbContext;
        private readonly IMapper _mapper;

        public UpdateBookCommandTest(CommonTestFixture testFixture)
        {
            _dbContext = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenNotExistIdIsGiven_InvalidOperationException_ShouldReturnError()
        {
            //Arrange : Preparing
            int Id = 1000000;
            UpdateBookCommand command = new UpdateBookCommand(_dbContext);
            command.BookId = Id;

            //Act : Running && Assert : Validation
            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Book was not found.");
        }

        [Fact]
        public void WhenValidInputsAreGiven_InvalidOperationException_ShouldNotReturn()
        {
            //Arrange : Preparing
            UpdateBookCommand command = new UpdateBookCommand(_dbContext);
            int bookId = _dbContext.Books.Max(m => m.Id);
            UpdateBookModel model = new UpdateBookModel() { AuthorId = 2, GenreId = 5, Title = "Updated Title" };

            command.BookId = bookId;
            command.Model = model;

            //Act : Running
            FluentActions
                .Invoking(() => command.Handle()).Invoke();

            //Assert : Validation
            var book = _dbContext.Books.SingleOrDefault(s => s.Title == model.Title);
            book.Should().NotBeNull();
            book.GenreId.Should().Be(model.GenreId);
            book.AuthorId.Should().Be(model.AuthorId);
        }
    }
}