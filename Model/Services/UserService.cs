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
    public async Task<bool> SignUp(SignUpCommand model)
    {
        var existingUser = await userDao.GetUserByEmail(model.Email);
        if (existingUser != null)
        {
            logger.LogInformation("User {ModelEmail} already exist.", model.Email);
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

        await userDao.SignUp(user);
        await emailNotificationFunctionClient.SendVerificationEmail(user.Email, activationKey);
        
        return true;
    }

    public async Task<Users?> SignIn(string email, string password)
    {
        var user = await userDao.GetUserByEmail(email);
        if (user != null && passwordService.VerifyPassword(password, user.Password)) 
            return user;
        logger.LogInformation("Invalid credentials for {Email}.", email);
        
        return null;

    }
    public async Task<Users?> GetUserByEmail(string email)
    {
        return await userDao.GetUserByEmail(email);
    }
}