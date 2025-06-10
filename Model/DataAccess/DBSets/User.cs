using System.ComponentModel.DataAnnotations;

namespace Model.DataAccess.DBSets;

public class User
{
    [Key]
    public int UserId { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}