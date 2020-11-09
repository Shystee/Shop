using FluentValidation;
using Shop.Api.Features.Commands;

namespace Shop.Api.Validators
{
    public class DeleteRatingCommandValidator : AbstractValidator<DeleteRatingCommand>
    {
        public DeleteRatingCommandValidator()
        {
            RuleFor(x => x.RatingId).NotEmpty();
            RuleFor(x => x.UserId).NotEmpty();
        }
    }
}