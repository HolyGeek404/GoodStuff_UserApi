using System.Net;
using GoodStuff.UserApi.Application.Services;
using GoodStuff.UserApi.Domain.Models.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Moq;

namespace GoodStuff.UserApi.Application.Tests.Services;

public class UserSessionServiceTests
{
    private readonly Mock<IMemoryCache> _cacheMock;
    private readonly Mock<IHttpContextAccessor> _httpContextAccessorMock;
    private readonly UserSessionService _service;

    public UserSessionServiceTests()
    {
        _cacheMock = new Mock<IMemoryCache>();
        _httpContextAccessorMock = new Mock<IHttpContextAccessor>();
        var loggerMock = new Mock<ILogger<UserSessionService>>();

        _service = new UserSessionService(
            _cacheMock.Object,
            _httpContextAccessorMock.Object,
            loggerMock.Object
        );
    }

    private static DefaultHttpContext CreateHttpContext(string sessionId = "abc123", string ip = "1.1.1.1")
    {
        var context = new DefaultHttpContext
        {
            Request =
            {
                Cookies = new RequestCookieCollection(
                    new Dictionary<string, string>
                    {
                        { "UserSessionId", sessionId }
                    })
            },
            Connection =
            {
                RemoteIpAddress = IPAddress.Parse(ip)
            }
        };

        return context;
    }

    [Fact]
    public void CreateSession_Should_Create_Session_And_Return_SessionId()
    {
        // Arrange
        var cacheEntryMock = new Mock<ICacheEntry>();
        _cacheMock
            .Setup(c => c.CreateEntry(It.IsAny<object>()))
            .Returns(cacheEntryMock.Object);

        var user = new Users { Email = "test@example.com" };
        _httpContextAccessorMock.Setup(x => x.HttpContext).Returns(CreateHttpContext());

        // Act
        var sessionId = _service.CreateSession(user);

        // Assert
        Assert.False(string.IsNullOrEmpty(sessionId));

        _cacheMock.Verify(
            c => c.CreateEntry(It.Is<string>(k => k.Contains("user_session_"))),
            Times.Once);
    }

    [Fact]
    public void GetUserSession_Should_Return_Session_When_Exists()
    {
        // Arrange
        var userSession = new UserSession
        {
            UserData = new Users { Email = "test@example.com" },
            IpAddress = "1.1.1.1",
            LastActivity = DateTime.UtcNow.AddMinutes(-1)
        };

        var context = CreateHttpContext();
        _httpContextAccessorMock.Setup(x => x.HttpContext).Returns(context);

        object? outValue = userSession;

        _cacheMock.Setup(x => x.TryGetValue(It.IsAny<object>(), out outValue))
            .Returns(true);

        // Act
        var result = _service.GetUserSession();

        // Assert
        Assert.NotNull(result);
        Assert.Equal("test@example.com", result.UserData.Email);
    }

    [Fact]
    public void GetUserSession_Should_Return_Null_When_No_Cookie()
    {
        // Arrange
        _httpContextAccessorMock.Setup(x => x.HttpContext)
            .Returns(new DefaultHttpContext());

        // Act
        var result = _service.GetUserSession();

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public void Validate_Should_Return_True_When_Session_Valid()
    {
        // Arrange
        var session = new UserSession
        {
            IpAddress = "1.1.1.1",
            LastActivity = DateTime.UtcNow.AddMinutes(-10)
        };

        var context = CreateHttpContext();
        _httpContextAccessorMock.Setup(a => a.HttpContext).Returns(context);

        object? outValue = session;

        _cacheMock.Setup(x => x.TryGetValue(It.IsAny<object>(), out outValue)).Returns(true);

        // Act
        var result = _service.Validate();

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void Validate_Should_Return_False_When_Session_Expired()
    {
        // Arrange
        var session = new UserSession
        {
            IpAddress = "1.1.1.1",
            LastActivity = DateTime.UtcNow.AddMinutes(-40)
        };

        var context = CreateHttpContext();
        _httpContextAccessorMock.Setup(a => a.HttpContext).Returns(context);
        object? outValue = session;

        _cacheMock.Setup(x => x.TryGetValue(It.IsAny<object>(), out outValue)).Returns(true);

        // Act
        var result = _service.Validate();

        // Assert
        Assert.False(result);
        _cacheMock.Verify(x => x.Remove(It.IsAny<string>()), Times.Once);
    }

    [Fact]
    public void Validate_Should_Return_False_When_Ip_Mismatch()
    {
        // Arrange
        var session = new UserSession
        {
            IpAddress = "2.2.2.2",
            LastActivity = DateTime.UtcNow
        };

        var context = CreateHttpContext();
        _httpContextAccessorMock.Setup(a => a.HttpContext).Returns(context);

        object? outValue = session;

        _cacheMock.Setup(x => x.TryGetValue(It.IsAny<object>(), out outValue)).Returns(true);

        // Act
        var result = _service.Validate();

        // Assert
        Assert.False(result);
        _cacheMock.Verify(x => x.Remove(It.IsAny<string>()), Times.Once);
    }

    [Fact]
    public void Validate_Should_Update_LastActivity_When_Valid()
    {
        var session = new UserSession
        {
            IpAddress = "1.1.1.1",
            LastActivity = DateTime.UtcNow.AddMinutes(-5)
        };

        object? outValue = session;
        _cacheMock.Setup(c => c.TryGetValue(It.IsAny<object>(), out outValue)).Returns(true);
        _httpContextAccessorMock.Setup(a => a.HttpContext).Returns(CreateHttpContext());

        _service.Validate();

        Assert.True((DateTime.UtcNow - session.LastActivity).TotalSeconds < 2);
    }

    [Fact]
    public void ClearUserCachedData_Should_Remove_Cache()
    {
        // Arrange
        const string sessionId = "abc123";

        // Act
        _service.ClearUserCachedData(sessionId);

        // Assert
        _cacheMock.Verify(x => x.Remove("user_session_abc123"), Times.Once);
    }
}