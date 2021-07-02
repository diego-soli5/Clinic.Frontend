using Clinic.Domain.Models.DTOs.Api;
using Clinic.Domain.Models.DTOs.Employee;
using Clinic.Domain.QueryFilters;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;


namespace Clinic.Domain.Models.ViewModels.Admin.Employee
{
    public class EmployeeListViewModel
    {
        public IEnumerable<EmployeeListDTO> Employees { get; set; }
        public EmployeeQueryFilter QueryFilters { get; set; }
        public Metadata Metadata { get; set; }
        public List<SelectListItem> SelectListEmployeeStatus
        {
            get => new List<SelectListItem>
            {
                new SelectListItem
                {
                    Text = Enumerations.EmployeeStatus.Active.ToString(),
                    Value = Enumerations.EmployeeStatus.Active.ToString(),
                    Selected = true
                },
                new SelectListItem
                {
                    Text = Enumerations.EmployeeStatus.Fired.ToString(),
                    Value = Enumerations.EmployeeStatus.Fired.ToString()
                }
            };
        }

    }
}

