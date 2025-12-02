using MediatR;

namespace Model.Features.User.Commands.SignOutCommand;

public class SignOutCommand:  IRequest<bool>
{
    public string SessionId { get; set; }
}
