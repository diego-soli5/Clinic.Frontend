using Clinic.CrossCutting.Abstractions;
using Clinic.CrossCutting.Routes;
using Clinic.Data.Abstractions;
using Clinic.Domain.Abstractions;
using Clinic.Domain.Models.DTOs.ConsultingRoom;
using Clinic.Domain.Models.DTOs.Medic;
using Clinic.Domain.Models.QueryFilters;
using Clinic.Domain.Models.Responses;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Clinic.Domain.Services
{
    public class MedicService : ServiceMiddleware, IMedicService
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

        public async Task<DefaultApiResponseResult<IEnumerable<MedicListDTO>>> GetAllAsync(MedicQueryFilter filters, string authToken)
        {
            string url = _uriGenerator.CreatePagedListUri(_medicRoutes.Medic, filters).ToString();

            var response = await _repository.Get<IEnumerable<MedicListDTO>>(url, authToken: authToken);

            ValidateResponse(response);

            return response;
        }

        public async Task<DefaultApiResponseResult<IEnumerable<MedicDisplayPendingForUpdateDTO>>> GetAllMedicsPendingForUpdate(string authToken)
        {
            var queryParams = new
            {
                PendingUpdate = true
            };

            string url = _uriGenerator.AddQueryStringParams(_medicRoutes.Medic, queryParams).ToString();

            var response = await _repository.Get<IEnumerable<MedicDisplayPendingForUpdateDTO>>(url, authToken: authToken);

            ValidateResponse(response);

            return response;
        }

        public async Task<DefaultApiResponseResult<MedicPedingUpdateDTO>> GetMedicPendingForUpdate(int idEmployee, string authToken)
        {
            string url = $"{_medicRoutes.Pending}{idEmployee}";

            var response = await _repository.Get<MedicPedingUpdateDTO>(url, authToken: authToken);

            ValidateResponse(response);

            return response;
        }

        public async Task<DefaultApiResponseResult> UpdatePendingMedic(MedicPedingUpdateDTO model, string authToken)
        {
            string url = $"{_medicRoutes.Medic}/{model.IdEmployee}";

            var response = await _repository.Patch(url, model, authToken);

            ValidateResponse(response);

            return response;
        }

        public async Task<DefaultApiResponseResult<IEnumerable<MedicalSpecialtyListDTO>>> GetAllMedicalSpecialties(string authToken)
        {
            var response = await _repository.Get<IEnumerable<MedicalSpecialtyListDTO>>(_medicalSpecialtyRoutes.MedicalSpecialty, authToken: authToken);

            ValidateResponse(response);

            return response;
        }

        public async Task<DefaultApiResponseResult<IEnumerable<ConsultingRoomDTO>>> GetAllConsultingRooms(string authToken)
        {
            var response = await _repository.Get<IEnumerable<ConsultingRoomDTO>>(_consultingRoomRoutes.ConsultingRoom, authToken: authToken);

            ValidateResponse(response);

            return response;
        }
    }
}
