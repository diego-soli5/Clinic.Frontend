using Clinic.Domain.Models.Enumerations;
using System;
using System.ComponentModel.DataAnnotations;

namespace Clinic.Domain.Models.QueryFilters
{
    public class EmployeeQueryFilter : BaseQueryFilter
    {
        [Display(Name = "Identificación")]
        public int? Identification { get; set; }
        public DateTime? HireDate { get; set; }
        public EmployeeRole? EmployeeRole { get; set; }
        [Display(Name = "Estado")]
        public EmployeeStatus? EmployeeStatus { get; set; }
        public bool IsPagination { get; set; }

        public EmployeeQueryFilter(int pageNumber)
        {
            PageNumber = pageNumber;
        }

        public EmployeeQueryFilter(int pageNumber, EmployeeStatus employeeStatus)
        {
            PageNumber = pageNumber;
            EmployeeStatus = employeeStatus;
        }

        public EmployeeQueryFilter()
        {

        }
    }
}
