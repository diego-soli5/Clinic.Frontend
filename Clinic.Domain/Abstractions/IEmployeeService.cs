using Clinic.Domain.Models.DTOs.Employee;
using Clinic.Domain.Models.QueryFilters;
using Clinic.Domain.Models.Responses;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Clinic.Domain.Abstractions
{
    public interface IEmployeeService
    {
        Task<DefaultResponseResult<IEnumerable<EmployeeListDTO>>> GetAllAsync(EmployeeQueryFilter filters, string token);
        Task<DefaultResponseResult<EmployeeDTO>> GetByIdAsync(int id, string token);
        Task<DefaultResponseResult<EmployeeUpdateDTO>> GetByIdToUpdateAsync(int id, string token);
        Task<DefaultResponseResult> UpdateAsync(EmployeeUpdateDTO model, string token);
        Task<DefaultResponseResult> CreateAsync(EmployeeCreateDTO model, string token);
        Task<DefaultResponseResult> DeleteAsync(int id, string pass, string token);
    }
}
