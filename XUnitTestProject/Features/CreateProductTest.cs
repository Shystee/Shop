using System.Threading.Tasks;
using AutoFixture;
using FluentAssertions;
using Shop.Api.Features.Commands;
using Shop.Api.Infrastructure.Exceptions;
using Xunit;
using XUnitTestProject.Common;

namespace XUnitTestProject.Features
{
    public class CreateProductTest : TestBase
    {
        [Fact]
        public async Task ThrowValidationExceptionWhenDescriptionIsEmpty()
        {
            var command = Fixture.Build<CreateProductCommand>()
                                 .With(x => x.Description, string.Empty)
                                 .Create();

            await Assert.ThrowsAsync<ValidationException>(() => Mediator.Send(command));
        }

        [Fact]
        public async Task ThrowValidationExceptionWhenDescriptionIsMissing()
        {
            var command = Fixture.Build<CreateProductCommand>()
                                 .Without(x => x.Description)
                                 .Create();

            await Assert.ThrowsAsync<ValidationException>(() => Mediator.Send(command));
        }

        [Fact]
        public async Task ThrowValidationExceptionWhenNameIsEmpty()
        {
            var command = Fixture.Build<CreateProductCommand>()
                                 .With(x => x.Name, string.Empty)
                                 .Create();

            await Assert.ThrowsAsync<ValidationException>(() => Mediator.Send(command));
        }

        [Fact]
        public async Task ThrowValidationExceptionWhenNameIsMissing()
        {
            var command = Fixture.Build<CreateProductCommand>()
                                 .Without(x => x.Name)
                                 .Create();

            await Assert.ThrowsAsync<ValidationException>(() => Mediator.Send(command));
        }

        [Fact]
        public async Task ThrowValidationExceptionWhenPriceIsMissing()
        {
            var command = Fixture.Build<CreateProductCommand>()
                                 .Without(x => x.Price)
                                 .Create();

            await Assert.ThrowsAsync<ValidationException>(() => Mediator.Send(command));
        }

        [Fact]
        public async Task CreateProductProvidingValidInfo()
        {
            var command = Fixture.Build<CreateProductCommand>()
                                .Create();

            var result = await Mediator.Send(command);

            result.Should().NotBeNull();
        }
    }
}