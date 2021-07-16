using Clinic.CrossCutting.Abstractions;
using Clinic.CrossCutting.Cache;
using Clinic.CrossCutting.Routes;
using Clinic.Data.Abstractions;
using Clinic.Domain.Abstractions;
using Clinic.Domain.Models.DTOs.Medic;
using Clinic.Domain.Models.QueryFilters;
using Clinic.Domain.Models.ViewModels.Client.Medic;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Domain.Services
{
    public class MedicService : IMedicService
    {
        private readonly IRepository _repository;
        private readonly MedicRoutes _medicRoutes;
        private readonly IUriGenerator _uriGenerator;

        public MedicService(IRepository repository,
                            MedicRoutes medicRoutes,
                            IUriGenerator uriGenerator)
        {
            _repository = repository;
            _medicRoutes = medicRoutes;
            _uriGenerator = uriGenerator;
        }

        public async Task<MedicIndexViewModel> GetAllAsync(MedicQueryFilter filters, string authToken)
        {
            MedicCache.GetMedicQueryFilterCache(filters);

            string url = _uriGenerator.CreatePagedListUri(_medicRoutes.Medic, filters).ToString();

            var medicListReponse = await _repository.Get<IEnumerable<MedicListDTO>>(url, authToken: authToken);

            MedicCache.SetMedicQueryFilterCache(filters);

            url = _medicRoutes.Specialties;

            var medicSpecialtiesResponse = await _repository.Get<IEnumerable<MedicalSpecialtyListDTO>>(url, authToken: authToken);

            List<SelectListItem> lstMedicSpecialties = new List<SelectListItem>();

            medicSpecialtiesResponse.Data.ToList().ForEach(esp =>
            {
                lstMedicSpecialties.Add(new SelectListItem(esp.Name, esp.Id.ToString()));
            });

            var viewModel = new MedicIndexViewModel
            {
                Medics = medicListReponse.Data,
                Metadata = medicListReponse.Metadata,
                MedicSpecialties = lstMedicSpecialties
            };

            return viewModel;
        }
    }
}
