using System.Threading.Tasks;
using AutoFixture;
using Shop.Api.Features.Commands;
using Shop.Api.Infrastructure.Exceptions;
using UnitTests.Common;
using Xunit;

namespace UnitTests.Features
{
    public class DeleteRatingCommandTest : TestBase
    {
        [Fact]
        public async Task ThrowValidationExceptionWhenRatingIsMissing()
        {
            var command = Fixture.Build<DeleteRatingCommand>()
                                 .Without(x => x.RatingId)
                                 .Create();

            await Assert.ThrowsAsync<ValidationException>(() => Mediator.Send(command));
        }

        [Fact]
        public async Task ThrowValidationExceptionWhenUserIsMissing()
        {
            var command = Fixture.Build<DeleteRatingCommand>()
                                 .Without(x => x.UserId)
                                 .Create();

            await Assert.ThrowsAsync<ValidationException>(() => Mediator.Send(command));
        }
    }
}