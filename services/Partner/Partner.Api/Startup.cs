using Autofac;
using CSONGE.Messaging.Extensions.Rabbitmq;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Partner.Dal;
using System.Reflection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.IdentityModel.Tokens.Jwt;
using IdentityProvider.Domain;
using IdentityModel;
using NSwag;
using System.Collections.Generic;
using NSwag.AspNetCore;
using Partner.Application.Interfaces;
using Partner.Api.Services;
using NSwag.Generation.Processors.Security;

namespace Partner.Api
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

            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.Authority = Configuration.GetValue<string>("Authentication:Authority");
                    options.Audience = Configuration.GetValue<string>("Authentication:Audience");
                    options.RequireHttpsMetadata = false;
                }
            );

            services.AddAuthorization(options =>
            {
                options.AddPolicy(JwtBearerDefaults.AuthenticationScheme, policy => policy.RequireAuthenticatedUser()
                    .RequireClaim("scope", "full.access")
                    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme));

                options.AddPolicy("Administrator", policy => policy.RequireAuthenticatedUser()
                    .RequireClaim(JwtClaimTypes.Role, UserType.Administrator.ToString())
                    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme));

                options.AddPolicy("Courier", policy => policy.RequireAuthenticatedUser()
                    .RequireClaim(JwtClaimTypes.Role, UserType.Courier.ToString())
                    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme));

                options.AddPolicy("Partner", policy => policy.RequireAuthenticatedUser()
                    .RequireClaim(JwtClaimTypes.Role, UserType.Partner.ToString())
                    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme));
            });

            services.AddHttpContextAccessor();
            services.AddScoped<IIdentityService, IdentityService>();

            services.AddMediatR(Assembly.Load("Partner.Application"));
            //services.AddFluentValidation(new[] { Assembly.Load("Partner.Application") });

            services
                .AddRabbitMq(
                    rabbitMqConfigSection: Configuration.GetSection("RabbitMQ"),
                    messagingConfigSection: Configuration.GetSection("Messaging"),
                    sentCommandsAssemblies: new[]
                    {
                        Assembly.Load("IdentityProvider.Interface")
                    },
                    consumerAssemblies: new[]
                    {
                        Assembly.Load("Partner.Application")
                    }
                );

            services.AddControllers();
            services.AddOpenApiDocument(config =>
            {
                config.Title = "Partner API";
                config.Description = "Partner api";
                config.DocumentName = "Partner";

                config.AddSecurity("OAuth2", new OpenApiSecurityScheme
                {
                    OpenIdConnectUrl =
                        $"{Configuration.GetValue<string>("Authentication:Authority")}/.well-known/openid-configuration",
                    Scheme = "Bearer",
                    Type = OpenApiSecuritySchemeType.OAuth2,
                    Flows = new OpenApiOAuthFlows
                    {
                        AuthorizationCode = new OpenApiOAuthFlow
                        {
                            AuthorizationUrl =
                                $"{Configuration.GetValue<string>("Authentication:Authority")}/connect/authorize",
                            TokenUrl = $"{Configuration.GetValue<string>("Authentication:Authority")}/connect/token",
                            Scopes = new Dictionary<string, string>
                            {
                                { "openid", "OpenId" },
                                { "full.access", "full.access" }
                            }
                        }
                    }
                });

                config.OperationProcessors.Add(new AspNetCoreOperationSecurityScopeProcessor("OAuth2"));
            });
        }

        public virtual void ConfigureDatabase(IServiceCollection services)
        {
            services.AddDbContext<PartnerDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("PartnerConnectionString")));
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(Assembly.Load("Partner.Dal"))
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

            app.UseCors("CorsPolicy");

            app.UseHttpsRedirection();

            app.UseOpenApi();
            app.UseSwaggerUi3(config =>
            {
                config.OAuth2Client = new OAuth2ClientSettings
                {
                    ClientId = "swagger",
                    ClientSecret = null,
                    UsePkceWithAuthorizationCodeGrant = true,
                    ScopeSeparator = " ",
                    Realm = null,
                    AppName = "Partner Swagger Client"
                };
            });

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
