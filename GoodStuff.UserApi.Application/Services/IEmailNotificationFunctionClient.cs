namespace GoodStuff.UserApi.Application.Services;

public interface IEmailNotificationFunctionClient
{
    Task SendVerificationEmail(string userEmail, Guid key);
}