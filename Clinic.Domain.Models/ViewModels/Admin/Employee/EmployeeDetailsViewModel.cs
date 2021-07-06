using Clinic.Domain.Models.DTOs.Employee;

namespace Clinic.Domain.Models.ViewModels.Admin.Employee
{
    public class EmployeeDetailsViewModel
    {
        public EmployeeDTO Employee { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
    }
}
