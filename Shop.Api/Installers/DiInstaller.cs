using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shop.Api.Repositories;

namespace Shop.Api.Installers
{
    public class DiInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            // services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
        }
    }
}