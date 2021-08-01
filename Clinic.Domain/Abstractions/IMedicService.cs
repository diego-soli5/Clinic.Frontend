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
        Task<DefaultApiResponseResult<IEnumerable<MedicListDTO>>> GetAllAsync(MedicQueryFilter filters, string authToken);
        Task<DefaultApiResponseResult<IEnumerable<MedicDisplayPendingForUpdateDTO>>> GetAllMedicsPendingForUpdate(string authToken);
        Task<DefaultApiResponseResult<MedicPedingUpdateDTO>> GetMedicPendingForUpdate(int idEmployee, string authToken);
        Task<DefaultApiResponseResult> UpdatePendingMedic(MedicPedingUpdateDTO model, string authToken);
        Task<DefaultApiResponseResult<IEnumerable<MedicalSpecialtyListDTO>>> GetAllMedicalSpecialties(string authToken);
        Task<DefaultApiResponseResult<IEnumerable<ConsultingRoomDTO>>> GetAllConsultingRooms(string authToken);
        Task<DefaultApiResponseResult<MedicUpdateDTO>> GetMedicForEdit(int id, string authToken);
        Task<DefaultApiResponseResult> EditMedic(MedicUpdateDTO model, string authToken);
    }
}
