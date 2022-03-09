using FluentValidation;
using System;

namespace DotNetCoreWebApi.Application.AuthorOperations.Commands.UpdateAuthor
{
    public class UpdateAuthorCommandValidator : AbstractValidator<UpdateAuthorCommand>
    {
        public UpdateAuthorCommandValidator()
        {
            RuleFor(command => command.AuthorId).GreaterThan(0);
            RuleFor(command => command.Model.Name).MinimumLength(2).When(command => command.Model.Name.Trim() != string.Empty);
            RuleFor(command => command.Model.Surname).MinimumLength(2).When(command => command.Model.Surname.Trim() != string.Empty);
            RuleFor(command => command.Model.BirthDate).LessThan(DateTime.Now.Date.AddYears(-15));
        }
    }
}