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

namespace Clinic.Domain.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IRepository _repository;
        private readonly IUriGenerator _uriGenerator;
        private readonly ApiRoutes _apiRoutes;

        public EmployeeService(IRepository repository, ApiRoutes apiRoutes, IUriGenerator uriGenerator)
        {
            _repository = repository;
            _apiRoutes = apiRoutes;
            _uriGenerator = uriGenerator;
        }

        public async Task<EmployeeListViewModel> GetAllAsync(EmployeeQueryFilter filters)
        {
            EmployeeCache.GetEmployeeQueryFilterCache(filters);

            string url = _uriGenerator.CreateUri(_apiRoutes.Employee, filters).ToString();

            var apiResponse = await _repository.Get<IEnumerable<EmployeeListDTO>>(url);

            EmployeeCache.SetEmployeeQueryFilterCache(filters);

            var oViewModel = new EmployeeListViewModel
            {
                Employees = apiResponse.Data,
                Metadata = apiResponse.Metadata
            };

            return oViewModel;
        }
    }
}
