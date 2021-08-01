using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Clinic.Domain.Models.DTOs.Medic
{
    public class MedicUpdateDTO
    {
        [BindProperty(Name = "Medic.Id")]
        public int Id{ get; set; }

        [Required(ErrorMessage = "El campo es obligatorio.")]
        [Display(Name = "Consultorio")]
        [BindProperty(Name = "Medic.IdConsultingRoom")]
        public int IdConsultingRoom { get; set; }

        [Required(ErrorMessage = "El campo es obligatorio.")]
        [Display(Name = "Especialidad Médica")]
        [BindProperty(Name = "Medic.IdMedicalSpecialty")]
        public int IdMedicalSpecialty { get; set; }

        [BindProperty(Name = "Medic.Names")]
        public string Names { get; set; }

        [BindProperty(Name = "Medic.Surnames")]
        public string Surnames { get; set; }

        [Display(Name = "Identificación")]
        [BindProperty(Name = "Medic.Identification")]
        public int Identification { get; set; }

        [Display(Name = "Nombre")]
        public string FullName => $"{Names} {Surnames}";
    }
}
