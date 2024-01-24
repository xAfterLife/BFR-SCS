using Core.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var host = GetApplication(args);

host.Run();
return;

static IHost GetApplication(string[] args)
{
    var builder = Host.CreateDefaultBuilder(args);
    builder.ConfigureServices(AddServices);
    return builder.Build();
}

static void AddServices(IServiceCollection services)
{
    var config = new ConfigurationBuilder().AddJsonFile("appsettings.json", true, true).Build();
    services.AddSingleton<IConfiguration>(config);
    services.AddCustomeLogging();
}