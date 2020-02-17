using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Projeto.Business.Interfaces;
using Projeto.Business.Notifications;
using Projeto.Business.Services;
using Projeto.Data.Context;
using Projeto.Data.Repository;

namespace Projeto.API.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services)
        {
            services.AddScoped<ProjetoContext>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IProfileRepository, ProfileRepository>();

            services.AddScoped<INotifier, Notifier>();
            services.AddScoped<IUserService, UserService>();

            return services;
        }
    }
}
