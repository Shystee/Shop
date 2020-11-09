using Shop.Api.Infrastructure.Core.Commands;
using Shop.Contracts.V1.Responses;

namespace Shop.Api.Features.Commands
{
    public class CreateRatingCommand : ICommand<RatingResponse>
    {
        public string Comment { get; set; }

        public int ProductId { get; set; }

        public string UserId { get; set; }

        public double Value { get; set; }
    }
}