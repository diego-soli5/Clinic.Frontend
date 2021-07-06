using Clinic.Data.Abstractions;
using Clinic.Domain.Abstractions;
using Clinic.Domain.Models.DTOs.Employee;
using Clinic.CrossCutting.Routes;
using System.Collections.Generic;
using System.Threading.Tasks;
using Clinic.Domain.QueryFilters;
using Clinic.CrossCutting.Abstractions;
using Clinic.Domain.Models.ViewModels.Admin.Employee;
using Clinic.CrossCutting.Cache;
using Clinic.Domain.Models.Responses;

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

        public async Task<EmployeeListViewModel> GetAllAsync(EmployeeQueryFilter filters)
        {
            EmployeeCache.GetEmployeeQueryFilterCache(filters);

            string url = _uriGenerator.CreatePagedListUri(_employeeRoutes.Employee, filters).ToString();

            var apiResponse = await _repository.Get<IEnumerable<EmployeeListDTO>>(url);

            EmployeeCache.SetEmployeeQueryFilterCache(filters);

            var oViewModel = new EmployeeListViewModel
            {
                Employees = apiResponse.Data,
                Metadata = apiResponse.Metadata
            };

            return oViewModel;
        }

        public async Task<EmployeeEditViewModel> GetByIdToUpdateAsync(int id)
        {
            string url = $"{_employeeRoutes.Employee}/{id}";

            url = _uriGenerator.AddQueryStringParams(url, new Dictionary<string, object> { { "isUpdate", true } }).ToString();

            var apiResponse = await _repository.Get<EmployeeUpdateDTO>(url);

            EmployeeEditViewModel oViewModel = new()
            {
                Employee = apiResponse.Data,
                Message = apiResponse.Message,
                Success = apiResponse.Success
            };

            return oViewModel;
        }

        public async Task<EmployeeDetailsViewModel> GetByIdAsync(int id)
        {
            string url = $"{_employeeRoutes.Employee}/{id}";

            url = _uriGenerator.AddQueryStringParams(url, new Dictionary<string, object> { { "isToUpdate", false } }).ToString();

            var apiResponse = await _repository.Get<EmployeeDTO>(url);

            EmployeeDetailsViewModel oViewModel = new()
            {
                Employee = apiResponse.Data,
                Message = apiResponse.Message,
                Success = apiResponse.Success
            };

            return oViewModel;
        }

        public async Task<DefaultPutApiResponse> UpdateAsync(EmployeeUpdateDTO model)
        {
            string url = $"{_employeeRoutes.Employee}/{model.Id}";

            return await _repository.Put(url, model);
        }

        public async Task<DefaultPostApiResponse> CreateAsync(EmployeeCreateDTO model)
        {
            string url = _employeeRoutes.Employee;

            return await _repository.Post(url, model);
        }

        public async Task<DefaultDeleteApiResponse> DeleteAsync(int id, string password)
        {
            string url = $"{_employeeRoutes.Employee}/{id}";

            return await _repository.Delete(url, new { password });
        }

        public async Task<DefaultPatchApiResponse> Fire(int id)
        {
            string url = _employeeRoutes.Fire + id;

            return await _repository.Patch(url);
        }

        public async Task<DefaultPatchApiResponse> Activate(int id)
        {
            string url = _employeeRoutes.Activate + id;

            return await _repository.Patch(url);
        }
    }
}
