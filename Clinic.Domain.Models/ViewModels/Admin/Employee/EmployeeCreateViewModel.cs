using Clinic.Domain.Models.DTOs.Employee;
using Clinic.Domain.Models.Enumerations;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Clinic.Domain.Models.ViewModels.Admin.Employee
{
    public class EmployeeCreateViewModel
    {
        public EmployeeCreateDTO Employee { get; set; }
        public List<SelectListItem> SelectListEmployeeRole
        {
            get
            {
                return new List<SelectListItem>()
                {
                    new SelectListItem
                    {
                        Value = EmployeeRole.Medic.ToString(),
                        Text = EmployeeRole.Medic.ToString(),
                        Selected = Employee?.EmployeeRole == EmployeeRole.Medic
                    },
                    new SelectListItem
                    {
                        Value = EmployeeRole.Secretary.ToString(),
                        Text = EmployeeRole.Secretary.ToString(),
                        Selected = Employee?.EmployeeRole == EmployeeRole.Secretary
                    }
                };
            }
        }
        
        public bool Success { get; set; }
        public string Message { get; set; }

        public EmployeeCreateViewModel()
        {

        }

        public EmployeeCreateViewModel(EmployeeCreateDTO employee)
        {
            Employee = employee;
        }
    }
}
