using Clinic.Domain.Models.Enumerations;

namespace Clinic.Domain.Models.DTOs.Account
{
    public class LoginResultDTO
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public int PhoneNumber { get; set; }
        public string ImageName { get; set; }
        public AppUserRole AppUserRole { get; set; }
        public EmployeeRole EmployeeRole { get; set; }
        public string Token { get; set; }
    }
}
