using MediatR;

namespace Model.Features.User.Commands;

public record SignUpCommand : IRequest<bool>
{
    public string Name { get; init; }
    public string Surname { get; init; }
    public string Password { get; init; }
    public string Email { get; init; }
}
