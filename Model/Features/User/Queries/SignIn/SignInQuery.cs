using MediatR;
using Model.DataAccess.DBSets;

namespace Model.Features.User.Queries;

public record SignInQuery : IRequest<Users?>
{
    public string Email { get; init; }
    public string Password { get; init; }
}