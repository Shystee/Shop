using System.Threading.Tasks;
using AutoFixture;
using Shop.Api.Features.Commands;
using Shop.Api.Infrastructure.Exceptions;
using UnitTests.Common;
using Xunit;

namespace UnitTests.Features
{
    public class RegistrationUserCommandTest : TestBase
    {
        [Fact]
        public async Task ThrowValidationExceptionWhenEmailIsEmpty()
        {
            var command = Fixture.Build<RegistrationUserCommand>()
                                 .With(x => x.Email, string.Empty)
                                 .Create();

            await Assert.ThrowsAsync<ValidationException>(() => Mediator.Send(command));
        }

        [Fact]
        public async Task ThrowValidationExceptionWhenEmailIsMissing()
        {
            var command = Fixture.Build<RegistrationUserCommand>()
                                 .Without(x => x.Email)
                                 .Create();

            await Assert.ThrowsAsync<ValidationException>(() => Mediator.Send(command));
        }

        [Fact]
        public async Task ThrowValidationExceptionWhenPasswordIsEmpty()
        {
            var command = Fixture.Build<RegistrationUserCommand>()
                                 .With(x => x.Password, string.Empty)
                                 .Create();

            await Assert.ThrowsAsync<ValidationException>(() => Mediator.Send(command));
        }

        [Fact]
        public async Task ThrowValidationExceptionWhenPasswordIsMissing()
        {
            var command = Fixture.Build<RegistrationUserCommand>()
                                 .Without(x => x.Password)
                                 .Create();

            await Assert.ThrowsAsync<ValidationException>(() => Mediator.Send(command));
        }
    }
}