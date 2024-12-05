using System.ComponentModel.DataAnnotations;

namespace Gestion_de_films.ViewModel
{
    public class CreateRoleViewModel
    {
        [Required]
        [Display(Name = "Role")]
        public string RoleName { get; set; }
    }
}
