using DotNetCoreWebApi.Application.AuthorOperations.Commands.UpdateAuthor;
using DotNetCoreWebApi.Application.BookOperations.Commands.UpdateBook;
using FluentAssertions;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.UnitTests.TestSetup;
using Xunit;
using static DotNetCoreWebApi.Application.AuthorOperations.Commands.UpdateAuthor.UpdateAuthorCommand;

namespace WebApi.UnitTests.Application.AuthorOperations.Commands.UpdateAuthor
{
    public class UpdateAuthorCommandValidatorTest : IClassFixture<CommonTestFixture>
    {
        [Theory]
        [InlineData(0,"Emre", "Gelen")]
        [InlineData(1,"Emre", "l")]
        [InlineData(1,"E", "Gelen")]
        public void WhenInvalidInputsAreGiven_Validator_ShouldReturnErrors(int authorId,string name, string surname)
        {
            //Arrange : Preparing
            UpdateAuthorCommand command = new UpdateAuthorCommand(null);
            command.AuthorId = authorId;
            command.Model = new UpdateAuthorModel()
            {
                Name = name,
                Surname = surname,
            };

            //Act : Running && Assert : Validation
            UpdateAuthorCommandValidator validator = new UpdateAuthorCommandValidator();

            FluentActions
                .Invoking(() => validator.ValidateAndThrow(command))
                .Should().Throw<FluentValidation.ValidationException>();
        }
        [Fact]
        public void WhenAgeLessThen15IsGiven_Validator_ShouldReturnError()
        {
            //Arrange : Preparing
            UpdateAuthorCommand command = new UpdateAuthorCommand(null);
            command.AuthorId = 1;
            command.Model = new UpdateAuthorModel()
            {
                Name = "Emre",
                Surname = "Gelen",
                BirthDate = DateTime.Now.Date.AddYears(-14)
            };

            //Act : Running && Assert : Validation
            UpdateAuthorCommandValidator validator = new UpdateAuthorCommandValidator();

            FluentActions
                .Invoking(() => validator.ValidateAndThrow(command))
                .Should().Throw<FluentValidation.ValidationException>();
        }
        [Theory]
        [InlineData(1, "Emre", "Gelen")]
        [InlineData(1, "Emre", "Ge")]
        [InlineData(1, "Em", "Gelen")]
        [InlineData(2, "Emr", "Gelen")]
        public void WhenValidInputsAreGiven_Validator_ShouldNotReturnError(int authorId, string name, string surname)
        {
            //Arrange : Preparing
            UpdateAuthorCommand command = new UpdateAuthorCommand(null);
            command.AuthorId = authorId;
            command.Model = new UpdateAuthorModel()
            {
                Name = name,
                Surname = surname,
                BirthDate = DateTime.Now.Date.AddYears(-16)
            };

            //Act : Running && Assert : Validation
            UpdateAuthorCommandValidator validator = new UpdateAuthorCommandValidator();

            FluentActions
                .Invoking(() => validator.ValidateAndThrow(command))
                .Should().NotThrow();
        }
    }
}
