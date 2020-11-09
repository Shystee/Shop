using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Shop.Api.Features.Commands;

namespace Shop.Api.Validators
{
    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidator()
        {
            RuleFor(x => x.Description).NotEmpty();
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Price).NotEmpty();
        }
    }
}