using Clinic.Data.Abstractions;
using Clinic.Domain.Abstractions;
using Clinic.Domain.Models.DTOs.Employee;
using Clinic.CrossCutting.Routes;
using System.Collections.Generic;
using System.Threading.Tasks;
using Clinic.CrossCutting.Abstractions;
using Clinic.Domain.Models.ViewModels.Admin.Employee;
using Clinic.Domain.Models.Responses;
using Clinic.Domain.Models.QueryFilters;

namespace Clinic.Domain.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IRepository _repository;
        private readonly IUriGenerator _uriGenerator;
        private readonly EmployeeRoutes _employeeRoutes;

        public EmployeeService(IRepository repository, EmployeeRoutes employeeRoutes, IUriGenerator uriGenerator)
        {
            _repository = repository;
            _employeeRoutes = employeeRoutes;
            _uriGenerator = uriGenerator;
        }

        public async Task<EmployeeIndexViewModel> GetAllAsync(EmployeeQueryFilter filters, string token)
        {
            string url = _uriGenerator.CreatePagedListUri(_employeeRoutes.Employee, filters).ToString();

            var apiResponse = await _repository.Get<IEnumerable<EmployeeListDTO>>(url, authToken: token);

            var oViewModel = new EmployeeIndexViewModel
            {
                Employees = apiResponse.Data,
                Metadata = apiResponse.Metadata
            };

            return oViewModel;
        }

        public async Task<EmployeeEditViewModel> GetByIdToUpdateAsync(int id, string token)
        {
            string url = $"{_employeeRoutes.Employee}/{id}";

            var queryParams = new
            {
                isUpdate = true
            };

            url = _uriGenerator.AddQueryStringParams(url, queryParams).ToString();

            var apiResponse = await _repository.Get<EmployeeUpdateDTO>(url, authToken: token);

            EmployeeEditViewModel oViewModel = new()
            {
                Employee = apiResponse.Data,
                Message = apiResponse.Message,
                Success = apiResponse.Success
            };

            return oViewModel;
        }

        public async Task<EmployeeDetailsViewModel> GetByIdAsync(int id, string token)
        {
            string url = $"{_employeeRoutes.Employee}/{id}";

            var queryParams = new
            {
                isUpdate = false
            };

            url = _uriGenerator.AddQueryStringParams(url, queryParams).ToString();

            var apiResponse = await _repository.Get<EmployeeDTO>(url, authToken: token);

            EmployeeDetailsViewModel oViewModel = new()
            {
                Employee = apiResponse.Data,
                Message = apiResponse.Message,
                Success = apiResponse.Success
            };

            return oViewModel;
        }

        public async Task<DefaultPutApiResponse> UpdateAsync(EmployeeUpdateDTO model, string token)
        {
            string url = $"{_employeeRoutes.Employee}/{model.Id}";

            return await _repository.Put(url, model, token);
        }

        public async Task<DefaultPostApiResponse> CreateAsync(EmployeeCreateDTO model, string token)
        {
            string url = _employeeRoutes.Employee;

            return await _repository.Post(url, model, token);
        }

        public async Task<DefaultDeleteApiResponse> DeleteAsync(int id, string password, string token)
        {
            string url = $"{_employeeRoutes.Employee}/{id}";

            return await _repository.Delete(url, new { password }, token);
        }

        public async Task<DefaultPatchApiResponse> Fire(int id, string token)
        {
            string url = _employeeRoutes.Fire + id;

            return await _repository.Patch(url, authToken: token);
        }

        public async Task<DefaultPatchApiResponse> Activate(int id, string token)
        {
            string url = _employeeRoutes.Activate + id;

            return await _repository.Patch(url, authToken: token);
        }
    }
}
