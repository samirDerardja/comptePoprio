using System.Net;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using dataApp.API.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using AutoMapper;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace dataApp.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            LogManager.LoadConfiguration(String.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));;
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.ConfigureCors();
            services.ConfigureIISIntegration();
            services.AddControllers();
            services.ConfigureRepositoryWrapper();
            services.ConfigureMySqlContext(Configuration);
            services.AddAutoMapper(typeof(Startup));
            //*Chaque fois que nous voulons utiliser un service d'enregistrement, 
            //*tout ce que nous devons faire est de l'injecter dans le constructeur de la classe 
            //*qui va utiliser ce service. .NET Core servira ce service à partir du conteneur IOC
            //* et toutes ses fonctionnalités seront disponibles à l'utilisation.
            //*  Ce type d'injection d'objets est appelé injection de dépendance. 
            services.ConfigureLoggerService();

                services.AddCors(options => options.AddPolicy("ApiCorsPolicy", builderPolicy =>
                 {
        builderPolicy.WithOrigins("http://localhost:4200").AllowAnyMethod().AllowAnyHeader();
                 }));
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
         {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }else
    {
        app.UseExceptionHandler(builder => {

            builder.Run(async context => {
                   var factory = context.RequestServices.GetRequiredService<ProblemDetailsFactory>();
                var problem = factory.CreateProblemDetails(context, statusCode: 500);
                context.Response.StatusCode = problem.Status ?? 500;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(System.Text.Json.JsonSerializer.Serialize(problem));
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                var error = context.Features.Get<IExceptionHandlerFeature>();

                if(error != null)
                {
                    context.Response.AddApplicationError(error.Error.Message);
                    await context.Response.WriteAsync(error.Error.Message);
                }
            });
        });
     
    }
      app.UseStatusCodePages(async context =>
{
    context.HttpContext.Response.ContentType = "text/plain";

    await context.HttpContext.Response.WriteAsync(
        "Status code page, status code: " + 
        context.HttpContext.Response.StatusCode);
});


            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseCors("ApiCorsPolicy");

            app.UseDefaultFiles();
            app.UseStaticFiles();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
