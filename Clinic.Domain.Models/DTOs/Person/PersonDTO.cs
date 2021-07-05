using System;
using System.ComponentModel.DataAnnotations;

namespace Clinic.Domain.Models.DTOs.Person
{
    public class PersonDTO
    {
        [Required(ErrorMessage = "El campo es obligatorio.")]
        [Display(Name = "Identificación")]
        public int? Identification { get; set; }

        [Required(ErrorMessage = "El campo es obligatorio.")]
        [Display(Name = "Nombre")]
        public string Names { get; set; }

        [Required(ErrorMessage = "El campo es obligatorio.")]
        [Display(Name = "Apellidos")]
        public string Surnames { get; set; }

        [Required(ErrorMessage = "El campo es obligatorio.")]
        [Display(Name = "Correo Electronico")]
        public string Email { get; set; }

        [Required(ErrorMessage = "El campo es obligatorio.")]
        [Display(Name = "Dirección")]
        public string Address { get; set; }

        [Required(ErrorMessage = "El campo es obligatorio.")]
        [Display(Name = "Número de Telefono")]
        public int? PhoneNumber { get; set; }

        [Display(Name = "Fecha de Nacimiento")]
        public DateTime? Birthdate { get; set; }
    }
}
