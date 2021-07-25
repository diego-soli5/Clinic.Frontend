using Clinic.CrossCutting.Options;
using Microsoft.Extensions.Options;

namespace Clinic.CrossCutting.Routes
{
    public class ApiRoutes
    {
        private readonly AccountRoutes _accountRoutes;
        private readonly ConsultingRoomRoutes _consultingRoomRoutes;
        private readonly EmployeeRoutes _employeeRoutes;
        private readonly MedicalSpecialtyRoutes _medicalSpecialtyRoutes;
        private readonly MedicRoutes _medicRoutes;
        private readonly ResourceRoutes _resourceRoutes;

        public ApiRoutes(AccountRoutes accountRoutes,
                         ConsultingRoomRoutes consultingRoomRoutes,
                         EmployeeRoutes employeeRoutes,
                         MedicalSpecialtyRoutes medicalSpecialtyRoutes,
                         MedicRoutes medicRoutes,
                         ResourceRoutes resourceRoutes)
        {
            _accountRoutes = accountRoutes;
            _consultingRoomRoutes = consultingRoomRoutes;
            _employeeRoutes = employeeRoutes;
            _medicalSpecialtyRoutes = medicalSpecialtyRoutes;
            _medicRoutes = medicRoutes;
            _resourceRoutes = resourceRoutes;
        }

        public AccountRoutes AccountRoutes => _accountRoutes;
        public ConsultingRoomRoutes ConsultingRoomRoutes => _consultingRoomRoutes;
        public EmployeeRoutes EmployeeRoutes => _employeeRoutes;
        public MedicalSpecialtyRoutes MedicalSpecialtyRoutes => _medicalSpecialtyRoutes;
        public MedicRoutes MedicRoutes => _medicRoutes;
        public ResourceRoutes ResourceRoutes => _resourceRoutes;
    }
}
