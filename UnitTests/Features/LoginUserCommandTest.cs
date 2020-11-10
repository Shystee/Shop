using System.Threading.Tasks;
using AutoFixture;
using Shop.Api.Features.Commands;
using Shop.Api.Infrastructure.Exceptions;
using UnitTests.Common;
using Xunit;

namespace UnitTests.Features
{
    public class LoginUserCommandTest : TestBase
    {
        [Fact]
        public async Task ThrowValidationExceptionWhenEmailIsMissing()
        {
            var command = Fixture.Build<LoginUserCommand>()
                                 .Without(x => x.Email)
                                 .Create();

            await Assert.ThrowsAsync<ValidationException>(() => Mediator.Send(command));
        }

        [Fact]
        public async Task ThrowValidationExceptionWhenEmailIsNotEmail()
        {
            var command = Fixture.Build<LoginUserCommand>()
                                 .With(x => x.Email, Fixture.Create<string>())
                                 .Create();

            await Assert.ThrowsAsync<ValidationException>(() => Mediator.Send(command));
        }

        [Fact]
        public async Task ThrowValidationExceptionWhenPasswordIsMissing()
        {
            var command = Fixture.Build<LoginUserCommand>()
                                 .Without(x => x.Password)
                                 .Create();

            await Assert.ThrowsAsync<ValidationException>(() => Mediator.Send(command));
        }
    }
}