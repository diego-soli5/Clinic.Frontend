using Clinic.Domain.Models.DTOs.Employee;
using Clinic.Domain.Models.QueryFilters;
using Clinic.Domain.Models.Responses;
using Clinic.Domain.Models.ViewModels.Admin.Employee;
using System.Threading.Tasks;

namespace Clinic.Domain.Abstractions
{
    public interface IEmployeeService
    {
        Task<EmployeeIndexViewModel> GetAllAsync(EmployeeQueryFilter filters, string token);
        Task<EmployeeDetailsViewModel> GetByIdAsync(int id, string token);
        Task<EmployeeEditViewModel> GetByIdToUpdateAsync(int id, string token );
        Task<DefaultPutApiResponse> UpdateAsync(EmployeeUpdateDTO model, string token);
        Task<DefaultPostApiResponse> CreateAsync(EmployeeCreateDTO model, string token);
        Task<DefaultDeleteApiResponse> DeleteAsync(int id, string pass, string token);

        #region DESECHADO
        //Codigo comentado por posibilidad de reintegrar la funcionalidad
        //Task<DefaultPatchApiResponse> Fire(int id, string token);
        //Task<DefaultPatchApiResponse> Activate(int id, string token);
        #endregion
    }
}
