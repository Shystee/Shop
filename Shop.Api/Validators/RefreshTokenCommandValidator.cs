using FluentValidation;
using Shop.Api.Features.Commands;

namespace Shop.Api.Validators
{
    public class RefreshTokenCommandValidator : AbstractValidator<RefreshTokenCommand>
    {
        public RefreshTokenCommandValidator()
        {
            RuleFor(x => x.RefreshToken).NotEmpty();
            RuleFor(x => x.Token).NotEmpty();
        }
    }
}