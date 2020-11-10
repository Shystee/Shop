using System.Threading.Tasks;
using AutoFixture;
using Shop.Api.Features.Commands;
using Shop.Api.Infrastructure.Exceptions;
using UnitTests.Common;
using Xunit;

namespace UnitTests.Features
{
    public class RefreshTokenCommandTest : TestBase
    {
        [Fact]
        public async Task ThrowValidationExceptionWhenRefreshTokenIsEmpty()
        {
            var command = Fixture.Build<RefreshTokenCommand>()
                                 .With(x => x.RefreshToken, string.Empty)
                                 .Create();

            await Assert.ThrowsAsync<ValidationException>(() => Mediator.Send(command));
        }

        [Fact]
        public async Task ThrowValidationExceptionWhenRefreshTokenIsMissing()
        {
            var command = Fixture.Build<RefreshTokenCommand>()
                                 .Without(x => x.RefreshToken)
                                 .Create();

            await Assert.ThrowsAsync<ValidationException>(() => Mediator.Send(command));
        }

        [Fact]
        public async Task ThrowValidationExceptionWhenTokenIsEmpty()
        {
            var command = Fixture.Build<RefreshTokenCommand>()
                                 .With(x => x.Token, string.Empty)
                                 .Create();

            await Assert.ThrowsAsync<ValidationException>(() => Mediator.Send(command));
        }

        [Fact]
        public async Task ThrowValidationExceptionWhenTokenIsMissing()
        {
            var command = Fixture.Build<RefreshTokenCommand>()
                                 .Without(x => x.Token)
                                 .Create();

            await Assert.ThrowsAsync<ValidationException>(() => Mediator.Send(command));
        }
    }
}