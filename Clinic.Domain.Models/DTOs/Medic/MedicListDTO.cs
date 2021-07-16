using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Domain.Models.DTOs.Medic
{
    public class MedicListDTO
    {
        public int Id { get; set; }
        public int Identification { get; set; }
        public string FullName { get; set; }
        public string MedicalSpecialtyName { get; set; }
    }
}
