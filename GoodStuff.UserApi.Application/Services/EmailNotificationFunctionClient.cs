using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Configuration;

namespace GoodStuff.UserApi.Application.Services;

public class EmailNotificationFunctionClient(HttpClient httpClient, IConfiguration configuration)
    : IEmailNotificationFunctionClient
{
    public async Task SendVerificationEmail(string userEmail, Guid key)
    {
        var uri = new Uri($"{configuration.GetSection("ApiUrls")["EmailNotificationFunction"]!}/verification");

        var body = new { userEmail, key };
        var json = JsonSerializer.Serialize(body);

        var msg = new HttpRequestMessage(HttpMethod.Post, uri)
        {
            Content = new StringContent(json, Encoding.UTF8, "application/json")
        };

        await httpClient.SendAsync(msg);
    }
}