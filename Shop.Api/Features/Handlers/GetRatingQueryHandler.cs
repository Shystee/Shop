using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Shop.Api.Features.Queries;
using Shop.Api.Infrastructure.Core.Queries;
using Shop.Api.Repositories;
using Shop.Contracts.V1.Responses;

namespace Shop.Api.Features.Handlers
{
    public class GetRatingQueryHandler : IQueryHandler<GetRatingQuery, RatingResponse>
    {
        private readonly IMapper mapper;
        private readonly IRatingRepository ratingRepository;

        public GetRatingQueryHandler(IRatingRepository ratingRepository, IMapper mapper)
        {
            this.ratingRepository = ratingRepository;
            this.mapper = mapper;
        }

        public async Task<RatingResponse> Handle(GetRatingQuery request, CancellationToken cancellationToken)
        {
            var rating = await ratingRepository.GetByIdAsync(request.RatingId).ConfigureAwait(false);

            if (rating is null)
            {
                throw new ArgumentNullException(
                    $"Could not find {nameof(rating)} with id '{request.RatingId}'");
            }

            return mapper.Map<RatingResponse>(rating);
        }
    }
}