using Clinic.Domain.Models.Enumerations;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Clinic.Domain.Models.DTOs.AppUser
{
    [Bind(Prefix = "Employee.AppUser")]
    public class AppUserCreateDTO
    {
        [Display(Name = "Nombre de Usuario")]
        [Required(ErrorMessage = "El campo es obligatorio.")]
        public string UserName { get; set; }

        [Display(Name = "Contraseña")]
        [Required(ErrorMessage = "El campo es obligatorio.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Confirmarción de Contraseña")]
        [Required(ErrorMessage = "El campo es obligatorio.")]
        [DataType(DataType.Password)]
        [Compare(nameof(Password),ErrorMessage = "La confirmación de contraseña debe ser igual a la contraseña.")]
        public string PasswordConfirmation { get; set; }

        [Display(Name = "Tipo de Cuenta")]
        [Required(ErrorMessage = "El campo es obligatorio.")]
        public AppUserRole AppUserRole { get; set; }
    }
}
