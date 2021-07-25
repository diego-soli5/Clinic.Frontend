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
using Microsoft.AspNetCore.Http;
using Clinic.CrossCutting.CustomExceptions;

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

        public async Task<EmployeeIndexViewModel> GetAllAsync(EmployeeQueryFilter filters, string token)
        {
            string url = _uriGenerator.CreatePagedListUri(_employeeRoutes.Employee, filters).ToString();

            var response = await _repository.Get<IEnumerable<EmployeeListDTO>>(url, authToken: token);

            var oViewModel = new EmployeeIndexViewModel
            {
                Employees = response.Data,
                Metadata = response.Metadata
            };

            ValidateResponse(response);

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

            var response = await _repository.Get<EmployeeUpdateDTO>(url, authToken: token);

            EmployeeEditViewModel oViewModel = new()
            {
                Employee = response.Data,
                Message = response.Message,
                Success = response.Success
            };

            ValidateResponse(response);

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

            var response = await _repository.Get<EmployeeDTO>(url, authToken: token);

            if (response.StatusCode == StatusCodes.Status404NotFound)
            {
                throw new NotFoundException(response.Message, id);
            }

            EmployeeDetailsViewModel oViewModel = new()
            {
                Employee = response.Data,
                Message = response.Message,
                Success = response.Success
            };

            ValidateResponse(response);

            return oViewModel;
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

        #region DESECHADO
        //Codigo comentado por posibilidad de reintegrar la funcionalidad
        /*
        public async Task<DefaultPatchApiResponse> Fire(int id, string token)
        {
            string url = _employeeRoutes.Fire + id;

            return await _repository.Patch(url, authToken: token);
        }

        public async Task<DefaultPatchApiResponse> Activate(int id, string token)
        {
            string url = _employeeRoutes.Activate + id;

            return await _repository.Patch(url, authToken: token);
        }*/
        #endregion
    }
}
