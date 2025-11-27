using Model.DataAccess.DBSets;
using Model.Models.User;

namespace Model.Services;

public interface IUserSessionService
{
    string CreateSession(Users user);
    UserSession? GetUserSession();
    bool Validate();
    void ClearUserCachedData(string sessionId);
}