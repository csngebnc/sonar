using CSONGE.Messaging.Extensions.Rabbitmq;
using IdentityProvider.Api.Services;
using IdentityProvider.Dal;
using IdentityProvider.Domain.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Reflection;

namespace IdentityProvider.Api
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
                    builder.WithOrigins(
                        "http://localhost:3000", 
                        "http://localhost:50000", 
                        "https://localhost:5102",
                        "https://localhost:5101"
                        )
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials();
                });
            });

            ConfigureDatabase(services);

            services.AddIdentity<ServiceUser, IdentityRole<Guid>>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddDefaultTokenProviders()
                .AddEntityFrameworkStores<IdentityProviderDbContext>();

            services.AddIdentityServer()
                .AddDeveloperSigningCredential()
                .AddInMemoryPersistedGrants()
                .AddInMemoryIdentityResources(Configuration.GetSection("IdentityServer:IdentityResources"))
                .AddInMemoryApiResources(Configuration.GetSection("IdentityServer:ApiResources"))
                .AddInMemoryApiScopes(Configuration.GetSection("IdentityServer:ApiScopes"))
                .AddInMemoryClients(Configuration.GetSection("IdentityServer:Clients"))
                .AddAspNetIdentity<ServiceUser>()
                .AddProfileService<ProfileService>();

            services.AddAuthorization(options =>
            {
                options.AddPolicy("DefaultPolicy", builder => builder.RequireAuthenticatedUser()
                    .AddAuthenticationSchemes(IdentityConstants.ApplicationScheme));

                options.DefaultPolicy = options.GetPolicy("DefaultPolicy");
            });

            services
                .AddRabbitMq(
                    rabbitMqConfigSection: Configuration.GetSection("RabbitMQ"),
                    messagingConfigSection: Configuration.GetSection("Messaging"),
                    consumerAssemblies: new[]
                    {
                        Assembly.Load("IdentityProvider.Application")
                    }
                );

            services.AddControllers();
            services.AddRazorPages();
        }

        public virtual void ConfigureDatabase(IServiceCollection services)
        {
            services.AddDbContext<IdentityProviderDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors("CorsPolicy");

            app.UseHttpsRedirection();

            app.UseDefaultFiles();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseIdentityServer();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapRazorPages();
            });
        }
    }
}
