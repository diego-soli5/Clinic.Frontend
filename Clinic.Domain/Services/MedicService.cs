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
        private readonly IUriGenerator _uriGenerator;
        private readonly ApiRoutes _routes;

        public MedicService(IRepository repository,
                            ApiRoutes routes,
                            IUriGenerator uriGenerator)
        {
            _repository = repository;
            _uriGenerator = uriGenerator;
            _routes = routes;
        }

        public async Task<DefaultApiResponseResult<IEnumerable<MedicListDTO>>> GetAllAsync(MedicQueryFilter filters, string authToken)
        {
            string url = _uriGenerator.CreatePagedListUri(_routes.MedicRoutes.Medic, filters).ToString();

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

            string url = _uriGenerator.AddQueryStringParams(_routes.MedicRoutes.Medic, queryParams).ToString();

            var response = await _repository.Get<IEnumerable<MedicDisplayPendingForUpdateDTO>>(url, authToken: authToken);

            ValidateResponse(response);

            return response;
        }

        public async Task<DefaultApiResponseResult<MedicPedingUpdateDTO>> GetMedicPendingForUpdate(int idEmployee, string authToken)
        {
            string url = $"{_routes.MedicRoutes.Pending}{idEmployee}";

            var response = await _repository.Get<MedicPedingUpdateDTO>(url, authToken: authToken);

            ValidateResponse(response);

            return response;
        }

        public async Task<DefaultApiResponseResult> UpdatePendingMedic(MedicPedingUpdateDTO model, string authToken)
        {
            string url = $"{_routes.MedicRoutes.Medic}/{model.IdEmployee}";

            var response = await _repository.Patch(url, model, authToken);

            ValidateResponse(response);

            return response;
        }

        public async Task<DefaultApiResponseResult<IEnumerable<MedicalSpecialtyListDTO>>> GetAllMedicalSpecialties(string authToken)
        {
            var response = await _repository.Get<IEnumerable<MedicalSpecialtyListDTO>>(_routes.MedicalSpecialtyRoutes.MedicalSpecialty, authToken: authToken);

            ValidateResponse(response);

            return response;
        }

        public async Task<DefaultApiResponseResult<IEnumerable<ConsultingRoomDTO>>> GetAllConsultingRooms(string authToken)
        {
            var response = await _repository.Get<IEnumerable<ConsultingRoomDTO>>(_routes.ConsultingRoomRoutes.ConsultingRoom, authToken: authToken);

            ValidateResponse(response);

            return response;
        }
    }
}
