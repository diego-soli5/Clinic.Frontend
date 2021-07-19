using Clinic.Domain.Abstractions;
using Clinic.Domain.Models.DTOs.Medic;
using Clinic.Domain.Models.Enumerations;
using Clinic.Domain.Models.QueryFilters;
using Clinic.Domain.Models.ViewModels.Client.Medic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Clinic.Web.Areas.Client.Controllers
{
    [Area("Client")]
    [Authorize(Roles = nameof(EmployeeRole.Secretary) + "," + nameof(AppUserRole.Administrator))]
    public class MedicController : Controller
    {
        private readonly IMedicService _medicService;

        public MedicController(IMedicService medicService)
        {
            _medicService = medicService;
        }

        public async Task<IActionResult> Index()
        {
            var oVM = await _medicService.GetAllAsync(new MedicQueryFilter(1), GetCurrentToken());

            oVM.MedicsPendingForUpdate = await _medicService.GetAllMedicsPendingForUpdate(GetCurrentToken());

            return View(oVM);
        }

        [HttpGet]
        public async Task<IActionResult> GetTable(MedicQueryFilter filters)
        {
            var oVM = await _medicService.GetAllAsync(filters, GetCurrentToken());

            return PartialView("_MedicPagedTablePartial", oVM);
        }

        [HttpGet]
        public async Task<IActionResult> GetPendingForUpdate()
        {
            var oVM = await _medicService.GetAllMedicsPendingForUpdate(GetCurrentToken());

            return PartialView("_MedicPendingUpdatePartial", oVM);
        }

        [HttpGet]
        public async Task<IActionResult> PendingUpdate(int id)
        {
            var oVM = await _medicService.GetMedicPendingForUpdate(id, GetCurrentToken());

            if (!oVM.Success)
            {
                TempData["ErrorMedicMessage"] = oVM.Message;

                return RedirectToAction(nameof(Index));
            }

            oVM.ConsultingRooms = new List<SelectListItem>();
            oVM.MedicalSpecialties = new List<SelectListItem>();

            return View(oVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PendingUpdate(MedicPedingUpdateDTO model)
        {
            if (!ModelState.IsValid)
            {
                var oVM = await _medicService.GetMedicPendingForUpdate(model.IdEmployee, GetCurrentToken());

                if (!oVM.Success)
                {
                    TempData["ErrorMedicMessage"] = oVM.Message;

                    return RedirectToAction(nameof(Index));
                }

                oVM.ConsultingRooms = new List<SelectListItem>();
                oVM.MedicalSpecialties = new List<SelectListItem>();

                return View(oVM);
            }

            TempData["MedicMessage"] = $"Se actualizó correctamente la información del médico {model.Names}.";

            return RedirectToAction(nameof(Index));
        }

        #region UTILITY METHODS
        private string GetCurrentToken()
        {
            string token = User.Claims.FirstOrDefault(x => x.Type == "Token").Value;

            return token;
        }
        #endregion
    }
}
