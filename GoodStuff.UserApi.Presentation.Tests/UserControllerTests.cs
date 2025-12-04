using System.Net;
using System.Net.Http.Json;
using GoodStuff.UserApi.Application.Features.User.Commands.SignUp;
using GoodStuff.UserApi.Application.Features.User.Queries.SignIn;
using GoodStuff.UserApi.Domain.Models.User;
using Moq;

namespace GoodStuff.UserApi.Presentation.Tests;

public class UserControllerTests(TestWebApplicationFactory factory) : IClassFixture<TestWebApplicationFactory>
{
    private readonly HttpClient _client = factory.CreateClient();

    [Fact]
    public async Task SignUp_Should_Return_Created_When_Successful()
    {
        // Arrange
        var cmd = new SignUpCommand
        {
            Email = "test@example.com",
            Name = "John",
            Surname = "Doe",
            Password = "Password123"
        };

        // Act
        var response = await _client.PostAsJsonAsync("/User/signup", cmd);

        // Assert
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);

        var content = await response.Content.ReadFromJsonAsync<dynamic>();
        Assert.Equal("test@example.com", (string)content.email);
    }

    [Fact]
    public async Task SignIn_Should_Succeed_And_Return_SessionId()
    {
        // Arrange - first create a user
        await _client.PostAsJsonAsync("/User/signup", new SignUpCommand
        {
            Email = "john@example.com",
            Name = "John",
            Surname = "Doe",
            Password = "12345"
        });

        // Mock the SessionService
        factory.SessionServiceMock
            .Setup(s => s.CreateSession(It.IsAny<Users>()))
            .Returns("mock-session-id");

        var query = new SignInQuery
        {
            Email = "john@example.com",
            Password = "12345"
        };

        // Act
        var response = await _client.PostAsJsonAsync("/User/signin", query);

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var model = await response.Content.ReadFromJsonAsync<UserModel>();
        Assert.Equal("mock-session-id", model.SessionId);
        Assert.Equal("john@example.com", model.Email);
    }

    [Fact]
    public async Task SignIn_Should_Return_BadRequest_When_Email_Empty()
    {
        var badQuery = new SignInQuery
        {
            Email = "",
            Password = ""
        };

        var response = await _client.PostAsJsonAsync("/User/signin", badQuery);

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }
}