using AutoMapper;
using DotNetCoreWebApi.Application.GenreOperations.Commands.CreateGenre;
using DotNetCoreWebApi.DBOperations;
using DotNetCoreWebApi.Entities;
using FluentAssertions;
using System;
using System.Linq;
using WebApi.UnitTests.TestSetup;
using Xunit;
using static DotNetCoreWebApi.Application.GenreOperations.Commands.CreateGenre.CreateGenreCommand;

namespace WebApi.UnitTests.Application.GenreOperations.Commands.CreateGenre
{
    public class CreateGenreCommandTest : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _dbContext;
        private readonly IMapper _mapper;

        public CreateGenreCommandTest(CommonTestFixture testFixture)
        {
            _dbContext = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenAlreadyExistGenreTitleIsGiven_InvalidOperationException_ShouldReturn()
        {
            //Arrange : Preparing
            var genre = new Genre() { IsActive = true, Name = "Test_WhenAlreadyExistGenreTitleIsGiven" };
            _dbContext.Genres.Add(genre);
            _dbContext.SaveChanges();

            CreateGenreCommand command = new CreateGenreCommand(_dbContext, _mapper);
            command.Model = new CreateGenreModel() { Name = genre.Name };

            //Act : Running && Assert : Validation
            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Genre is already exists.");
        }

        [Fact]
        public void WhenValidInputsAreGiven_InvalidOperationException_ShouldNotReturn()
        {
            //Arrange : Preparing
            CreateGenreCommand command = new CreateGenreCommand(_dbContext, _mapper);
            CreateGenreModel model = new CreateGenreModel() { Name = "Test__" };
            command.Model = model;

            //Act : Running
            FluentActions
                .Invoking(() => command.Handle()).Invoke();

            //Assert : Validation
            var genre = _dbContext.Genres.SingleOrDefault(s => s.Name == model.Name);
            genre.Should().NotBeNull();
            genre.IsActive.Should().BeTrue();
        }
    }
}