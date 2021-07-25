using Clinic.Data.Abstractions;
using Clinic.Domain.Abstractions;
using Clinic.Domain.Models.DTOs.Employee;
using Clinic.CrossCutting.Routes;
using System.Collections.Generic;
using System.Threading.Tasks;
using Clinic.CrossCutting.Abstractions;
using Clinic.Domain.Models.Responses;
using Clinic.Domain.Models.QueryFilters;

namespace Clinic.Domain.Services
{
    public class EmployeeService : ServiceMiddleware, IEmployeeService
    {
        private readonly IRepository _repository;
        private readonly IUriGenerator _uriGenerator;
        private readonly EmployeeRoutes _employeeRoutes;

        public EmployeeService(IRepository repository,
                               EmployeeRoutes employeeRoutes,
                               IUriGenerator uriGenerator)
        {
            _repository = repository;
            _employeeRoutes = employeeRoutes;
            _uriGenerator = uriGenerator;
        }

        public async Task<DefaultApiResponseResult<IEnumerable<EmployeeListDTO>>> GetAllAsync(EmployeeQueryFilter filters, string token)
        {
            string url = _uriGenerator.CreatePagedListUri(_employeeRoutes.Employee, filters).ToString();

            var response = await _repository.Get<IEnumerable<EmployeeListDTO>>(url, authToken: token);

            ValidateResponse(response);

            return response;
        }

        public async Task<DefaultApiResponseResult<EmployeeUpdateDTO>> GetByIdToUpdateAsync(int id, string token)
        {
            string url = $"{_employeeRoutes.Employee}/{id}";

            var queryParams = new
            {
                isUpdate = true
            };

            url = _uriGenerator.AddQueryStringParams(url, queryParams).ToString();

            var response = await _repository.Get<EmployeeUpdateDTO>(url, authToken: token);

            ValidateResponse(response);

            return response;
        }

        public async Task<DefaultApiResponseResult<EmployeeDTO>> GetByIdAsync(int id, string token)
        {
            string url = $"{_employeeRoutes.Employee}/{id}";

            var queryParams = new
            {
                isUpdate = false
            };

            url = _uriGenerator.AddQueryStringParams(url, queryParams).ToString();

            var response = await _repository.Get<EmployeeDTO>(url, authToken: token);

            ValidateResponse(response);

            return response;
        }

        public async Task<DefaultApiResponseResult> UpdateAsync(EmployeeUpdateDTO model, string token)
        {
            string url = $"{_employeeRoutes.Employee}/{model.Id}";

            var response = await _repository.Put(url, model, token);

            ValidateResponse(response);

            return response;
        }

        public async Task<DefaultApiResponseResult> CreateAsync(EmployeeCreateDTO model, string token)
        {
            string url = _employeeRoutes.Employee;

            var response = await _repository.Post(url, model, token);

            ValidateResponse(response);

            return response;
        }

        public async Task<DefaultApiResponseResult> DeleteAsync(int id, string password, string token)
        {
            string url = $"{_employeeRoutes.Employee}/{id}";

            var response = await _repository.Delete(url, new { password }, token);

            ValidateResponse(response);

            return response;
        }
    }
}
