namespace GoodStuff.UserApi.Application.Services.Interfaces;

public interface IEmailNotificationFunctionClient
{
    Task SendVerificationEmail(string userEmail, Guid key);
}