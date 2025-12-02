using Microsoft.Extensions.Logging;
using Model.DataAccess.DBSets;
using Model.DataAccess.Interfaces;
using Model.Features.User.Commands.SignUp;
using Model.Services.Interfaces;

namespace Model.Services;

public class UserService(
    IUserDao userDao,
    IEmailNotificationFunctionClient emailNotificationFunctionClient,
    IPasswordService passwordService,
    ILogger<UserService> logger) : IUserService
{
    public async Task<bool> SignUpAsync(SignUpCommand model)
    {
        try
        {
            Logs.LogStartingSignupForEmailEmail(logger, model.Email);

            var existingUser = await userDao.GetUserByEmailAsync(model.Email);

            if (existingUser != null)
            {
                Logs.LogUserWithEmailEmailAlreadyExists(logger, model.Email);
                return false;
            }

            var activationKey = Guid.NewGuid();

            var user = new Users
            {
                Name = model.Name,
                Surname = model.Surname,
                Email = model.Email,
                Password = passwordService.HashPassword(model.Password),
                CreatedAt = DateTime.UtcNow,
                IsActive = false,
                ActivationKey = activationKey
            };

            await userDao.SignUpAsync(user);

            Logs.LogNewUserCreatedEmailEmailActivationkeyActivationkey(logger, user.Email, user.ActivationKey);

            await emailNotificationFunctionClient.SendVerificationEmail(user.Email, activationKey);

            Logs.LogVerificationEmailSentToEmail(logger, user.Email);

            return true;
        }
        catch (Exception ex)
        {
            Logs.LogErrorOccurredDuringSignupForEmail(logger, ex, model.Email);
            throw;
        }
    }

    public async Task<Users?> SignInAsync(string email, string password)
    {
        try
        {
            Logs.LogAttemptingSigninForEmailEmail(logger, email);

            var user = await userDao.GetUserByEmailAsync(email);

            if (user != null && passwordService.VerifyPassword(password, user.Password))
            {
                Logs.LogUserEmailSuccessfullySignedIn(logger, email);
                return user;
            }

            if (user is { IsActive: false })
            {
                Logs.LogUserWithEmailEmailIsNotActive(logger, email);
                return null;
            }

            Logs.LogInvalidCredentialsForEmail(logger, email);
            return null;
        }
        catch (Exception ex)
        {
            Logs.LogErrorOccurredDuringSigninForEmail(logger, ex, email);
            throw;
        }
    }

    public async Task<Users?> GetUserByEmailAsync(string email)
    {
        try
        {
            Logs.LogFetchingUserByEmailEmail(logger, email);
            return await userDao.GetUserByEmailAsync(email);
        }
        catch (Exception ex)
        {
            Logs.LogErrorFetchingUserByEmailEmail(logger, ex, email);
            throw;
        }
    }

    public async Task<bool> ActivateUserAsync(string email, Guid providedKey)
    {
        try
        {
            Logs.LogAttemptingToActivateUserEmailWithKeyKey(logger, email, providedKey);

            var result = await userDao.ActivateUserAsync(email, providedKey);

            if (result)
                Logs.LogUserEmailSuccessfullyActivated(logger, email);
            else
                Logs.LogActivationFailedInvalidKeyForEmail(logger, email);

            return result;
        }
        catch (Exception ex)
        {
            Logs.LogErrorOccurredWhileActivatingUserEmailWithKeyKey(logger, ex, email, providedKey);
            throw;
        }
    }
}
