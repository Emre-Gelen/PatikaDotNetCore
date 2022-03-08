using FluentValidation;

namespace DotNetCoreWebApi.Application.BookOperations.Commands.UpdateBook
{
    public class UpdateBookCommandValidator : AbstractValidator<UpdateBookCommand>
    {
        public UpdateBookCommandValidator()
        {
            RuleFor(command => command.BookId).GreaterThan(0);
            RuleFor(command => command.Model.Title).NotEmpty().MinimumLength(4);
            RuleFor(command => command.Model.AuthorId).GreaterThan(0);
            RuleFor(command => command.Model.GenreId).NotEmpty().WithMessage("Genre Id can not be empty.").GreaterThan(0).WithMessage("Genre Id must be greater than 0.");
        }
    }
}