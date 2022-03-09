using DotNetCoreWebApi.Application.AuthorOperations.Commands.CreateAuthor;
using FluentAssertions;
using FluentValidation;
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
    public class CreateAuthorCommandValidatorTest : IClassFixture<CommonTestFixture>
    {
        [Theory]
        [InlineData("Emre", "G")]
        [InlineData("E", "Gelen")]
        [InlineData("E", "l")]
        public void WhenInvalidInputsAreGiven_Validator_ShouldReturnErrors(string name, string surname)
        {
            //Arrange : Preparing
            CreateAuthorCommand command = new CreateAuthorCommand(null, null);
            command.Model = new CreateAuthorModel()
            {
                Name = name,
                Surname = surname,
                BirthDate = DateTime.Now.Date.AddYears(-20)
            };

            CreateAuthorCommandValidator validator = new CreateAuthorCommandValidator();

            //Act : Running && Assert : Validation
            FluentActions
                .Invoking(() => validator.ValidateAndThrow(command))
                .Should().Throw<FluentValidation.ValidationException>();
        }

        [Fact]
        public void WhenDateTimeLessThen15IsGiven_Validator_ShouldReturnError()
        {
            //Arrange : Preparing
            CreateAuthorCommand command = new CreateAuthorCommand(null, null);
            command.Model = new CreateAuthorModel()
            {
                Name = "DateTimeTestName",
                Surname = "DateTimeTestSurname",
                BirthDate = DateTime.Now.Date.AddYears(-10)
            };

            CreateAuthorCommandValidator validator = new CreateAuthorCommandValidator();

            //Act : Running && Assert : Validation
            FluentActions
               .Invoking(() => validator.ValidateAndThrow(command))
               .Should().Throw<FluentValidation.ValidationException>();
        }

        [Fact]
        public void WhenValidInputsAreGiven_Validator_ShouldNotReturnError()
        {
            //Arrange : Preparing
            CreateAuthorCommand command = new CreateAuthorCommand(null, null);
            command.Model = new CreateAuthorModel()
            {
                Name = "DateTimeTestName",
                Surname = "DateTimeTestSurname",
                BirthDate = DateTime.Now.Date.AddYears(-16)
            };

            CreateAuthorCommandValidator validator = new CreateAuthorCommandValidator();

            //Act : Running && Assert : Validation
            FluentActions
               .Invoking(() => validator.ValidateAndThrow(command))
               .Should().NotThrow();
        }
    }
}
