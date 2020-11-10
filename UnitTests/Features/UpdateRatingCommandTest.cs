using System.Threading.Tasks;
using AutoFixture;
using Shop.Api.Features.Commands;
using Shop.Api.Infrastructure.Exceptions;
using UnitTests.Common;
using Xunit;

namespace UnitTests.Features
{
    public class UpdateRatingCommandTest : TestBase
    {
        [Fact]
        public async Task ThrowValidationExceptionWhenCommentIsEmpty()
        {
            var command = Fixture.Build<UpdateRatingCommand>()
                                 .With(x => x.Comment, string.Empty)
                                 .Create();

            await Assert.ThrowsAsync<ValidationException>(() => Mediator.Send(command));
        }

        [Fact]
        public async Task ThrowValidationExceptionWhenCommentIsMissing()
        {
            var command = Fixture.Build<UpdateRatingCommand>()
                                 .Without(x => x.Comment)
                                 .Create();

            await Assert.ThrowsAsync<ValidationException>(() => Mediator.Send(command));
        }

        [Fact]
        public async Task ThrowValidationExceptionWhenCommentIdMissing()
        {
            var command = Fixture.Build<UpdateRatingCommand>()
                                 .Without(x => x.Id)
                                 .Create();

            await Assert.ThrowsAsync<ValidationException>(() => Mediator.Send(command));
        }


        [Fact]
        public async Task ThrowValidationExceptionWhenValueIsGreaterThan5()
        {
            var command = Fixture.Build<UpdateRatingCommand>()
                                 .With(x => x.Value, 8)
                                 .Create();

            await Assert.ThrowsAsync<ValidationException>(() => Mediator.Send(command));
        }

        [Fact]
        public async Task ThrowValidationExceptionWhenValueIsLessThan0()
        {
            var command = Fixture.Build<UpdateRatingCommand>()
                                 .With(x => x.Value, -1)
                                 .Create();

            await Assert.ThrowsAsync<ValidationException>(() => Mediator.Send(command));
        }

        [Fact]
        public async Task ThrowValidationExceptionWhenUserIsEmpty()
        {
            var command = Fixture.Build<UpdateRatingCommand>()
                                 .With(x => x.UserId, string.Empty)
                                 .Create();

            await Assert.ThrowsAsync<ValidationException>(() => Mediator.Send(command));
        }

        [Fact]
        public async Task ThrowValidationExceptionWhenUserIsMissing()
        {
            var command = Fixture.Build<UpdateRatingCommand>()
                                 .Without(x => x.UserId)
                                 .Create();

            await Assert.ThrowsAsync<ValidationException>(() => Mediator.Send(command));
        }
    }
}