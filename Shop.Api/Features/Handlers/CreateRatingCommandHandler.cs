using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Shop.Api.Features.Commands;
using Shop.Api.Infrastructure.Core.Commands;
using Shop.Api.Infrastructure.Exceptions;
using Shop.Api.Repositories;
using Shop.Contracts.V1.Responses;
using Shop.DataAccess.Entities;

namespace Shop.Api.Features.Handlers
{
    public class CreateRatingCommandHandler : ICommandHandler<CreateRatingCommand, RatingResponse>
    {
        private readonly IMapper mapper;
        private readonly IProductRepository productRepository;
        private readonly IRatingRepository ratingRepository;

        public CreateRatingCommandHandler(
            IRatingRepository ratingRepository,
            IProductRepository productRepository,
            IMapper mapper)
        {
            this.ratingRepository = ratingRepository;
            this.productRepository = productRepository;
            this.mapper = mapper;
        }

        public async Task<RatingResponse> Handle(
            CreateRatingCommand request,
            CancellationToken cancellationToken)
        {
            if (!productRepository.DoesProductExist(request.ProductId))
            {
                throw new ValidationException(new ErrorModel
                {
                    FieldName = nameof(request.ProductId),
                    Message = $"Could not find {nameof(Product)} with id '{request.ProductId}'"
                });
            }

            var rating = new Rating
            {
                Comment = request.Comment,
                ProductId = request.ProductId,
                UserId = request.UserId,
                Value = request.Value
            };

            await ratingRepository.AddAsync(rating).ConfigureAwait(false);
            await ratingRepository.SaveAsync().ConfigureAwait(false);

            return mapper.Map<RatingResponse>(rating);
        }
    }
}