﻿using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Shop.Api.Features.Commands;
using Shop.Api.Infrastructure.Core.Commands;
using Shop.Api.Infrastructure.Exceptions;
using Shop.Api.Repositories;
using Shop.Contracts.V1.Responses;

namespace Shop.Api.Features.Handlers
{
    public class DeleteRatingCommandHandler : ICommandHandler<DeleteRatingCommand>
    {
        private readonly IRatingRepository ratingRepository;

        public DeleteRatingCommandHandler(IRatingRepository ratingRepository)
        {
            this.ratingRepository = ratingRepository;
        }

        public async Task<Unit> Handle(DeleteRatingCommand request, CancellationToken cancellationToken)
        {
            var rating = await ratingRepository.GetByIdAsync(request.RatingId).ConfigureAwait(false);

            if (rating is null)
            {
                throw new ArgumentNullException(
                    $"Could not find {nameof(rating)} with id '{request.RatingId}'");
            }

            if (rating.UserId != request.UserId)
            {
                throw new ValidationException(new ErrorModel
                {
                    FieldName = nameof(request.UserId),
                    Message = $"User doesn't own {nameof(rating)}"
                });
            }

            ratingRepository.Remove(rating);
            await ratingRepository.SaveAsync().ConfigureAwait(false);

            return Unit.Value;
        }
    }
}