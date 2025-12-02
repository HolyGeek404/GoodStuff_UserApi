using GoodStuff.UserApi.Application.Services.Interfaces;
using MediatR;

namespace GoodStuff.UserApi.Application.Features.User.Commands.SignUp;

public class SignUpCommandHandler(IUserService userService) : IRequestHandler<SignUpCommand, bool>
{
    public Task<bool> Handle(SignUpCommand request, CancellationToken cancellationToken)
    {
        return userService.SignUpAsync(request);
    }
}