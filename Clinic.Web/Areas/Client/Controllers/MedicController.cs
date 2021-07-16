using Clinic.Domain.Abstractions;
using Clinic.Domain.Models.QueryFilters;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Clinic.Web.Areas.Client.Controllers
{
    [Area("Client")]
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

            return View(oVM);
        }

        [HttpGet]
        public IActionResult GetTable(MedicQueryFilter filters)
        {
            var oVM = _medicService.GetAllAsync(filters, GetCurrentToken());

            return PartialView("_MedicPagedTablePartial", oVM);
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
