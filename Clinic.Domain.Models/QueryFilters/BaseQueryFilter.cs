using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Domain.Models.QueryFilters
{
    public class BaseQueryFilter
    {
        public int? PageSize { get; set; }
        public int? PageNumber { get; set; }
    }
}
