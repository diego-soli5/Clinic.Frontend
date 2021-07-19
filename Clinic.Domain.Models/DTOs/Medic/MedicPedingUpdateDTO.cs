using System.ComponentModel.DataAnnotations;

namespace Clinic.Domain.Models.DTOs.Medic
{
    public class MedicPedingUpdateDTO
    {
        public int IdEmployee { get; set; }
        //This info is going to be updated
        [Required(ErrorMessage = "El campo es obligatorio.")]
        [Display(Name = "Consultorio")]
        public int IdConsultingRoom { get; set; }
        [Required(ErrorMessage = "El campo es obligatorio.")]
        [Display(Name = "Especialidad Médica")]
        public int IdMedicalSpecialty { get; set; }

        //Just to display
        public string Names { get; set; }
        public string Surnames { get; set; }
        [Display(Name = "Identificación")]
        public int Identification { get; set; }

        [Display(Name = "Nombre")]
        public string FullName => $"{Names} {Surnames}";
    }
}
