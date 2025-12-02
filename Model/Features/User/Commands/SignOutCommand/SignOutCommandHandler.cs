using MediatR;
using Model.Services.Interfaces;

namespace Model.Features.User.Commands.SignOutCommand;

public class SignOutCommandHandler(IUserSessionService sessionService) : IRequestHandler<SignOutCommand, bool>
{
    public Task<bool> Handle(SignOutCommand request, CancellationToken cancellationToken)
    {
        sessionService.ClearUserCachedData(request.SessionId);
        return Task.FromResult(true);
    }
}