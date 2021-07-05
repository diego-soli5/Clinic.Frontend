using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;

namespace Clinic.Domain.Models.DTOs.Person
{
    [Bind(Prefix = "Employee.Person")]
    public class PersonUpdateDTO
    {
        
        [Display(Name = "Identificación")]
        [Required(ErrorMessage = "El campo es obligatorio.")]
        public int Identification { get; set; }

        [Display(Name = "Nombres")]
        [Required(ErrorMessage = "El campo es obligatorio.")]
        public string Names { get; set; }

        [Display(Name = "Apellidos")]
        [Required(ErrorMessage = "El campo es obligatorio.")]
        public string Surnames { get; set; }

        [EmailAddress(ErrorMessage = "Ingrese una dirección de correo válido.")]
        [Display(Name = "Correo Electronico")]
        [Required(ErrorMessage = "El campo es obligatorio.")]
        public string Email { get; set; }

        [Display(Name = "Dirección")]
        [Required(ErrorMessage = "El campo es obligatorio.")]
        public string Address { get; set; }

        [Display(Name = "Número de Telefono")]
        [Required(ErrorMessage = "El campo es obligatorio.")]
        public int PhoneNumber { get; set; }

        [Display(Name = "Fecha de Nacimiento")]
        [Required(ErrorMessage = "El campo es obligatorio.")]
        public DateTime Birthdate { get; set; }
    }
}
