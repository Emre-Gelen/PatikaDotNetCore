using AutoMapper;
using DotNetCoreWebApi.Application.AuthorOperations.Queries.GetAuthorDetail;
using DotNetCoreWebApi.DBOperations;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.UnitTests.TestSetup;
using Xunit;

namespace WebApi.UnitTests.Application.AuthorOperations.Queries.GetAuthorDetail
{
    public class GetAuthorDetailQueryTest : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetAuthorDetailQueryTest(CommonTestFixture testFixture)
        {
            _dbContext = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenInvalidInputIsGiven_InvalidOperationException_ShouldReturnError()
        {
            //Arrange : Preparing
            GetAuthorDetailQuery query = new GetAuthorDetailQuery(_dbContext, _mapper);
            int Id = _dbContext.Authors.Max(m => m.Id) + 10;
            query.AuthorId = Id;

            //Act : Running && Assert : Validation
            FluentActions
                .Invoking(() => query.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Author was not found.");
        }

        [Fact]
        public void WhenValidInputIsGiven_InvalidOperationException_ShouldNotReturnError()
        {
            //Arrange : Preparing
            GetAuthorDetailQuery query = new GetAuthorDetailQuery(_dbContext, _mapper);
            int Id = _dbContext.Authors.Max(m => m.Id);
            query.AuthorId = Id;

            //Act : Running && Assert : Validation
            FluentActions
                .Invoking(() => query.Handle())
                .Should().NotThrow();
        }
    }
}
