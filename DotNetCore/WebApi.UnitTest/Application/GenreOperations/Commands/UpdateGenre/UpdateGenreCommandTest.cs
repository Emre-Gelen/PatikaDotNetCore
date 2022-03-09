using AutoMapper;
using DotNetCoreWebApi.Application.GenreOperations.Commands.UpdateGenre;
using DotNetCoreWebApi.DBOperations;
using FluentAssertions;
using System;
using System.Linq;
using WebApi.UnitTests.TestSetup;
using Xunit;

namespace WebApi.UnitTests.Application.GenreOperations.Commands.UpdateGenre
{
    public class UpdateGenreCommandTest : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _dbContext;
        private readonly IMapper _mapper;

        public UpdateGenreCommandTest(CommonTestFixture testFixture)
        {
            _dbContext = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenNotExistsIdIsGiven_InvalidOperationException_ShouldReturn()
        {
            //Arrange
            UpdateGenreCommand command = new UpdateGenreCommand(_dbContext);
            int Id = 10000;

            command.GenreId = Id;

            //Act & Assert
            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>();
        }

        [Fact]
        public void WhenValidInputsAreGiven_InvalidOperationException_ShouldNotReturn()
        {
            //Arrange
            UpdateGenreCommand command = new UpdateGenreCommand(_dbContext);
            UpdateGenreModel model = new UpdateGenreModel()
            {
                Name = "Test_UpdatedName",
                IsActive = true
            };

            command.GenreId = _dbContext.Genres.Max(m => m.Id);
            command.Model = model;

            //Act
            FluentActions
                .Invoking(() => command.Handle()).Invoke();

            //Assert
            var genre = _dbContext.Genres.SingleOrDefault(s => s.Name == model.Name);
            genre.Should().NotBeNull();
            genre.Name.Should().Be(model.Name);
            genre.IsActive.Should().Be(model.IsActive);
        }
    }
}