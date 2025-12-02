using GoodStuff.UserApi.Infrastructure.DataAccess.DBSets;
using MediatR;

namespace GoodStuff.UserApi.Application.Features.User.Queries.SignIn;

public record SignInQuery : IRequest<Users?>
{
    public string Email { get; init; }
    public string Password { get; init; }
}