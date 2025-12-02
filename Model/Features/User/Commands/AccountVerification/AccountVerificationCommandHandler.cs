using MediatR;
using Model.Services.Interfaces;

namespace Model.Features.User.Commands.AccountVerification;

public class AccountVerificationCommandHandler(IUserService userService) : IRequestHandler<AccountVerificationCommand, bool>
{
    public async Task<bool> Handle(AccountVerificationCommand request, CancellationToken cancellationToken)
    {
        var result = await userService.ActivateUserAsync(request.Email, request.VerificationKey);
        return result;
    }
}