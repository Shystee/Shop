using FluentValidation;
using Shop.Api.Features.Commands;

namespace Shop.Api.Validators
{
    public class UpdateRatingCommandValidator : AbstractValidator<UpdateRatingCommand>
    {
        public UpdateRatingCommandValidator()
        {
            RuleFor(x => x.Comment).NotEmpty();
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.Value).GreaterThanOrEqualTo(0).LessThanOrEqualTo(5);
            RuleFor(x => x.UserId).NotEmpty();
        }
    }
}