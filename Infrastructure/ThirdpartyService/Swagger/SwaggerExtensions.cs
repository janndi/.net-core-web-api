using Infrastructure.ThirdpartyService.IndentityServer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;

namespace Infrastructure.ThirdpartyService.Swagger
{
    public static class SwaggerExtensions
    {
        public static IServiceCollection AddSwagger(this IServiceCollection services, IConfiguration Configuration)
        {
            var options = new SwaggerOptions();
            Configuration.GetSection(nameof(SwaggerOptions)).Bind(options);
            services.Configure<SwaggerOptions>(Configuration.GetSection(nameof(SwaggerOptions)));

            if (string.IsNullOrWhiteSpace(options.Title))
                options.Title = AppDomain.CurrentDomain.FriendlyName.Trim().Trim('_');

            if (string.IsNullOrWhiteSpace(options.Version))
                options.Version = "v1";

            services.AddSwaggerGen(swagger =>
            {
                swagger.SwaggerDoc(options.Version, options);

                swagger.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.OAuth2,
                    Flows = new OpenApiOAuthFlows
                    {
                        Password = new OpenApiOAuthFlow
                        {
                            TokenUrl = new Uri($"{ IdentityServerConfiguration.Instance.IS4URI }connect/token"),
                            Scopes = new Dictionary<string, string>
                            {
                               { "scope", "apiscope" }
                            }
                        }
                    }
                });

                swagger.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "oauth2"
                            },
                            In = ParameterLocation.Header,
                            Type = SecuritySchemeType.OAuth2
                        },
                        new List<string> { "Bearer" }
                    }
                });
            });

            return services;
        }

        public static IApplicationBuilder UseSwagger(this IApplicationBuilder app, IConfiguration Configuration)
        {
            var options = new SwaggerOptions();
            Configuration.GetSection(nameof(SwaggerOptions)).Bind(options);

            if (string.IsNullOrWhiteSpace(options.Title))
            {
                options.Title = AppDomain.CurrentDomain.FriendlyName.Trim().Trim('_');
            }

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint($"/swagger/v1/swagger.json", options.Title);
                c.RoutePrefix = options.RoutePrefix;

                c.OAuthConfigObject = new Swashbuckle.AspNetCore.SwaggerUI.OAuthConfigObject
                {
                    ClientId = IdentityServerConfiguration.Instance.CLIENT_ID,
                    ClientSecret = IdentityServerConfiguration.Instance.CLIENT_SECRET
                };
            });

            return app;
        }
    }
}
