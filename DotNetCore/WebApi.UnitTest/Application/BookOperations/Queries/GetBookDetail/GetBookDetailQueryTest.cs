using AutoMapper;
using DotNetCoreWebApi.Application.BookOperations.Queries.GetBookDetail;
using DotNetCoreWebApi.DBOperations;
using FluentAssertions;
using System;
using WebApi.UnitTests.TestSetup;
using Xunit;

namespace WebApi.UnitTests.Application.BookOperations.Queries.GetBookDetail
{
    public class GetBookDetailQueryTest : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetBookDetailQueryTest(CommonTestFixture testFixture)
        {
            _dbContext = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenInvalidInputIsGiven_InvalidOperationException_ShouldReturnError()
        {
            //Arrange : Preparing
            GetBookDetailQuery query = new GetBookDetailQuery(_dbContext, _mapper);
            int Id = 1000000;
            query.BookId = Id;

            //Act : Running && Assert : Validation
            FluentActions
                .Invoking(() => query.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Book was not found.");
        }

        [Fact]
        public void WhenValidInputIsGiven_InvalidOperationException_ShouldNotReturnError()
        {
            //Arrange : Preparing
            GetBookDetailQuery query = new GetBookDetailQuery(_dbContext, _mapper);
            int Id = 1;
            query.BookId = Id;

            //Act : Running && Assert : Validation
            FluentActions
                .Invoking(() => query.Handle())
                .Should().NotThrow();
        }
    }
}