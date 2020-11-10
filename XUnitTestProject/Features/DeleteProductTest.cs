using System;
using System.Threading.Tasks;
using AutoFixture;
using Shop.Api.Features.Commands;
using Shop.Api.Infrastructure.Exceptions;
using Xunit;
using XUnitTestProject.Common;

namespace XUnitTestProject.Features
{
    public class DeleteProductTest : TestBase
    {
        [Fact]
        public async Task ThrowValidationExceptionWhenProductIsMissing()
        {
            var command = Fixture.Build<DeleteProductCommand>()
                                 .Without(x => x.ProductId)
                                 .Create();

            await Assert.ThrowsAsync<ValidationException>(() => Mediator.Send(command));
        }

        [Fact]
        public async Task ThrowValidationExceptionWhenProductDoesntExist()
        {
            var command = Fixture.Build<DeleteProductCommand>()
                                 .With(x => x.ProductId)
                                 .Create();

            await Assert.ThrowsAsync<ArgumentNullException>(() => Mediator.Send(command));
        }

        [Fact]
        public async Task DeleteProductProvidingValidInfo()
        {
            var createCommand = Fixture.Build<CreateProductCommand>().Create();
            var product = await Mediator.Send(createCommand);
            var deleteCommand = new DeleteProductCommand
            {
                ProductId = product.Id
            };
            await Mediator.Send(deleteCommand);
        }
    }
}