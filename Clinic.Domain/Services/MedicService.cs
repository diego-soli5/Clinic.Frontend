using Clinic.CrossCutting.Abstractions;
using Clinic.CrossCutting.CustomExceptions;
using Clinic.CrossCutting.Routes;
using Clinic.Data.Abstractions;
using Clinic.Domain.Abstractions;
using Clinic.Domain.Models.DTOs.ConsultingRoom;
using Clinic.Domain.Models.DTOs.Medic;
using Clinic.Domain.Models.QueryFilters;
using Clinic.Domain.Models.Responses;
using Clinic.Domain.Models.ViewModels.Client.Medic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Clinic.Domain.Services
{
    public class MedicService : IMedicService
    {
        private readonly IRepository _repository;
        private readonly MedicRoutes _medicRoutes;
        private readonly MedicalSpecialtyRoutes _medicalSpecialtyRoutes;
        private readonly ConsultingRoomRoutes _consultingRoomRoutes;
        private readonly IUriGenerator _uriGenerator;

        public MedicService(IRepository repository,
                            MedicRoutes medicRoutes,
                            MedicalSpecialtyRoutes medicalSpecialtyRoutes,
                            ConsultingRoomRoutes consultingRoomRoutes,
                            IUriGenerator uriGenerator)
        {
            _repository = repository;
            _medicRoutes = medicRoutes;
            _medicalSpecialtyRoutes = medicalSpecialtyRoutes;
            _consultingRoomRoutes = consultingRoomRoutes;
            _uriGenerator = uriGenerator;
        }

        public async Task<MedicIndexViewModel> GetAllAsync(MedicQueryFilter filters, string authToken)
        {
            string url = _uriGenerator.CreatePagedListUri(_medicRoutes.Medic, filters).ToString();

            var medicListReponse = await _repository.Get<IEnumerable<MedicListDTO>>(url, authToken: authToken);

            var lstMedicSpecialties = await GetMedicalSpecialtiesSelectListItems(authToken);

            var viewModel = new MedicIndexViewModel
            {
                Medics = medicListReponse.Data,
                Metadata = medicListReponse.Metadata,
                MedicSpecialties = lstMedicSpecialties
            };

            return viewModel;
        }

        public async Task<IEnumerable<MedicDisplayPendingForUpdateDTO>> GetAllMedicsPendingForUpdate(string authToken)
        {
            var queryParams = new
            {
                PendingUpdate = true
            };

            string url = _uriGenerator.AddQueryStringParams(_medicRoutes.Medic, queryParams).ToString();

            var response = await _repository.Get<IEnumerable<MedicDisplayPendingForUpdateDTO>>(url, authToken: authToken);

            return response.Data;
        }

        public async Task<MedicPendingUpdateViewModel> GetMedicPendingForUpdate(int idEmployee, string authToken)
        {
            string url = $"{_medicRoutes.Pending}{idEmployee}";

            var medicResponse = await _repository.Get<MedicPedingUpdateDTO>(url, authToken: authToken);

            if (medicResponse.StatusCode == StatusCodes.Status404NotFound)
            {
                throw new NotFoundException(medicResponse.Message, idEmployee);
            }

            var lstMedicSpecialtiesSelectListItems = await GetMedicalSpecialtiesSelectListItems(authToken);

            var lstConsultingRoomsSelectListItems = await GetConsultingRoomsSelectListItems(authToken);

            var viewModel = new MedicPendingUpdateViewModel
            {
                Medic = medicResponse.Data,
                Success = medicResponse.Success,
                Message = medicResponse.Message,
                MedicalSpecialties = lstMedicSpecialtiesSelectListItems,
                ConsultingRooms = lstConsultingRoomsSelectListItems
            };

            return viewModel;
        }

        public async Task<DefaultApiResponseResult> UpdatePendingMedic(MedicPedingUpdateDTO model, string authToken)
        {
            string url = $"{_medicRoutes.Medic}/{model.IdEmployee}";

            var response = await _repository.Patch(url, model, authToken);

            return response;
        }

        #region UTILITY METHODS
        private async Task<IEnumerable<SelectListItem>> GetMedicalSpecialtiesSelectListItems(string authToken)
        {
            var medicSpecialtiesResponse = await _repository.Get<IEnumerable<MedicalSpecialtyListDTO>>(_medicalSpecialtyRoutes.MedicalSpecialty, authToken: authToken);

            List<SelectListItem> lstMedicSpecialties = new List<SelectListItem>();

            medicSpecialtiesResponse.Data.ToList().ForEach(esp =>
            {
                lstMedicSpecialties.Add(new SelectListItem(esp.Name, esp.Id.ToString()));
            });

            return lstMedicSpecialties;
        }

        private async Task<IEnumerable<SelectListItem>> GetConsultingRoomsSelectListItems(string authToken)
        {
            var consultingRoomsResponse = await _repository.Get<IEnumerable<ConsultingRoomDTO>>(_consultingRoomRoutes.ConsultingRoom, authToken: authToken);

            List<SelectListItem> lstConsultingRooms = new List<SelectListItem>();

            consultingRoomsResponse.Data.ToList().ForEach(room =>
            {
                lstConsultingRooms.Add(new SelectListItem(room.NameIdentifier, room.Id.ToString()));
            });

            return lstConsultingRooms;
        }
        #endregion
    }
}
