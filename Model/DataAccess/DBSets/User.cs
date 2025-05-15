using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Model.DataAccess.DBSets;

public class User
{
    [Key]
    public int UserId { get; set; }

    [Required, StringLength(10)]
    public string Name { get; set; }

    [Required, StringLength(15)]
    public string Surname { get; set; }

    [Required, StringLength(50)]
    [EmailAddress]
    public string Email { get; set; }

    [Required, StringLength(16)]
    [PasswordPropertyText]
    public string Password { get; set; }

    [Required]
    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }
}