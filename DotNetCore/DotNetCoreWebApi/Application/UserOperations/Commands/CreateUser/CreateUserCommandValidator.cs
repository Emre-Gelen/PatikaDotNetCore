using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using DotNetCoreWebApi.Common;
using System.Threading.Tasks;

namespace DotNetCoreWebApi.Application.UserOperations.Commands.CreateUser
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
            RuleFor(command => command.Model.Name).MinimumLength(2);
            RuleFor(command => command.Model.Surname).MinimumLength(2);
            RuleFor(command => command.Model.Email).EmailAddress();
            RuleFor(command => command.Model.Password).MinimumLength(8)
                .ChildRules(childRule => childRule.RuleFor(str => str.Any(character => char.IsUpper(character))).Equal(true).WithMessage("Password must contain at least one upper character."))
                .ChildRules(childRule => childRule.RuleFor(str => str.Any(character => char.IsLower(character))).Equal(true).WithMessage("Password must contain at least one lower character."))
                .ChildRules(childRule => childRule.RuleFor(str => str.Any(character => char.IsDigit(character))).Equal(true).WithMessage("Password must contain at least one number."))
                .ChildRules(childRule => childRule.RuleFor(str => str.Any(character => character.IsSpecialCharacter())).Equal(true).WithMessage("Password must contain at least one special character."));
        }
       
    }
}
