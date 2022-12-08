using Autofac;
using CSONGE.Messaging.Extensions.Rabbitmq;
using FluentValidation;
using MediatR;
using MediatR.Extensions.FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Order.Dal;
using System.Reflection;

namespace Order.Api
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
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder =>
                {
                    builder.WithOrigins("http://localhost:3000", "http://localhost:50000")
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials();
                });
            });

            ConfigureDatabase(services);

            services.AddAuthorization(options =>
            {
                options.AddPolicy(JwtBearerDefaults.AuthenticationScheme, policy => policy.RequireAuthenticatedUser()
                    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme));
            });

            services.AddMediatR(Assembly.Load("Order.Application"));
            services.AddFluentValidation(new[] { Assembly.Load("Order.Application") });

            services
                .AddRabbitMq(
                    rabbitMqConfigSection: Configuration.GetSection("RabbitMQ"),
                    messagingConfigSection: Configuration.GetSection("Messaging"),
                    sentCommandsAssemblies: new[]
                    {
                        Assembly.Load("Status.Interface"),
                        Assembly.Load("Shipping.Interface"),
                    },
                    consumerAssemblies: new[]
                    {
                        Assembly.Load("Order.Application")
                    }
                );

            services.AddControllers();
            services.AddOpenApiDocument(config =>
            {
                config.Title = "Order API";
                config.Description = "API of Order microservice";
                config.DocumentName = "Order.Api";
            });
        }

        public virtual void ConfigureDatabase(IServiceCollection services)
        {
            services.AddDbContext<OrderDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("OrderConnectionString")));
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(Assembly.Load("Order.Dal"))
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

            app.UseCors();

            app.UseHttpsRedirection();

            app.UseOpenApi();
            app.UseSwaggerUi3();

            app.UseRouting();

             app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
