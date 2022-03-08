using DotNetCoreWebApi.Application.BookOperations.Queries.GetBookDetail;
using FluentAssertions;
using FluentValidation;
using WebApi.UnitTests.TestSetup;
using Xunit;

namespace WebApi.UnitTests.Application.BookOperations.Queries.GetBookDetail
{
    public class GetBookDetailQueryValidatorTest : IClassFixture<CommonTestFixture>
    {
        [Fact]
        public void WhenInvalidInputIsGiven_Validator_ShouldReturnError()
        {
            //Arrange : Preparing
            GetBookDetailQuery command = new GetBookDetailQuery(null, null);
            command.BookId = 0;

            GetBookDetailQueryValidator validator = new GetBookDetailQueryValidator();

            //Act : Running && Assert : Validation
            FluentActions
                .Invoking(() => validator.ValidateAndThrow(command))
                .Should().Throw<FluentValidation.ValidationException>();
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        [InlineData(5)]
        [InlineData(6)]
        [InlineData(7)]
        public void WhenValidInputAreGiven_Validator_ShouldNotReturnError(int bookId)
        {
            //Arrange : Preparing
            GetBookDetailQuery command = new GetBookDetailQuery(null, null);
            command.BookId = bookId;

            GetBookDetailQueryValidator validator = new GetBookDetailQueryValidator();

            //Act : Running && Assert : Validation
            FluentActions
                .Invoking(() => validator.ValidateAndThrow(command))
                .Should().NotThrow();
        }
    }
}