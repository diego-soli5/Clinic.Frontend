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
        private readonly ApiRoutes _routes;

        public EmployeeService(IRepository repository,
                               ApiRoutes routes,
                               IUriGenerator uriGenerator)
        {
            _repository = repository;
            _routes = routes;
            _uriGenerator = uriGenerator;
        }

        public async Task<DefaultResponseResult<IEnumerable<EmployeeListDTO>>> GetAllAsync(EmployeeQueryFilter filters, string token)
        {
            string url = _uriGenerator.CreatePagedListUri(_routes.EmployeeRoutes.Employee, filters).ToString();

            var response = await _repository.Get<IEnumerable<EmployeeListDTO>>(url, authToken: token);

            ValidateResponse(response);

            return response;
        }

        public async Task<DefaultResponseResult<EmployeeUpdateDTO>> GetByIdToUpdateAsync(int id, string token)
        {
            string url = $"{_routes.EmployeeRoutes.Employee}/{id}";

            var queryParams = new
            {
                isUpdate = true
            };

            url = _uriGenerator.AddQueryStringParams(url, queryParams).ToString();

            var response = await _repository.Get<EmployeeUpdateDTO>(url, authToken: token);

            ValidateResponse(response);

            return response;
        }

        public async Task<DefaultResponseResult<EmployeeDTO>> GetByIdAsync(int id, string token)
        {
            string url = $"{_routes.EmployeeRoutes.Employee}/{id}";

            var queryParams = new
            {
                isUpdate = false
            };

            url = _uriGenerator.AddQueryStringParams(url, queryParams).ToString();

            var response = await _repository.Get<EmployeeDTO>(url, authToken: token);

            ValidateResponse(response);

            return response;
        }

        public async Task<DefaultResponseResult> UpdateAsync(EmployeeUpdateDTO model, string token)
        {
            string url = $"{_routes.EmployeeRoutes.Employee}/{model.Id}";

            var response = await _repository.Put(url, model, token);

            ValidateResponse(response);

            return response;
        }

        public async Task<DefaultResponseResult> CreateAsync(EmployeeCreateDTO model, string token)
        {
            string url = _routes.EmployeeRoutes.Employee;

            var response = await _repository.Post(url, model, token);

            ValidateResponse(response);

            return response;
        }

        public async Task<DefaultResponseResult> DeleteAsync(int id, string password, string token)
        {
            string url = $"{_routes.EmployeeRoutes.Employee}/{id}";

            var response = await _repository.Delete(url, new { password }, token);

            ValidateResponse(response);

            return response;
        }
    }
}
