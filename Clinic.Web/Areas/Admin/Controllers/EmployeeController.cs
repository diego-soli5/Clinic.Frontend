using Clinic.Domain.Abstractions;
using Clinic.Domain.Models.Enumerations;
using Clinic.Domain.QueryFilters;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Clinic.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var oVM = await _employeeService.GetAllAsync(new EmployeeQueryFilter(1, EmployeeStatus.Active));

            return View(oVM);
        }

        [HttpGet]
        public async Task<IActionResult> GetTable(EmployeeQueryFilter filter)
        {
            var oVM = await _employeeService.GetAllAsync(filter);

            return PartialView("_EmployeePagedTablePartial", oVM);
        }
    }
}
