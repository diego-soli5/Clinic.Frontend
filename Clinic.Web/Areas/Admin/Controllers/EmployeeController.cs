using Clinic.Domain.Abstractions;
using Clinic.Domain.Models.DTOs.Employee;
using Clinic.Domain.Models.Enumerations;
using Clinic.Domain.Models.QueryFilters;
using Clinic.Domain.Models.ViewModels.Admin.Employee;
using Clinic.Web.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Clinic.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = nameof(AppUserRole.Administrator))]
    public class EmployeeController : BaseController
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var oViewModel = new EmployeeIndexViewModel();

            var response = await _employeeService.GetAllAsync(new EmployeeQueryFilter(1, EmployeeStatus.Active), CurrentToken);

            oViewModel.Employees = response.Data;
            oViewModel.Metadata = response.Metadata;

            return View(oViewModel);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var oViewModel = new EmployeeCreateViewModel();

            return View(oViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EmployeeCreateDTO model)
        {
            EmployeeCreateViewModel oViewModel;

            if (!ModelState.IsValid)
            {
                oViewModel = new EmployeeCreateViewModel(model);

                return View(oViewModel);
            }

            var response = await _employeeService.CreateAsync(model, CurrentToken);

            if (!response.Success)
            {
                oViewModel = new EmployeeCreateViewModel(model)
                {
                    Message = response.Message
                };

                return View(oViewModel);
            }

            if (model.EmployeeRole == EmployeeRole.Medic)
                TempData["IdMedic"] = response.NewResourceId;

            TempData["EmployeeMessage"] = $"El empleado {model.Person.Names} se agregó correctamente!";

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        { 
            var response = await _employeeService.GetByIdToUpdateAsync(id, CurrentToken);

            if (!response.Success)
            {
                TempData["ErrorEmployeeMessage"] = response.Message;

                return RedirectToAction(nameof(Index));
            }

            var oViewModel = new EmployeeEditViewModel
            {
                Employee = response.Data
            };

            return View(oViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EmployeeUpdateDTO model)
        {
            EmployeeEditViewModel oViewModel;

            if (!ModelState.IsValid)
            {
                oViewModel = new EmployeeEditViewModel(model);

                return View(oViewModel);
            }

            var response = await _employeeService.UpdateAsync(model, CurrentToken);

            if (!response.Success)
            {
                oViewModel = new EmployeeEditViewModel(model)
                {
                    Message = response.Message
                };

                return View(oViewModel);
            }

            TempData["EmployeeMessage"] = $"El empleado {model.Person.Names} se actualizó correctamente!";

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var response = await _employeeService.GetByIdAsync(id, CurrentToken);

            if (!response.Success)
            {
                TempData["ErrorEmployeeMessage"] = response.Message;

                return RedirectToAction(nameof(Index));
            }

            var oViewModel = new EmployeeDetailsViewModel
            {
                Employee = response.Data
            };

            return View(oViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> GetTable(EmployeeQueryFilter filter)
        {
            var response = await _employeeService.GetAllAsync(filter, CurrentToken);

            var oViewModel = new EmployeeIndexViewModel
            {
                Employees = response.Data,
                Metadata = response.Metadata
            };

            return PartialView("_EmployeePagedTablePartial", oViewModel);
        }

        #region API ACTIONS
        [HttpPost]
        [Route("api/admin/employee/delete/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, [FromBody] EmployeeDeleteDTO model)
        {
            var response = await _employeeService.DeleteAsync(id, model.Password, CurrentToken);

            if (response.StatusCode == StatusCodes.Status204NoContent)
            {
                return Ok(response);
            }
            else if (response.StatusCode == StatusCodes.Status400BadRequest)
            {
                return BadRequest(response);
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }
        #endregion
    }
}
