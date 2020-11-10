using AutoMapper;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Shop.Api.Extensions;
using Shop.Api.Infrastructure.Filters;
using Shop.Api.Repositories;
using Shop.Api.Services;
using Shop.DataAccess;
using Shop.DataAccess.Entities;

namespace Shop.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger(Configuration);
            app.UseSerilogRequestLogging();

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseAuthorization();
            app.UseAuthentication();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers(options => { options.Filters.Add<ExceptionFilter>(); })
                    .AddFluentValidation(mvcConfiguration =>
                            mvcConfiguration.RegisterValidatorsFromAssemblyContaining<Startup>());

            services.AddAutoMapper(typeof(Startup));
            services.AddMediatR(typeof(Startup));

            services.AddDbContext<DataContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddDefaultIdentity<IdentityUser>()
                    .AddRoles<IdentityRole>()
                    .AddEntityFrameworkStores<DataContext>();

            services.AddBearerAuthentication(Configuration);
            services.AddSwagger(Configuration);

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehaviour<,>));

            services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.Decorate<IProductRepository, InMemoryCachedProductRepository>();
            services.AddScoped<IRatingRepository, RatingRepository>();
            services.Decorate<IRatingRepository, InMemoryCachedRatingRepository>();


            services.AddScoped<IIdentityService, IdentityService>();
            services.AddSingleton<IPaginationService, PaginationService>();
            services.AddSingleton<ISortingService, SortingService>();
        }
    }
}