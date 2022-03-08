using FluentValidation;

namespace DotNetCoreWebApi.Application.AuthorOperations.Commands.UpdateAuthor
{
    public class UpdateAuthorCommandValidator : AbstractValidator<UpdateAuthorCommand>
    {
        public UpdateAuthorCommandValidator()
        {
            RuleFor(command => command.AuthorId).GreaterThan(0);
            RuleFor(command => command.Model.Name).MinimumLength(4).When(command => command.Model.Name.Trim() != string.Empty);
            RuleFor(command => command.Model.Surname).MinimumLength(4).When(command => command.Model.Surname.Trim() != string.Empty);
        }
    }
}