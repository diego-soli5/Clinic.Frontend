using Clinic.CrossCutting.Options;
using Microsoft.Extensions.Options;

namespace Clinic.CrossCutting.Routes
{
    public class MedicalSpecialtyRoutes
    {
        private readonly ApiOptions _apiOptions;

        public MedicalSpecialtyRoutes(IOptions<ApiOptions> options)
        {
            _apiOptions = options.Value;

            MedicalSpecialty = $"{_apiOptions.Domain}/api/{nameof(MedicalSpecialty)}";
        }

        public string MedicalSpecialty { get; private set; }
    }
}
