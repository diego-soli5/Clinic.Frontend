using Clinic.Domain.Models.DTOs.Employee;
using Clinic.Domain.Models.QueryFilters;
using Clinic.Domain.Models.Responses;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Clinic.Domain.Abstractions
{
    public interface IEmployeeService
    {
        Task<DefaultApiResponseResult<IEnumerable<EmployeeListDTO>>> GetAllAsync(EmployeeQueryFilter filters, string token);
        Task<DefaultApiResponseResult<EmployeeDTO>> GetByIdAsync(int id, string token);
        Task<DefaultApiResponseResult<EmployeeUpdateDTO>> GetByIdToUpdateAsync(int id, string token);
        Task<DefaultApiResponseResult> UpdateAsync(EmployeeUpdateDTO model, string token);
        Task<DefaultApiResponseResult> CreateAsync(EmployeeCreateDTO model, string token);
        Task<DefaultApiResponseResult> DeleteAsync(int id, string pass, string token);
    }
}
