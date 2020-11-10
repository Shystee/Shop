using System.Threading.Tasks;
using AutoFixture;
using FluentAssertions;
using Shop.Api.Features.Commands;
using Shop.Api.Infrastructure.Exceptions;
using Shop.DataAccess.Entities;
using UnitTests.Common;
using Xunit;

namespace UnitTests.Features
{
    public class UpdateProductCommandTest : TestBase
    {
        [Fact]
        public async Task ThrowValidationExceptionWhenDescriptionIsEmpty()
        {
            var command = Fixture.Build<UpdateProductCommand>()
                                 .With(x => x.Description, string.Empty)
                                 .Create();

            await Assert.ThrowsAsync<ValidationException>(() => Mediator.Send(command));
        }

        [Fact]
        public async Task ThrowValidationExceptionWhenDescriptionIsMissing()
        {
            var command = Fixture.Build<UpdateProductCommand>()
                                 .Without(x => x.Description)
                                 .Create();

            await Assert.ThrowsAsync<ValidationException>(() => Mediator.Send(command));
        }

        [Fact]
        public async Task ThrowValidationExceptionWhenIdIsMissing()
        {
            var command = Fixture.Build<UpdateProductCommand>()
                                 .Without(x => x.Id)
                                 .Create();

            await Assert.ThrowsAsync<ValidationException>(() => Mediator.Send(command));
        }

        [Fact]
        public async Task ThrowValidationExceptionWhenNameIsEmpty()
        {
            var command = Fixture.Build<UpdateProductCommand>()
                                 .With(x => x.Name, string.Empty)
                                 .Create();

            await Assert.ThrowsAsync<ValidationException>(() => Mediator.Send(command));
        }

        [Fact]
        public async Task ThrowValidationExceptionWhenNameIsMissing()
        {
            var command = Fixture.Build<UpdateProductCommand>()
                                 .Without(x => x.Name)
                                 .Create();

            await Assert.ThrowsAsync<ValidationException>(() => Mediator.Send(command));
        }

        [Fact]
        public async Task ThrowValidationExceptionWhenPriceIsMissing()
        {
            var command = Fixture.Build<UpdateProductCommand>()
                                 .Without(x => x.Price)
                                 .Create();

            await Assert.ThrowsAsync<ValidationException>(() => Mediator.Send(command));
        }

        [Fact]
        public async Task UpdateProductProvidingValidInfo()
        {
            var product = new Product
            {
                Price = 1,
                Description = "Test",
                Name = "Test"
            };
            Db.Add(product);
            Db.SaveChanges();

            var command = new UpdateProductCommand
            {
                Id = product.Id,
                Price = product.Price,
                Name = "x",
                Description = product.Description
            };
            var result = await Mediator.Send(command);

            result.Name.Should().Be("x");
        }
    }
}