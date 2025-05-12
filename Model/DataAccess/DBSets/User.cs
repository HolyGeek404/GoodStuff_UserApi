using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Model.DataAccess.DBSets
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Required, StringLength(20)]
        public string Name { get; set; }

        [Required, StringLength(50)]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [PasswordPropertyText]
        public string PasswordHash { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}