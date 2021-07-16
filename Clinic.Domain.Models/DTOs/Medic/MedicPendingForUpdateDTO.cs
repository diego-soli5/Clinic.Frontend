namespace Clinic.Domain.Models.DTOs.Medic
{
    public class MedicPendingForUpdateDTO
    {
        public int EmployeeId { get; set; }
        public int Identification { get; set; }
        public string Names { get; set; }
        public string Surnames { get; set; }

        public string FullName { get { return $"{Names} {Surnames}"; } }
    }
}
