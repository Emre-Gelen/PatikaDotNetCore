using AutoMapper;
using DotNetCoreWebApi.Application.AuthorOperations.Commands.CreateAuthor;
using DotNetCoreWebApi.DBOperations;
using DotNetCoreWebApi.Entities;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.UnitTests.TestSetup;
using Xunit;
using static DotNetCoreWebApi.Application.AuthorOperations.Commands.CreateAuthor.CreateAuthorCommand;

namespace WebApi.UnitTests.Application.AuthorOperations.Commands.CreateAuthor
{
    
    public class CreateAuthorCommandTest : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        public CreateAuthorCommandTest(CommonTestFixture testFixture)
        {
            _dbContext = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenAlreadyExistAuthorFullNameIsGiven_InvalidOperationException_ShouldReturn()
        {
            //Arrange : Preparing
            var author = new Author() { Name= "Test_AuthorName", Surname = "Test_AuthorSurname", BirthDate = DateTime.Now.Date.AddYears(-30)};
            _dbContext.Authors.Add(author);
            _dbContext.SaveChanges();

            CreateAuthorCommand command = new CreateAuthorCommand(_dbContext, _mapper);
            command.Model = new CreateAuthorModel() { Name = author.Name, Surname = author.Surname, BirthDate = author.BirthDate};
            //Act : Running && Assert : Validation
            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Author is already exists.");
        }
        [Fact]
        public void WhenValidInputsAreGiven_Author_ShouldBeCreated()
        {
            //Arrange : Preparing
            CreateAuthorCommand command = new CreateAuthorCommand(_dbContext, _mapper);
            CreateAuthorModel model = new CreateAuthorModel() { Name = "Emre", Surname = "Gelen", BirthDate = DateTime.Now.Date.AddYears(-30) };
            command.Model = model;
            //Act : Running
            FluentActions
                .Invoking(() => command.Handle()).Invoke();

            //Assert : Validation
            var author = _dbContext.Authors.SingleOrDefault(s => s.Name == model.Name && s.Surname == model.Surname);
            author.Should().NotBeNull();
            author.Name.Should().Be(model.Name);
            author.Surname.Should().Be(model.Surname);
            author.BirthDate.Should().Be(model.BirthDate);
        }
    }
}
