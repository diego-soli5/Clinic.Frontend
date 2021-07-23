using System;
using System.ComponentModel.DataAnnotations;

namespace Clinic.Domain.Models.DTOs.Person
{
    public class PersonDTO
    {
        public int Id { get; set; }
        [Display(Name = "Identificación:")]
        public int Identification { get; set; }

        [Display(Name = "Nombres:")]
        public string Names { get; set; }

        [Display(Name = "Apellidos:")]
        public string Surnames { get; set; }

        [Display(Name = "Correo Electronico:")]
        public string Email { get; set; }

        [Display(Name = "Dirección:")]
        public string Address { get; set; }

        [Display(Name = "Número de Telefono:")]
        public int PhoneNumber { get; set; }

        [Display(Name = "Fecha de Nacimiento:")]
        public DateTime Birthdate { get; set; }

        public string ImageName { get; set; }

        [Display(Name = "Nombre Completo")]
        public string FullName => $"{Names} {Surnames}";
        public string FormatedBirthdate => Birthdate.ToShortDateString();
    }
}
