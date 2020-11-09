using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

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