using Microsoft.Extensions.Logging;
using Model.DataAccess;
using Model.Features.User.Commands.AccountVerification;
using Model.Features.User.Commands.SignOutCommand;

namespace Model.Services;

public static partial class Logs
{
    [LoggerMessage(LogLevel.Information, "Starting SignUp for email: {Email}")]
    public static partial void LogStartingSignupForEmailEmail(ILogger<UserService> logger, string Email);

    [LoggerMessage(LogLevel.Warning, "User with email {Email} already exists.")]
    public static partial void LogUserWithEmailEmailAlreadyExists(ILogger<UserService> logger, string Email);

    [LoggerMessage(LogLevel.Information, "New user created. Email: {Email}, ActivationKey: {ActivationKey}")]
    public static partial void LogNewUserCreatedEmailEmailActivationkeyActivationkey(ILogger<UserService> logger, string Email, Guid? ActivationKey);

    [LoggerMessage(LogLevel.Information, "Verification email sent to {Email}")]
    public static partial void LogVerificationEmailSentToEmail(ILogger<UserService> logger, string Email);

    [LoggerMessage(LogLevel.Error, "Error occurred during SignUp for {Email}")]
    public static partial void LogErrorOccurredDuringSignupForEmail(ILogger<UserService> logger, Exception e ,string Email);

    [LoggerMessage(LogLevel.Information, "Attempting SignIn for email: {Email}")]
    public static partial void LogAttemptingSigninForEmailEmail(ILogger<UserService> logger, string Email);

    [LoggerMessage(LogLevel.Information, "User {Email} successfully signed in.")]
    public static partial void LogUserEmailSuccessfullySignedIn(ILogger<UserService> logger, string Email);

    [LoggerMessage(LogLevel.Warning, "Invalid credentials for {Email}.")]
    public static partial void LogInvalidCredentialsForEmail(ILogger<UserService> logger, string Email);

    [LoggerMessage(LogLevel.Error, "Error occurred during SignIn for {Email}")]
    public static partial void LogErrorOccurredDuringSigninForEmail(ILogger<UserService> logger, Exception e ,string Email);

    [LoggerMessage(LogLevel.Information, "Fetching user by email: {Email}")]
    public static partial void LogFetchingUserByEmailEmail(ILogger<UserService> logger, string Email);

    [LoggerMessage(LogLevel.Error, "Error fetching user by email: {Email}")]
    public static partial void LogErrorFetchingUserByEmailEmail(ILogger<UserService> logger, Exception e ,string Email);

    [LoggerMessage(LogLevel.Information, "Attempting to activate user {Email} with key {Key}")]
    public static partial void LogAttemptingToActivateUserEmailWithKeyKey(ILogger<UserService> logger, string Email, Guid Key);

    [LoggerMessage(LogLevel.Information, "User {Email} successfully activated.")]
    public static partial void LogUserEmailSuccessfullyActivated(ILogger<UserService> logger, string Email);

    [LoggerMessage(LogLevel.Warning, "Activation failed. Invalid key for {Email}.")]
    public static partial void LogActivationFailedInvalidKeyForEmail(ILogger<UserService> logger, string Email);

    [LoggerMessage(LogLevel.Error, "Error occurred while activating user {Email} with key {Key}")]
    public static partial void LogErrorOccurredWhileActivatingUserEmailWithKeyKey(ILogger<UserService> logger, Exception e ,string Email, Guid Key);

    [LoggerMessage(LogLevel.Information, "Called {SignUpName} by {Unknown}")]
    public static partial void LogCalledSignupnameByUnknown(ILogger logger, string SignUpName, string Unknown);

    [LoggerMessage(LogLevel.Information, "Successfully registered new user {Email}. Called by {Unknown}")]
    public static partial void LogSuccessfullyRegisteredNewUserEmailCalledByUnknown(ILogger logger, string Email, string Unknown);

    [LoggerMessage(LogLevel.Information, "Couldn't register user {Email}. Called by {Unknown}")]
    public static partial void LogCouldnTRegisterUserEmailCalledByUnknown(ILogger logger, string Email, string Unknown);

    [LoggerMessage(LogLevel.Information, $"Couldn't sign in because email or password is empty")]
    public static partial void LogCouldnTSignInBecauseEmailOrPasswordIsEmpty(ILogger logger);

    [LoggerMessage(LogLevel.Information, "Successfully signed in user {Email}. Called {SignUpName} by {Unknown}")]
    public static partial void LogSuccessfullySignedInUserEmailCalledSignupnameByUnknown(ILogger logger, string Email, string SignUpName, string Unknown);

    [LoggerMessage(LogLevel.Information, "SignOut request received. SessionId: {SessionId}")]
    public static partial void LogSignoutRequestReceivedSessionidSessionid(ILogger logger, SignOutCommand SessionId);

    [LoggerMessage(LogLevel.Information, "Clearing cached user data for SessionId: {SessionId}")]
    public static partial void LogClearingCachedUserDataForSessionidSessionid(ILogger logger, SignOutCommand SessionId);

    [LoggerMessage(LogLevel.Information, "Successfully signed out user. SessionId: {SessionId}")]
    public static partial void LogSuccessfullySignedOutUserSessionidSessionid(ILogger logger, SignOutCommand SessionId);

    [LoggerMessage(LogLevel.Error, "An error occurred while signing out user. SessionId: {SessionId}")]
    public static partial void LogAnErrorOccurredWhileSigningOutUserSessionidSessionid(ILogger logger,
        Exception sessionId, string SessionId);

    [LoggerMessage(LogLevel.Information, "AccountVerification endpoint called with payload: {Payload}")]
    public static partial void LogAccountverificationEndpointCalledWithPayloadPayload(ILogger logger, AccountVerificationCommand Payload, string unknown = "@Payload");

    [LoggerMessage(LogLevel.Information, "Account verification successful for account: {Account}")]
    public static partial void LogAccountVerificationSuccessfulForAccountAccount(ILogger logger, string Account);

    [LoggerMessage(LogLevel.Error, "Error occurred during verification for account: {Account}")]
    public static partial void LogErrorOccurredDuringVerificationForAccountAccount(ILogger logger, Exception account,
        string Account);

    [LoggerMessage(LogLevel.Information, "Adding user {UserEmail} to database.")]
    public static partial void LogAddingUserUseremailToDatabase(ILogger<UserDao> logger, string UserEmail);

    [LoggerMessage(LogLevel.Information, "Added user {UserEmail} to database.")]
    public static partial void LogAddedUserUseremailToDatabase(ILogger<UserDao> logger, string UserEmail);

    [LoggerMessage(LogLevel.Error, "Couldn't add user {UserEmail} to database because: {ExMessage}.")]
    public static partial void LogCouldnTAddUserUseremailToDatabaseBecauseExmessage(ILogger<UserDao> logger,
        Exception userEmail, string UserEmail, string ExMessage);

    [LoggerMessage(LogLevel.Warning, "User with email {Email} is not active")]
    public static partial void LogUserWithEmailEmailIsNotActive(this ILogger<UserService> logger, string Email);
}