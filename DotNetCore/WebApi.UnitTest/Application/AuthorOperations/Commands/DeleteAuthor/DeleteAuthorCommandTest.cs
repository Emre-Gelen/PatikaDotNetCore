using DotNetCoreWebApi.Application.AuthorOperations.Commands.DeleteAuthor;
using DotNetCoreWebApi.DBOperations;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.UnitTests.TestSetup;
using Xunit;

namespace WebApi.UnitTests.Application.AuthorOperations.Commands.DeleteAuthor
{
    public class DeleteAuthorCommandTest : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _dbContext;
        public DeleteAuthorCommandTest(CommonTestFixture testFixture)
        {
            _dbContext = testFixture.Context;
        }

        [Fact]
        public void WhenNotExistIdIsGiven_InvalidOperationException_ShouldReturnError()
        {
            //Arrange : Preparing
            int Id = 1000000;
            DeleteAuthorCommand command = new DeleteAuthorCommand(_dbContext);
            command.AuthorId = Id;

            //Act : Running && Assert : Validation
            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Author was not found.");
        }
        [Fact]
        public void WhenExistIdIsGivenButAuthorHasBook_InvalidOperationException_ShouldReturnError()
        {
            //Arrange : Preparing
            int Id = _dbContext.Authors
                .Where(author => _dbContext.Books.Where(book => book.AuthorId == author.Id).Count() > 0)
                .First().Id;
            DeleteAuthorCommand command = new DeleteAuthorCommand(_dbContext);
            command.AuthorId = Id;

            //Act : Running && Assert : Validation
            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Author has book(s).");
        }
        [Fact]
        public void WhenValidInputsAreGiven_InvalidOperationException_ShouldNotReturnError()
        {
            //Arrange : Preparing
            DeleteAuthorCommand command = new DeleteAuthorCommand(_dbContext);
            int authorId = _dbContext.Authors
                .Where(author => _dbContext.Books.Where(book => book.AuthorId == author.Id).Count() == 0)
                .First().Id;

            command.AuthorId = authorId;

            //Act : Running
            FluentActions
                .Invoking(() => command.Handle()).Invoke();

            //Assert : Validation
            var author= _dbContext.Authors.SingleOrDefault(s => s.Id == authorId);
            author.Should().BeNull();
        }
    }
}
