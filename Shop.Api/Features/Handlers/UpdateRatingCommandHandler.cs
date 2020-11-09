using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using Shop.Api.Features.Commands;
using Shop.Api.Infrastructure.Core.Commands;
using Shop.Api.Repositories;
using Shop.Contracts.V1.Responses;

namespace Shop.Api.Features.Handlers
{
    public class UpdateRatingCommandHandler : ICommandHandler<UpdateRatingCommand, RatingResponse>
    {
        private readonly IMapper mapper;
        private readonly IRatingRepository ratingRepository;

        public UpdateRatingCommandHandler(IRatingRepository ratingRepository, IMapper mapper)
        {
            this.ratingRepository = ratingRepository;
            this.mapper = mapper;
        }

        public async Task<RatingResponse> Handle(
            UpdateRatingCommand request,
            CancellationToken cancellationToken)
        {
            var rating = await ratingRepository.GetByIdAsync(request.Id).ConfigureAwait(false);

            if (rating is null)
            {
                throw new ArgumentNullException($"Could not find {nameof(rating)} with id '{request.Id}'");
            }

            if (rating.UserId != request.UserId)
            {
                throw new ValidationException($"User doesn't own {nameof(rating)}");
            }

            rating.Comment = request.Comment;
            rating.Value = request.Value;

            await ratingRepository.SaveAsync().ConfigureAwait(false);

            return mapper.Map<RatingResponse>(rating);
        }
    }
}