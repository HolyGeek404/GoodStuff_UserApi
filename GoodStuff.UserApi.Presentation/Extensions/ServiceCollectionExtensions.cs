using Azure.Identity;
using FluentValidation;
using FluentValidation.AspNetCore;
using GoodStuff.UserApi.Application.Features;
using GoodStuff.UserApi.Application.Features.User.Validators.SignUp;
using GoodStuff.UserApi.Application.Services;
using GoodStuff.UserApi.Application.Services.Interfaces;
using GoodStuff.UserApi.Infrastructure;
using GoodStuff.UserApi.Infrastructure.DataAccess;
using GoodStuff.UserApi.Infrastructure.DataAccess.Context;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Azure.Cosmos;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Web;
using Microsoft.OpenApi.Models;

namespace GoodStuff.UserApi.Presentation.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IUserDao, UserDao>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IPasswordService, PasswordService>();
        services.AddScoped<IUserSessionService, UserSessionService>();
        services.AddScoped<IEmailNotificationFunctionClient, EmailNotificationFunctionClient>();

        return services;
    }

    public static IServiceCollection AddMediatRConfig(this IServiceCollection services)
    {
        services.AddMediatR(x => x.RegisterServicesFromAssembly(typeof(SignUpCommandValidator).Assembly));
        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        services.AddValidatorsFromAssemblyContaining<SignUpCommandValidator>();
        services.AddFluentValidationAutoValidation();

        return services;
    }

    public static IServiceCollection AddAzureConfig(this IServiceCollection services,
        IConfigurationManager configuration)
    {
        var azureAd = configuration.GetSection("AzureAd");

        configuration.AddAzureKeyVault(new Uri(azureAd["KvUrl"]!), new DefaultAzureCredential());
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddMicrosoftIdentityWebApi(azureAd);

        return services;
    }

    public static IServiceCollection AddDataBaseConfig(this IServiceCollection services,
        IConfigurationManager configuration)
    {
        services.AddDbContext<GoodStuffContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("SqlDb")));
        services.AddSingleton(s => new CosmosClient(configuration.GetConnectionString("CosmosDB")));
        return services;
    }

    public static IServiceCollection AddSwaggerConfig(this IServiceCollection services, IConfiguration configuration)
    {
        var tenantId = configuration.GetSection("AzureAd")["TenantId"];
        var swaggerScope = configuration.GetSection("Swagger")["Scope"];
        var authority = $"https://login.microsoftonline.com/{tenantId}/v2.0";

        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "GoodStuff User Api Swagger", Version = "v1" });
            c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
            {
                Description = "OAuth2.0 Auth Code with PKCE",
                Name = "oauth2",
                Type = SecuritySchemeType.OAuth2,
                Flows = new OpenApiOAuthFlows
                {
                    AuthorizationCode = new OpenApiOAuthFlow
                    {
                        AuthorizationUrl =
                            new Uri($"https://login.microsoftonline.com/{tenantId}/oauth2/v2.0/authorize"),
                        TokenUrl = new Uri($"https://login.microsoftonline.com/{tenantId}/oauth2/v2.0/token"),
                        Scopes = new Dictionary<string, string>
                        {
                            { $"{swaggerScope}", "Swagger - Local testing" }
                        }
                    }
                }
            });
            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "oauth2" }
                    },
                    new[] { $"{swaggerScope}" }
                }
            });
        });

        return services;
    }
}