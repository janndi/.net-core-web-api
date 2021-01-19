using Api.Configuration;
using Api.Middleware;
using Application.Extention;
using AutoMapper;
using Infrastructure.Extension;
using Infrastructure.Persistence.Context;
using Infrastructure.ThirdpartyService.IndentityServer;
using Infrastructure.ThirdpartyService.MailService;
using Infrastructure.ThirdpartyService.Swagger;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Reflection;
using System.Text;
using System.Text.Json;

namespace Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

            AppSettings.LoadSettings(configuration);
            IdentityServerConfiguration.LoadIS4Settings(configuration);
            MailServiceConfiguration.LoadMailServiceSettings(configuration);
        }

        public void ConfigureServices(IServiceCollection services)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));

            //Register connection object
            var connectionString = AppSettings.Instance.ConnectionString;
            services.AddDbContext<TestDbContext>(options => options.UseSqlServer(connectionString));

            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddRouting(options => options.LowercaseUrls = true);

            services
                .AddCors()
                .AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Latest);
            
            //Identity Server Auth Configuration
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
             .AddIdentityServerAuthentication(options =>
             {
                 options.Authority = IdentityServerConfiguration.Instance.IS4URI;
                 options.RequireHttpsMetadata = false;
                 options.ApiName = "api_is4";
             });

            services.AddMediatR(Assembly.GetExecutingAssembly());

            //Register Extensionss
            services.RegisterDataServices();
            services.RegisterInfraServices();

            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            //Register Swagger Service
            services.AddSwagger(Configuration);

            services.AddControllers()
                    .AddJsonOptions(o =>
                    {
                        o.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                        o.JsonSerializerOptions.DictionaryKeyPolicy = JsonNamingPolicy.CamelCase;
                    });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //Use Swagger
            app.UseSwagger(Configuration);
            app.UseMiddleware(typeof(GlobalException));
            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
