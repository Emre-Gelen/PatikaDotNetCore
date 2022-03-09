using AutoMapper;
using DotNetCoreWebApi.Application.GenreOperations.Queries.GetGenreDetail;
using DotNetCoreWebApi.DBOperations;
using FluentAssertions;
using System;
using System.Linq;
using WebApi.UnitTests.TestSetup;
using Xunit;

namespace WebApi.UnitTests.Application.GenreOperations.Queries.GetGenreDetail
{
    public class GetGenreDetailQueryTest : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetGenreDetailQueryTest(CommonTestFixture testFixture)
        {
            _dbContext = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenInvalidInputIsGiven_InvalidOperationException_ShouldReturnError()
        {
            //Arrange : Preparing
            GetGenreDetailQuery query = new GetGenreDetailQuery(_dbContext, _mapper);
            int Id = 1000000;
            query.GenreId = Id;

            //Act : Running && Assert : Validation
            FluentActions
                .Invoking(() => query.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Genre was not found.");
        }

        [Fact]
        public void WhenValidInputIsGiven_InvalidOperationException_ShouldNotReturnError()
        {
            //Arrange : Preparing
            GetGenreDetailQuery query = new GetGenreDetailQuery(_dbContext, _mapper);
            int Id = _dbContext.Genres.Max(m => m.Id);
            query.GenreId = Id;

            //Act : Running && Assert : Validation
            FluentActions
                .Invoking(() => query.Handle())
                .Should().NotThrow();
        }
    }
}