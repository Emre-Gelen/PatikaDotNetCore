using DotNetCoreWebApi.Application.AuthorOperations.Queries.GetAuthorDetail;
using FluentAssertions;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.UnitTests.TestSetup;
using Xunit;

namespace WebApi.UnitTests.Application.AuthorOperations.Queries.GetAuthorDetail
{
    public class GetAuthorDetailQueryValidatorTest : IClassFixture<CommonTestFixture>
    {
        [Fact]
        public void WhenInvalidInputIsGiven_Validator_ShouldReturnError()
        {
            //Arrange : Preparing
            GetAuthorDetailQuery command = new GetAuthorDetailQuery(null, null);
            command.AuthorId = 0;

            GetAuthorDetailQueryValidator validator = new GetAuthorDetailQueryValidator();

            //Act : Running && Assert : Validation
            FluentActions
                .Invoking(() => validator.ValidateAndThrow(command))
                .Should().Throw<FluentValidation.ValidationException>();
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public void WhenValidInputAreGiven_Validator_ShouldNotReturnError(int authorId)
        {
            //Arrange : Preparing
            GetAuthorDetailQuery command = new GetAuthorDetailQuery(null, null);
            command.AuthorId = authorId;

            GetAuthorDetailQueryValidator validator = new GetAuthorDetailQueryValidator();

            //Act : Running && Assert : Validation
            FluentActions
                .Invoking(() => validator.ValidateAndThrow(command))
                .Should().NotThrow();
        }
    }
}
