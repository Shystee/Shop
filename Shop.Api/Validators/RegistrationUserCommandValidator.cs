using FluentValidation;
using Shop.Api.Features.Commands;

namespace Shop.Api.Validators
{
    public class RegistrationUserCommandValidator : AbstractValidator<RegistrationUserCommand>
    {
        public RegistrationUserCommandValidator()
        {
            RuleFor(x => x.Email).NotEmpty();
            RuleFor(x => x.Password).NotEmpty();
        }
    }
}