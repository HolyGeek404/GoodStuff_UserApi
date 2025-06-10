using MediatR;
using Model.Features.User.Commands.SignUp;
using Model.Services.Interfaces;

namespace Model.Features.User.Commands;

public class SignUpCommandHandler(IUserService userService) : IRequestHandler<SignUpCommand, bool>
{
    public Task<bool> Handle(SignUpCommand request, CancellationToken cancellationToken)
    {
        return userService.SignUp(request);
    }
}
