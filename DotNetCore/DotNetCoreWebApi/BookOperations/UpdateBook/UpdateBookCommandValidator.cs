using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetCoreWebApi.BookOperations.UpdateBook
{
    public class UpdateBookCommandValidator : AbstractValidator<UpdateBookCommand>
    {
        public UpdateBookCommandValidator()
        {
            RuleFor(command => command.BookId).GreaterThan(0);
            RuleFor(command => command.Model.Title).NotEmpty().MinimumLength(4);
            RuleFor(command => command.Model.GenreId).IsInEnum().NotEmpty().WithMessage("Genre Id can not be empty.").GreaterThan(0).WithMessage("Genre Id must be greater than 0.");
        }
    }
}
