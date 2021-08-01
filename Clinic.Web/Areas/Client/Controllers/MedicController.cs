using Clinic.Domain.Abstractions;
using Clinic.Domain.Models.DTOs.Medic;
using Clinic.Domain.Models.Enumerations;
using Clinic.Domain.Models.QueryFilters;
using Clinic.Domain.Models.ViewModels.Client.Medic;
using Clinic.Web.Controllers;
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
    public class MedicController : BaseController
    {
        private readonly IMedicService _medicService;

        public MedicController(IMedicService medicService)
        {
            _medicService = medicService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var oViewModel = new MedicIndexViewModel();

            var medicListResponse = await _medicService.GetAllAsync(new MedicQueryFilter(1), CurrentToken);
            var medicsPendingForUpdateResponse = await _medicService.GetAllMedicsPendingForUpdate(CurrentToken);

            oViewModel.Medics = medicListResponse.Data;
            oViewModel.Metadata = medicListResponse.Metadata;
            oViewModel.MedicalSpecialtiesSelectListItems = await GetMedicalSpecialtiesSLI();
            oViewModel.MedicsPendingForUpdate = medicsPendingForUpdateResponse.Data;

            return View(oViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> GetTable(MedicQueryFilter filters)
        {
            var oViewModel = new MedicIndexViewModel();

            var medicListResponse = await _medicService.GetAllAsync(filters, CurrentToken);

            oViewModel.Medics = medicListResponse.Data;
            oViewModel.Metadata = medicListResponse.Metadata;

            return PartialView("_MedicPagedTablePartial", oViewModel);
        }
        
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var oViewModel = new MedicEditViewModel();

            var medicResponse = await _medicService.GetMedicForEdit(id, CurrentToken);

            if (!medicResponse.Success)
            {
                TempData["ErrorMedicMessage"] = medicResponse.Message;

                return RedirectToAction(nameof(Index));
            }

            oViewModel.ConsultingRoomSelectListItems = await GetConsultingRoomsSLI();
            oViewModel.MedicalSpecialtiesSelectListItems = await GetMedicalSpecialtiesSLI();
            oViewModel.Medic = medicResponse.Data;

            return View(oViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(MedicUpdateDTO model)
        {
            MedicEditViewModel oViewModel;

            if (!ModelState.IsValid)
            {
                oViewModel = new MedicEditViewModel();

                oViewModel.ConsultingRoomSelectListItems = await GetConsultingRoomsSLI();
                oViewModel.MedicalSpecialtiesSelectListItems = await GetMedicalSpecialtiesSLI();
                oViewModel.Medic = model;

                return View(oViewModel);
            }

            var response = await _medicService.EditMedic(model, CurrentToken);

            if (!response.Success)
            {
                ViewData["ErrorMessage"] = response.Message;

                oViewModel = new MedicEditViewModel();

                oViewModel.ConsultingRoomSelectListItems = await GetConsultingRoomsSLI();
                oViewModel.MedicalSpecialtiesSelectListItems = await GetMedicalSpecialtiesSLI();
                oViewModel.Medic = model;

                return View(oViewModel);
            }

            TempData["MedicMessage"] = $"Se actualizó correctamente la informacion del médico {model.Names}.";

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> GetPendingForUpdate()
        {
            var medicsPendingForUpdateResponse = await _medicService.GetAllMedicsPendingForUpdate(CurrentToken);

            var medicsPendingForUpdateList = medicsPendingForUpdateResponse.Data;

            return PartialView("_MedicPendingUpdatePartial", medicsPendingForUpdateList);
        }

        [HttpGet]
        public async Task<IActionResult> PendingUpdate(int id)
        {
            var oViewModel = new MedicPendingUpdateViewModel();

            var medicPendingResponse = await _medicService.GetMedicPendingForUpdate(id, CurrentToken);

            if (!medicPendingResponse.Success)
            {
                TempData["ErrorMedicMessage"] = medicPendingResponse.Message;

                return RedirectToAction(nameof(Index));
            }

            oViewModel.ConsultingRoomSelectListItems = await GetConsultingRoomsSLI();
            oViewModel.MedicalSpecialtiesSelectListItems = await GetMedicalSpecialtiesSLI();
            oViewModel.Medic = medicPendingResponse.Data;

            return View(oViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PendingUpdate(MedicPedingUpdateDTO model)
        {
            if (!ModelState.IsValid)
            {
                var oViewModel = new MedicPendingUpdateViewModel();

                var medicPendingResponse = await _medicService.GetMedicPendingForUpdate(model.IdEmployee, CurrentToken);

                if (!medicPendingResponse.Success)
                {
                    TempData["ErrorMedicMessage"] = medicPendingResponse.Message;

                    return RedirectToAction(nameof(Index));
                }

                oViewModel.ConsultingRoomSelectListItems = await GetConsultingRoomsSLI();
                oViewModel.MedicalSpecialtiesSelectListItems = await GetMedicalSpecialtiesSLI();
                oViewModel.Medic = medicPendingResponse.Data;

                return View(oViewModel);
            }

            var response = await _medicService.UpdatePendingMedic(model, CurrentToken);

            if (!response.Success)
            {
                TempData["ErrorMedicMessage"] = response.Message;
            }
            else
            {
                TempData["MedicMessage"] = $"Se actualizó correctamente la información del médico {model.Names}.";
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult AttentionSchedules()
        {
            return View();
        }

        #region UTILITY METHODS
        private async Task<IEnumerable<SelectListItem>> GetMedicalSpecialtiesSLI()
        {
            var medicalSpecialtiesResponse = await _medicService.GetAllMedicalSpecialties(CurrentToken);

            var lst = new List<SelectListItem>();

            medicalSpecialtiesResponse.Data.ToList().ForEach(ms =>
            {
                lst.Add(new SelectListItem(ms.Name, ms.Id.ToString()));
            });

            return lst;
        }

        private async Task<IEnumerable<SelectListItem>> GetConsultingRoomsSLI()
        {
            var consultingRoomsResponse = await _medicService.GetAllConsultingRooms(CurrentToken);

            var lst = new List<SelectListItem>();

            consultingRoomsResponse.Data.ToList().ForEach(room =>
            {
                lst.Add(new SelectListItem(room.NameIdentifier, room.Id.ToString()));
            });

            return lst;
        }
        #endregion
    }
}
