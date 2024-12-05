using System.ComponentModel.DataAnnotations;

namespace Gestion_de_films.ViewModel
{
    public class LoginViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Rememberme")]
        public bool RememberMe { get; set; }
    }
}
