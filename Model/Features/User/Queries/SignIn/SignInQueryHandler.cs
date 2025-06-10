using MediatR;
using Model.DataAccess.DBSets;
using Model.Services.Interfaces;

namespace Model.Features.User.Queries.SignIn;

public class SignInQueryHandler(IUserService userService) : IRequestHandler<SignInQuery, Users?>
{
    public Task<Users?> Handle(SignInQuery request, CancellationToken cancellationToken)
    {
        return userService.SignIn(request.Email, request.Password);
    }
}