using Clinic.Domain.Abstractions;
using Clinic.Domain.Models.Enumerations;
using Clinic.Domain.Models.QueryFilters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace Clinic.Web.Areas.Client.Controllers
{
    [Area("Client")]
    [Authorize(Roles = nameof(EmployeeRole.Secretary))]
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

        #region UTILITY METHODS
        private string GetCurrentToken()
        {
            string token = User.Claims.FirstOrDefault(x => x.Type == "Token").Value;

            return token;
        }
        #endregion
    }
}
