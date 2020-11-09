using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Shop.Api.Features.Queries;
using Shop.Api.Infrastructure.Core.Queries;
using Shop.Api.Repositories;
using Shop.Contracts.V1.Responses;

namespace Shop.Api.Features.Handlers
{
    public class GetRatingsQueryHandler : IQueryHandler<GetRatingsQuery, List<RatingResponse>>
    {
        private readonly IMapper mapper;
        private readonly IRatingRepository ratingRepository;

        public GetRatingsQueryHandler(IRatingRepository ratingRepository, IMapper mapper)
        {
            this.ratingRepository = ratingRepository;
            this.mapper = mapper;
        }

        public async Task<List<RatingResponse>> Handle(
            GetRatingsQuery request,
            CancellationToken cancellationToken)
        {
            var ratings = await ratingRepository
                                .GetAllAsync(request.Filter, request.Pagination, request.Sorting)
                                .ConfigureAwait(false);

            return mapper.Map<List<RatingResponse>>(ratings);
        }
    }
}