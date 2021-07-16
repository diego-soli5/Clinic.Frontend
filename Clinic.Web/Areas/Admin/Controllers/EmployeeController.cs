using Clinic.CrossCutting.CustomExceptions;
using Clinic.Domain.Abstractions;
using Clinic.Domain.Models.DTOs.Employee;
using Clinic.Domain.Models.Enumerations;
using Clinic.Domain.Models.QueryFilters;
using Clinic.Domain.Models.ViewModels.Admin.Employee;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Linq;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;

namespace Clinic.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = nameof(AppUserRole.Administrator))]
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
            var oVM = await _employeeService.GetAllAsync(new EmployeeQueryFilter(1, EmployeeStatus.Active), GetCurrentToken());

            return View(oVM);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var oVM = new EmployeeCreateViewModel();

            return View(oVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EmployeeCreateDTO model)
        {
            EmployeeCreateViewModel oVM;

            if (!ModelState.IsValid)
            {
                oVM = new EmployeeCreateViewModel(model);

                return View(oVM);
            }

            var response = await _employeeService.CreateAsync(model, GetCurrentToken());

            if (response.Success)
            {
                TempData["EmployeeMessage"] = $"El empleado {model.Person.Names} se agregó correctamente!";

                return RedirectToAction(nameof(Index));
            }

            oVM = new EmployeeCreateViewModel(model)
            {
                Message = response.Message
            };

            return View(oVM);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var oVM = await _employeeService.GetByIdToUpdateAsync(id, GetCurrentToken());

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

            var response = await _employeeService.UpdateAsync(model, GetCurrentToken());

            if (response.Success)
            {
                TempData["EmployeeMessage"] = $"El empleado {model.Person.Names} se actualizó correctamente!";

                return RedirectToAction(nameof(Index));
            }

            oVM = new EmployeeEditViewModel(model)
            {
                Message = response.Message
            };

            return View(oVM);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var oVM = await _employeeService.GetByIdAsync(id, GetCurrentToken());

            return View(oVM);
        }

        [HttpGet]
        public async Task<IActionResult> GetTable(EmployeeQueryFilter filter)
        {
            var oVM = await _employeeService.GetAllAsync(filter, GetCurrentToken());

            return PartialView("_EmployeePagedTablePartial", oVM);
        }

        #region API ACTIONS
        [HttpPost]
        [Route("api/admin/employee/fire/{id}")]
        public async Task<IActionResult> Fire(int id)
        {
            var response = await _employeeService.Fire(id, GetCurrentToken());

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
            var response = await _employeeService.Activate(id, GetCurrentToken());

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

        [HttpPost]
        [Route("api/admin/employee/delete/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, [FromBody] EmployeeDeleteDTO model)
        {
            var response = await _employeeService.DeleteAsync(id, model.Password, GetCurrentToken());

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

        #region UTILITY METHODS
        private string GetCurrentToken()
        {
            string token = User.Claims.FirstOrDefault(x => x.Type == "Token").Value;

            return token;
        }
        #endregion
    }
}
