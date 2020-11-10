using System;
using AutoFixture;
using AutoMapper;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Shop.Api;
using Shop.Api.Infrastructure.Filters;
using Shop.Api.Repositories;
using Shop.Api.Services;
using Shop.DataAccess;
using StructureMap;

namespace XUnitTestProject.Common
{
    public class TestBase : IDisposable
    {
        protected readonly DataContext Db;
        protected readonly Fixture Fixture;
        protected readonly IMediator Mediator;

        public TestBase()
        {
            var services = new ServiceCollection();

            // Services
            services.AddScoped<IIdentityService, IdentityService>();
            services.AddSingleton<IPaginationService, PaginationService>();
            services.AddSingleton<ISortingService, SortingService>();
            services.AddScoped<IRatingRepository, RatingRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddMediatR(typeof(Startup));
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            services.AddMvc()
                    .AddFluentValidation(cfg =>
                    {
                        cfg.RegisterValidatorsFromAssemblyContaining<Startup>();
                    });
            services.AddAutoMapper(typeof(Startup));

            // Database
            var databaseName = Guid.NewGuid().ToString();
            Db = new DataContext(DatabaseContextMock<DataContext>.InMemoryDatabase());

            // Global objects
            Fixture = new Fixture();

            IContainer container = new Container(cfg =>
            {
                cfg.For<DataContext>().Use(Db);
                cfg.For(typeof(ILogger<>)).Use(typeof(NullLogger<>));
                cfg.Populate(services);
            });

            Mediator = container.GetInstance<IMediator>();
        }

        public void Dispose()
        {
            Db.Dispose();
        }
    }
}