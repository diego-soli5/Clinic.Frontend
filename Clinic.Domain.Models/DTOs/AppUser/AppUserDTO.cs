using Clinic.Domain.Models.Enumerations;
using System.ComponentModel.DataAnnotations;

namespace Clinic.Domain.Models.DTOs.AppUser
{
    public class AppUserDTO
    {
        [Display(Name = "Nombre de Usuario")]
        public string UserName { get; set; }

        public string Password { get; set; }

        [Display(Name = "Tipo de Cuenta")]
        [Required(ErrorMessage = "El campo es obligatorio.")]
        public AppUserRole Role { get; set; }
    }
}
