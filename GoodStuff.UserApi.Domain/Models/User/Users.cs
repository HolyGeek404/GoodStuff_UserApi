using System.ComponentModel.DataAnnotations;

namespace GoodStuff.UserApi.Infrastructure.DataAccess.DBSets;

public class Users
{
    [Key] public int UserId { get; set; }

    public string Name { get; set; }
    public string Surname { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public bool IsActive { get; set; }
    public Guid? ActivationKey { get; set; }
}