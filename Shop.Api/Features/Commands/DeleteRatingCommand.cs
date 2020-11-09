using Shop.Api.Infrastructure.Core.Commands;

namespace Shop.Api.Features.Commands
{
    public class DeleteRatingCommand : ICommand
    {
        public int RatingId { get; set; }

        public string UserId { get; set; }
    }
}