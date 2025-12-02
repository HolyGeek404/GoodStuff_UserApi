using GoodStuff.UserApi.Domain.Models.User;
using GoodStuff.UserApi.Infrastructure.DataAccess.DBSets;

namespace GoodStuff.UserApi.Application.Services.Interfaces;

public interface IUserSessionService
{
    string CreateSession(Users user);
    UserSession? GetUserSession();
    bool Validate();
    void ClearUserCachedData(string sessionId);
}