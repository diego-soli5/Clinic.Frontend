using Clinic.Domain.Models.DTOs.Medic;
using Clinic.Domain.Models.QueryFilters;
using Clinic.Domain.Models.Responses;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Clinic.Domain.Models.ViewModels.Client.Medic
{
    public class MedicIndexViewModel
    {
        [Display(Name = "Especialidad")]
        public IEnumerable<SelectListItem> MedicSpecialties { get; set; }
        public IEnumerable<MedicListDTO> Medics { get; set; }
        public IEnumerable<MedicDisplayPendingForUpdateDTO> MedicsPendingForUpdate { get; set; }
        public MedicQueryFilter QueryFilters { get; set; }
        public Metadata Metadata { get; set; }
    }
}
