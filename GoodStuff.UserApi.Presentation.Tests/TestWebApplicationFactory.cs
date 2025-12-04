using GoodStuff.UserApi.Application.Services.Interfaces;
using GoodStuff.UserApi.Infrastructure.DataAccess.Context;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace GoodStuff.UserApi.Presentation.Tests;

public class TestWebApplicationFactory : WebApplicationFactory<Program>
{
    public Mock<IUserSessionService> SessionServiceMock { get; } = new();

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            // Remove real DB
            var descriptor = services.SingleOrDefault(
                d => d.ServiceType == typeof(DbContextOptions<GoodStuffContext>));
            if (descriptor != null)
                services.Remove(descriptor);

            // Add InMemory EF
            services.AddDbContext<GoodStuffContext>(options =>
            {
                options.UseInMemoryDatabase("TestDb_" + Guid.NewGuid());
            });

            // Replace session service
            services.AddSingleton(SessionServiceMock.Object);
        });
    }
}