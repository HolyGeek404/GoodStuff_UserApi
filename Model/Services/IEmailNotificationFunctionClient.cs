namespace Model.Services;

public interface IEmailNotificationFunctionClient
{
    Task SendVerificationEmail(string userEmail, Guid key);
}