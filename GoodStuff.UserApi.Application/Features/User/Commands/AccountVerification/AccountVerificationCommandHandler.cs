using GoodStuff.UserApi.Application.Services.Interfaces;
using MediatR;

namespace GoodStuff.UserApi.Application.Features.User.Commands.AccountVerification;

public class AccountVerificationCommandHandler(IUserService userService)
    : IRequestHandler<AccountVerificationCommand, bool>
{
    public async Task<bool> Handle(AccountVerificationCommand request, CancellationToken cancellationToken)
    {
        var result = await userService.ActivateUserAsync(request.Email, request.VerificationKey);
        return result;
    }
}