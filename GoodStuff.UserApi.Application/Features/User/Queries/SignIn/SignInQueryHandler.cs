using GoodStuff.UserApi.Application.Services.Interfaces;
using GoodStuff.UserApi.Infrastructure.DataAccess.DBSets;
using MediatR;

namespace GoodStuff.UserApi.Application.Features.User.Queries.SignIn;

public class SignInQueryHandler(IUserService userService) : IRequestHandler<SignInQuery, Users?>
{
    public Task<Users?> Handle(SignInQuery request, CancellationToken cancellationToken)
    {
        return userService.SignInAsync(request.Email, request.Password);
    }
}