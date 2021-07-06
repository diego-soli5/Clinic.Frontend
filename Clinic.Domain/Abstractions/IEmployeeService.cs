using Clinic.Domain.Models.DTOs.Employee;
using Clinic.Domain.Models.Responses;
using Clinic.Domain.Models.ViewModels.Admin.Employee;
using Clinic.Domain.QueryFilters;
using System.Threading.Tasks;

namespace Clinic.Domain.Abstractions
{
    public interface IEmployeeService
    {
        Task<EmployeeListViewModel> GetAllAsync(EmployeeQueryFilter filters);
        Task<EmployeeDetailsViewModel> GetByIdAsync(int id);
        Task<EmployeeEditViewModel> GetByIdToUpdateAsync(int id);
        Task<DefaultPutApiResponse> UpdateAsync(EmployeeUpdateDTO model);
        Task<DefaultPostApiResponse> CreateAsync(EmployeeCreateDTO model);
        Task<DefaultDeleteApiResponse> DeleteAsync(int id, string pass);
        Task<DefaultPatchApiResponse> Fire(int id);
        Task<DefaultPatchApiResponse> Activate(int id);
    }
}
