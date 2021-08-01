using Clinic.Domain.Models.DTOs.ConsultingRoom;
using Clinic.Domain.Models.DTOs.Medic;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Clinic.Domain.Models.ViewModels.Client.Medic
{
    public class MedicEditViewModel
    {
        public MedicUpdateDTO Medic { get; set; }
        public IEnumerable<ConsultingRoomDTO> ConsultingRooms { private get; set; }
        public IEnumerable<SelectListItem> MedicalSpecialtiesSelectListItems { get; set; }
        public IEnumerable<SelectListItem> ConsultingRoomSelectListItems { get; set; }
    }
}
