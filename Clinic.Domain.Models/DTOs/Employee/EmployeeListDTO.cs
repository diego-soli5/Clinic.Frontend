using Clinic.Domain.Models.Enumerations;
using System;

namespace Clinic.Domain.Models.DTOs.Employee
{
    public class EmployeeListDTO
    {
        public int Identification { get; set; }
        public string FullName { get; set; }
        public DateTime HireDate { get; set; }
        public EmployeeRole EmployeeRole { get; set; }
        public EmployeeStatus EmployeeStatus { get; set; }
    }
}
