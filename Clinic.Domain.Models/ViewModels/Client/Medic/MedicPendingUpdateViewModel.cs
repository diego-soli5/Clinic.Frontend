﻿using Clinic.Domain.Models.DTOs.Medic;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Clinic.Domain.Models.ViewModels.Client.Medic
{
    public class MedicPendingUpdateViewModel
    {
        public MedicDTO Medic { get; set; }
        public IEnumerable<SelectListItem> MedicalSpecialties { get; set; }
        public IEnumerable<SelectListItem> ConsultingRooms { get; set; }
    }
}
