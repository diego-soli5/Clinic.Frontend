using Clinic.Domain.Models.QueryFilters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.CrossCutting.Cache
{
    public class MedicCache
    {
        private static MedicQueryFilter MedicQueryFilterCache { get; set; }

        public static void GetMedicQueryFilterCache(MedicQueryFilter filters)
        {
            if (filters.IsPagination)
            {
                filters.Identification ??= MedicQueryFilterCache?.Identification;
                filters.MedicalSpecialty ??= MedicQueryFilterCache?.MedicalSpecialty;
            }
        }

        public static void SetMedicQueryFilterCache(MedicQueryFilter filters)
        {
            MedicQueryFilterCache = filters;
        }
    }
}
