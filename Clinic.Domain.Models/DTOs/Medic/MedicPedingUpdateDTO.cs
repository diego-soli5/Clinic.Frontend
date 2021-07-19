using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Clinic.Domain.Models.DTOs.Medic
{
    public class MedicPedingUpdateDTO
    {
        [BindProperty(Name = "Medic.IdEmployee")]
        public int IdEmployee { get; set; }

        //This info is going to be updated

        [Required(ErrorMessage = "El campo es obligatorio.")]
        [Display(Name = "Consultorio")]
        [BindProperty(Name = "Medic.IdConsultingRoom")]
        public int IdConsultingRoom { get; set; }

        [Required(ErrorMessage = "El campo es obligatorio.")]
        [Display(Name = "Especialidad Médica")]
        [BindProperty(Name = "Medic.IdMedicalSpecialty")]
        public int IdMedicalSpecialty { get; set; }

        //Just to display
        [BindProperty(Name = "Medic.Names")]
        public string Names { get; set; }
        public string Surnames { get; set; }
        [Display(Name = "Identificación")]
        public int Identification { get; set; }

        [Display(Name = "Nombre")]
        public string FullName => $"{Names} {Surnames}";
    }
}
