using Model.DataAccess;
using Model.DataAccess.Interfaces;
using Model.Services;
using Model.Services.Interfaces;

namespace WebApi;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IUserDao, UserDao>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IPasswordService, PasswordService>();
        return services;
    }
}