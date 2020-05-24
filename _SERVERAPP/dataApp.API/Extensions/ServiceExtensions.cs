using Contracts;
using Entities;
using LoggerService;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Repository;

namespace dataApp.API.Extensions
{
    public  static class ServiceExtensions
    {
        public static void ConfigureCors(this IServiceCollection services)
{
      services.AddCors(options =>
      {
          options.AddPolicy("CorsPolicy",
              builder => builder.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader());
      });
}

public static void ConfigureMySqlContext(this IServiceCollection services, IConfiguration config)
{
    var connectionString = config["mysqlconnection:connectionString"];
    services.AddDbContext<RepositoryContext>(o => o.UseMySql(connectionString));
}
public static void ConfigureIISIntegration(this IServiceCollection services)
{
      services.Configure<IISOptions>(options => 
      {
 
      });          
}
public static void ConfigureLoggerService(this IServiceCollection services)
{

    /*
En appelant services.AddSingleton crée le service la première fois que vous le demandez, 
  puis chaque demande suivante appelle la même instance du service. 
Cela signifie que tous les composants partagent le même service chaque fois qu'ils en ont besoin. 
Vous utilisez toujours la même instance */
     services.AddSingleton<ILoggerManager, LoggerManager>();
}
public static void ConfigureRepositoryWrapper(this IServiceCollection services)
{
    services.AddScoped<IRepositoryWrapper, RepositoryWrapper>();
}

    }
}