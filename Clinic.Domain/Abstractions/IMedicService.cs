using Clinic.Domain.Models.DTOs.Medic;
using Clinic.Domain.Models.QueryFilters;
using Clinic.Domain.Models.ViewModels.Client.Medic;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Clinic.Domain.Abstractions
{
    public interface IMedicService
    {
        Task<MedicIndexViewModel> GetAllAsync(MedicQueryFilter filters, string authToken);
        Task<IEnumerable<MedicPendingForUpdateDTO>> GetAllMedicsPendingForUpdate(string authToken);
    }
}
