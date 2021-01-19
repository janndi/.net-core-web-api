using Infrastructure.Persistence.Interface;
using Infrastructure.Persistence.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Extension
{
    public static class InfraDependencyResolver
    {
        public static void RegisterInfraServices(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();

            services.AddScoped<IMailServiceRepository, MailServiceRepository>();
        }
    }
}
