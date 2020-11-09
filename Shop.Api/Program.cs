using System;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace Shop.Api
{
    public class Program
    {
        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                       .ConfigureAppConfiguration(builder =>
                       {
                           builder.SetBasePath(Directory.GetCurrentDirectory())
                                  .AddJsonFile("appsettings.Development.json", false, true)
                                  .AddJsonFile(
                                      $"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json")
                                  .AddEnvironmentVariables();
                       })
                       .UseSerilog((context, configuration) => { configuration.ReadFrom.Configuration(context.Configuration); })
                       .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
        }

        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }
    }
}