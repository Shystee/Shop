using System.Threading.Tasks;
using AutoFixture;
using Shop.Api.Features.Commands;
using Shop.Api.Infrastructure.Exceptions;
using UnitTests.Common;
using Xunit;

namespace UnitTests.Features
{
    public class CreateRatingTest : TestBase
    {
        [Fact]
        public async Task ThrowValidationExceptionWhenCommentIsEmpty()
        {
            var command = Fixture.Build<CreateRatingCommand>().With(x => x.Comment, string.Empty).Create();

            await Assert.ThrowsAsync<ValidationException>(() => Mediator.Send(command));
        }

        [Fact]
        public async Task ThrowValidationExceptionWhenCommentIsMissing()
        {
            var command = Fixture.Build<CreateRatingCommand>().Without(x => x.Comment).Create();

            await Assert.ThrowsAsync<ValidationException>(() => Mediator.Send(command));
        }

        [Fact]
        public async Task ThrowValidationExceptionWhenProductDoesntExist()
        {
            var command = Fixture.Build<CreateRatingCommand>().With(x => x.ProductId).Create();

            await Assert.ThrowsAsync<ValidationException>(() => Mediator.Send(command));
        }

        [Fact]
        public async Task ThrowValidationExceptionWhenProductIsMissing()
        {
            var command = Fixture.Build<CreateRatingCommand>().Without(x => x.ProductId).Create();

            await Assert.ThrowsAsync<ValidationException>(() => Mediator.Send(command));
        }

        [Fact]
        public async Task ThrowValidationExceptionWhenUserIsEmpty()
        {
            var command = Fixture.Build<CreateRatingCommand>().With(x => x.UserId, string.Empty).Create();

            await Assert.ThrowsAsync<ValidationException>(() => Mediator.Send(command));
        }

        [Fact]
        public async Task ThrowValidationExceptionWhenUserIsMissing()
        {
            var command = Fixture.Build<CreateRatingCommand>().Without(x => x.UserId).Create();

            await Assert.ThrowsAsync<ValidationException>(() => Mediator.Send(command));
        }

        [Fact]
        public async Task ThrowValidationExceptionWhenValueIsGreaterThan5()
        {
            var command = Fixture.Build<CreateRatingCommand>().With(x => x.Value, 8).Create();

            await Assert.ThrowsAsync<ValidationException>(() => Mediator.Send(command));
        }

        [Fact]
        public async Task ThrowValidationExceptionWhenValueIsLessThan0()
        {
            var command = Fixture.Build<CreateRatingCommand>().With(x => x.Value, -1).Create();

            await Assert.ThrowsAsync<ValidationException>(() => Mediator.Send(command));
        }
    }
}