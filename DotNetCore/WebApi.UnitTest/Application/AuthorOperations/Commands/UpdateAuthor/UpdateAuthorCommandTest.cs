using AutoMapper;
using DotNetCoreWebApi.Application.AuthorOperations.Commands.UpdateAuthor;
using DotNetCoreWebApi.DBOperations;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.UnitTests.TestSetup;
using Xunit;
using static DotNetCoreWebApi.Application.AuthorOperations.Commands.UpdateAuthor.UpdateAuthorCommand;

namespace WebApi.UnitTests.Application.AuthorOperations.Commands.UpdateAuthor
{
    public class UpdateAuthorCommandTest : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _dbContext;
        private readonly IMapper _mapper;

        public UpdateAuthorCommandTest(CommonTestFixture testFixture)
        {
            _dbContext = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenNotExistIdIsGiven_InvalidOperationException_ShouldReturnError()
        {
            //Arrange : Preparing
            int Id = 1000000;
            UpdateAuthorCommand command = new UpdateAuthorCommand(_dbContext);
            command.AuthorId = Id;

            //Act : Running && Assert : Validation
            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Author was not found.");
        }

        [Fact]
        public void WhenValidInputsAreGiven_Author_ShouldBeUpdated()
        {
            //Arrange : Preparing
            UpdateAuthorCommand command = new UpdateAuthorCommand(_dbContext);
            int authorId = _dbContext.Books.Max(m => m.Id);
            UpdateAuthorModel model = new UpdateAuthorModel() { Name = "Test_WhenValidInputsAreGiven_Author_ShouldBeUpdated",Surname = "TestSurname_WhenValidInputsAreGiven_Author_ShouldBeUpdated"};

            command.AuthorId = authorId;
            command.Model = model;

            //Act : Running
            FluentActions
                .Invoking(() => command.Handle()).Invoke();

            //Assert : Validation
            var author = _dbContext.Authors.SingleOrDefault(s => s.Name == model.Name && s.Surname == model.Surname );
            author.Should().NotBeNull();
            author.Name.Should().Be(model.Name);
            author.Surname.Should().Be(model.Surname);
        }
    }
}
