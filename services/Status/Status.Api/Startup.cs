using Autofac;
using CSONGE.Messaging.Extensions.Rabbitmq;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Status.Dal;
using System.Reflection;

namespace Status.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            ConfigureDatabase(services);

            services.AddMediatR(Assembly.Load("Status.Application"));

            services
                .AddRabbitMq(
                    rabbitMqConfigSection: Configuration.GetSection("RabbitMQ"),
                    messagingConfigSection: Configuration.GetSection("Messaging"),
                    consumerAssemblies: new[]
                    {
                        Assembly.Load("Status.Application")
                    }
                );

            services.AddControllers();
            services.AddOpenApiDocument(config =>
            {
                config.Title = "Status API";
                config.Description = "API of Status microservice";
                config.DocumentName = "Status.Api";
            });
        }

        public virtual void ConfigureDatabase(IServiceCollection services)
        {
            services.AddDbContext<StatusDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("StatusConnectionString")));
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(Assembly.Load("Status.Dal"))
                .Where(x => x.Name.EndsWith("Repository"))
                .AsImplementedInterfaces()
                .InstancePerDependency();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseOpenApi();
            app.UseSwaggerUi3();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
