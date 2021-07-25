using Clinic.Domain.Models.DTOs.Employee;
using Clinic.Domain.Models.QueryFilters;
using Clinic.Domain.Models.Responses;
using System.Collections.Generic;


namespace Clinic.Domain.Models.ViewModels.Admin.Employee
{
    public class EmployeeIndexViewModel
    {
        public IEnumerable<EmployeeListDTO> Employees { get; set; }
        public EmployeeQueryFilter QueryFilters { get; set; }
        public Metadata Metadata { get; set; }
    }
}

