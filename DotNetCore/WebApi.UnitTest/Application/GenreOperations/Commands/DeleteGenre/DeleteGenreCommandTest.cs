using DotNetCoreWebApi.Application.GenreOperations.Commands.DeleteGenre;
using DotNetCoreWebApi.DBOperations;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.UnitTests.TestSetup;
using Xunit;

namespace WebApi.UnitTests.Application.GenreOperations.Commands.DeleteGenre
{
    public class DeleteGenreCommandTest : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _dbContext;
        public DeleteGenreCommandTest(CommonTestFixture testFixture)
        {
            _dbContext = testFixture.Context;
        }

        [Fact]
        public void WhenNotExistsIdIsGiven_InvalidOperationException_ShouldReturn()
        {
            //Arrange : Preparing
            int Id = 10000;
            DeleteGenreCommand command = new DeleteGenreCommand(_dbContext);

            command.GenreId = Id;

            //Act : Running && Assert : Validation
            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>();
        }
        [Fact]
        public void WhenValidIdIsGiven_InvalidOperationException_ShouldNotReturn()
        {
            //Arrange : Preparing
            DeleteGenreCommand command = new DeleteGenreCommand(_dbContext);
            int Id = 1;

            command.GenreId = Id;

            //Act : Running && Assert : Validation
            FluentActions
                .Invoking(() => command.Handle()).Invoke();

            var genre = _dbContext.Genres.SingleOrDefault(s => s.Id == Id);
            genre.Should().BeNull();
        }
    }
}
