using FluentValidation;
using Shop.Api.Features.Commands;

namespace Shop.Api.Validators
{
    public class CreateRatingCommandValidator : AbstractValidator<CreateRatingCommand>
    {
        public CreateRatingCommandValidator()
        {
            RuleFor(x => x.Comment).NotEmpty();
            RuleFor(x => x.ProductId).NotEmpty();
            RuleFor(x => x.UserId).NotEmpty();
            RuleFor(x => x.Value).LessThanOrEqualTo(5).GreaterThanOrEqualTo(0);
        }
    }
}