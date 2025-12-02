using GoodStuff.UserApi.Domain.Models.User;

namespace GoodStuff.UserApi.Application.Services.Interfaces;

public interface IUserSessionService
{
    string CreateSession(Users user);
    UserSession? GetUserSession();
    bool Validate();
    void ClearUserCachedData(string sessionId);
}