using Clinic.Domain.Models.DTOs.ConsultingRoom;
using Clinic.Domain.Models.DTOs.Medic;
using Clinic.Domain.Models.QueryFilters;
using Clinic.Domain.Models.Responses;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Clinic.Domain.Abstractions
{
    public interface IMedicService
    {
        Task<DefaultResponseResult<IEnumerable<MedicListDTO>>> GetAllAsync(MedicQueryFilter filters, string authToken);
        Task<DefaultResponseResult<IEnumerable<MedicDisplayPendingForUpdateDTO>>> GetAllMedicsPendingForUpdate(string authToken);
        Task<DefaultResponseResult<MedicPedingUpdateDTO>> GetMedicPendingForUpdate(int idEmployee, string authToken);
        Task<DefaultResponseResult> UpdatePendingMedic(MedicPedingUpdateDTO model, string authToken);
        Task<DefaultResponseResult<IEnumerable<MedicalSpecialtyListDTO>>> GetAllMedicalSpecialties(string authToken);
        Task<DefaultResponseResult<IEnumerable<ConsultingRoomDTO>>> GetAllConsultingRooms(string authToken);
        Task<DefaultResponseResult<MedicUpdateDTO>> GetMedicForEdit(int id, string authToken);
        Task<DefaultResponseResult> EditMedic(MedicUpdateDTO model, string authToken);
    }
}
