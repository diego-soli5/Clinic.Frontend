using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Domain.Models.QueryFilters
{
    public class MedicQueryFilter : BaseQueryFilter
    {
        [Display(Name = "Identificación")]
        public int? Identification { get; set; }
        public int? MedicalSpecialty { get; set; }
        public bool IsPagination { get; set; }

        public MedicQueryFilter(int pageNumber)
        {
            PageNumber = pageNumber;
        }

        public MedicQueryFilter()
        {

        }
    }
}
