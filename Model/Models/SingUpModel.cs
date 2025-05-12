using System.ComponentModel.DataAnnotations;

namespace Model.Models
{
    public class SingUpModel
    {
        [Required(ErrorMessage = "You have to provide your Name")]
        public string Name { get; set; }


        [Required(ErrorMessage = "You have to provide your Surname")]
        public string Surname { get; set; }


        [Required(ErrorMessage = "Email is required")]
        [EmailAddress]
        public string Email { get; set; }


        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }

        [Required]
        [Compare("Password", ErrorMessage = "The passwords does not match.")]
        private string ConfirmPassword {  get; set; }
    }
}