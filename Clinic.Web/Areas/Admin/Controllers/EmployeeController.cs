using Clinic.Domain.Abstractions;
using Clinic.Domain.Models.DTOs.Employee;
using Clinic.Domain.Models.Enumerations;
using Clinic.Domain.Models.ViewModels.Admin.Employee;
using Clinic.Domain.QueryFilters;
using Microsoft.AspNetCore.Http;
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
        public async Task<IActionResult> Edit(int id)
        {
            var oVM = await _employeeService.GetByIdToUpdateAsync(id);

            if (!oVM.Success)
                return NotFound();

            return View(oVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EmployeeUpdateDTO model)
        {
            EmployeeEditViewModel oVM;

            if (!ModelState.IsValid)
            {
                oVM = new EmployeeEditViewModel(model);

                return View(oVM);
            }

            var response = await _employeeService.UpdateAsync(model);

            if (response.Success)
            {
                TempData["Message"] = $"El empleado {model.Person.Names} se actualizó correctamente!";

                return RedirectToAction("Index");
            }

            oVM = new EmployeeEditViewModel(model)
            {
                Message = response.Message
            };

            return View(oVM);
        }

        [HttpGet]
        public async Task<IActionResult> GetTable(EmployeeQueryFilter filter)
        {
            var oVM = await _employeeService.GetAllAsync(filter);

            return PartialView("_EmployeePagedTablePartial", oVM);
        }

        #region API ACTIONS
        [HttpPost]
        [Route("api/admin/employee/fire/{id}")]
        public async Task<IActionResult> Fire(int id)
        {
            var response = await _employeeService.Fire(id);

            if (response.StatusCode == StatusCodes.Status204NoContent)
            {
                response.Message = "Se despidió exitosamente!";

                return Ok(response);
            }
            else if (response.StatusCode == StatusCodes.Status400BadRequest)
            {
                return BadRequest(response);
            }
            else if (response.StatusCode == StatusCodes.Status404NotFound)
            {
                return NotFound(response);
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }

        [HttpPost]
        [Route("api/admin/employee/activate/{id}")]
        public async Task<IActionResult> Activate(int id)
        {
            var response = await _employeeService.Activate(id);

            if (response.StatusCode == StatusCodes.Status204NoContent)
            {
                response.Message = "Se activó exitosamente!";

                return Ok(response);
            }
            else if (response.StatusCode == StatusCodes.Status400BadRequest)
            {
                return BadRequest(response);
            }
            else if (response.StatusCode == StatusCodes.Status404NotFound)
            {
                return NotFound(response);
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }
        #endregion
    }
}
