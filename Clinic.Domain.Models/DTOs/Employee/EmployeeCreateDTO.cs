using Clinic.Domain.Models.DTOs.AppUser;
using Clinic.Domain.Models.DTOs.Person;
using Clinic.Domain.Models.Enumerations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Clinic.Domain.Models.DTOs.Employee
{
    public class EmployeeCreateDTO
    {
        [Display(Name = "Tipo de Empleado")]
        [Required(ErrorMessage = "El campo es obligatorio.")]
        [BindProperty(Name = "Employee.EmployeeRole")]
        public EmployeeRole? EmployeeRole { get; set; }

        public AppUserCreateDTO AppUser { get; set; }

        public PersonCreateDTO Person { get; set; }

        [Display(Name = "Imagen")]
        public IFormFile Image { get; set; }
    }
}
