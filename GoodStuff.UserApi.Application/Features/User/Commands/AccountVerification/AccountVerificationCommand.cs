using System.Text.Json.Serialization;
using MediatR;

namespace GoodStuff.UserApi.Application.Features.User.Commands.AccountVerification;

public class AccountVerificationCommand : IRequest<bool>
{
    [JsonPropertyName("userEmail")] public required string Email { get; set; }

    [JsonPropertyName("key")] public Guid VerificationKey { get; set; }
}