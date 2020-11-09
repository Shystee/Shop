using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;

namespace Shop.Api.Infrastructure.Filters
{
    public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly IValidatorFactory validationFactory;

        public ValidationBehavior(IValidatorFactory validationFactory)
        {
            this.validationFactory = validationFactory;
        }

        public async Task<TResponse> Handle(
            TRequest request,
            CancellationToken cancellationToken,
            RequestHandlerDelegate<TResponse> next)
        {
            var validator = validationFactory.GetValidator(request.GetType());
            var result = validator?.Validate(new ValidationContext<TRequest>(request));

            if (result != null && !result.IsValid)
            {
                throw new ValidationException(result.Errors);
            }

            var response = await next();

            return response;
        }
    }
}