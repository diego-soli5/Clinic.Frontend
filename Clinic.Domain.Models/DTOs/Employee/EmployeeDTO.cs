using Clinic.Domain.Models.DTOs.AppUser;
using Clinic.Domain.Models.DTOs.Person;
using Clinic.Domain.Models.Enumerations;
using System;
using System.ComponentModel.DataAnnotations;

namespace Clinic.Domain.Models.DTOs.Employee
{
    public class EmployeeDTO
    {
        public int Id { get; set; }

        [Display(Name = "Tipo de Empleado")]
        public EmployeeRole EmployeeRole { get; set; }

        [Display(Name = "Estado del Empleado:")]
        public EmployeeStatus EmployeeStatus { get; set; }

        [Display(Name = "Fecha de Contratacion:")]
        public DateTime HireDate { get; set; }

        public PersonDTO Person { get; set; }
    }
}
