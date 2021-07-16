using Clinic.Domain.Models.QueryFilters;
using Clinic.Domain.Models.ViewModels.Client.Medic;
using System.Threading.Tasks;

namespace Clinic.Domain.Abstractions
{
    public interface IMedicService
    {
        Task<MedicIndexViewModel> GetAllAsync(MedicQueryFilter filters, string authToken);
    }
}
