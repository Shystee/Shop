using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Shop.Api.Installers
{
    public class DbInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            //services.AddDbContext<DataContext>(options =>
            //        options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
            //services.AddDefaultIdentity<IdentityUser>()
            //        .AddRoles<IdentityRole>()
            //        .AddEntityFrameworkStores<DataContext>();
        }
    }
}