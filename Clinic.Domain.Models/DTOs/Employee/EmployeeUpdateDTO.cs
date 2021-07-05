using Clinic.Domain.Models.DTOs.Person;
using Clinic.Domain.Models.Enumerations;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Clinic.Domain.Models.DTOs.Employee
{
    public class EmployeeUpdateDTO
    {
        public int Id { get; set; }

        [Display(Name = "Tipo de Empleado")]
        [Required(ErrorMessage = "El campo es obligatorio.")]
        public EmployeeRole EmployeeRole { get; set; }
        
        public PersonUpdateDTO Person { get; set; }

        [Display(Name = "Imagen")]
        public IFormFile Image { get; set; }
    }
}
