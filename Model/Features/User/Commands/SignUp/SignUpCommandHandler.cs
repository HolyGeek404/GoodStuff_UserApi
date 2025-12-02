using MediatR;
using Model.Services.Interfaces;

namespace Model.Features.User.Commands.SignUp;

public class SignUpCommandHandler(IUserService userService) : IRequestHandler<SignUpCommand, bool>
{
    public Task<bool> Handle(SignUpCommand request, CancellationToken cancellationToken)
    {
        return userService.SignUpAsync(request);
    }
}
