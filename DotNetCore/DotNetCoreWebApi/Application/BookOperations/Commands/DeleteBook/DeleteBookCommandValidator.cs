using FluentValidation;

namespace DotNetCoreWebApi.Application.BookOperations.Commands.DeleteBook
{
    public class DeleteBookCommandValidator : AbstractValidator<DeleteBookCommand>
    {
        public DeleteBookCommandValidator()
        {
            RuleFor(command => command.BookId).GreaterThan(0).WithMessage("Id must be greater than 0.");
        }
    }
}