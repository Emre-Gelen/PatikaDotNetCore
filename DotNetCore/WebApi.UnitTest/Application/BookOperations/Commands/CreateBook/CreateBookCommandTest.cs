using AutoMapper;
using DotNetCoreWebApi.Application.BookOperations.Commands.CreateBook;
using DotNetCoreWebApi.DBOperations;
using DotNetCoreWebApi.Entities;
using FluentAssertions;
using System;
using System.Linq;
using WebApi.UnitTests.TestSetup;
using Xunit;
using static DotNetCoreWebApi.Application.BookOperations.Commands.CreateBook.CreateBookCommand;

namespace WebApi.UnitTests.Application.BookOperations.Commands.CreateBook
{
    public class CreateBookCommandTest : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _dbContext;
        private readonly IMapper _mapper;

        public CreateBookCommandTest(CommonTestFixture testFixture)
        {
            _dbContext = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenAlreadyExistBookTitleIsGiven_InvalidOperationException_ShouldReturn()
        {
            //Arrange : Preparing
            var book = new Book() { Title = "Test_WhenAlreadyExistBookTitleIsGiven_InvalidOperationException_ShouldBeReturn", PageCount = 504, PublishDate = new DateTime(1999, 9, 6), GenreId = 1, AuthorId = 1 };
            _dbContext.Books.Add(book);
            _dbContext.SaveChanges();

            CreateBookCommand command = new CreateBookCommand(_dbContext, _mapper);
            command.Model = new CreateBookModel() { Title = book.Title };
            //Act : Running && Assert : Validation
            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Book is already exist.");
        }

        [Fact]
        public void WhenValidInputsAreGiven_InvalidOperationException_ShouldReturn()
        {
            //Arrange : Preparing
            CreateBookCommand command = new CreateBookCommand(_dbContext, _mapper);
            CreateBookModel model = new CreateBookModel() { Title = "Hobbit", PageCount = 504, PublishDate = new DateTime(1999, 9, 6), GenreId = 1, AuthorId = 1 };
            command.Model = model;
            //Act : Running
            FluentActions
                .Invoking(() => command.Handle()).Invoke();

            //Assert : Validation
            var book = _dbContext.Books.SingleOrDefault(s => s.Title == model.Title);
            book.Should().NotBeNull();
            book.PageCount.Should().Be(model.PageCount);
            book.PublishDate.Should().Be(model.PublishDate);
            book.GenreId.Should().Be(model.GenreId);
            book.AuthorId.Should().Be(model.AuthorId);
        }
    }
}